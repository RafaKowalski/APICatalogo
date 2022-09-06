using APICatalogo.Filter;
using APICatalogo.Models;
using APICatalogo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uol;

        public ProdutosController(IUnitOfWork context)
        {
            _uol = context;
        }

        [HttpGet("MenorPreco")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPrecos()
        {
            return _uol.ProdutoRepository.GetProdutosPorPreco().ToList();
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _uol.ProdutoRepository.Get().ToList();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }

            return produtos;
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _uol.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto is null)
                return NotFound();

            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();

            _uol.ProdutoRepository.Add(produto);
            _uol.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
                return BadRequest();

            _uol.ProdutoRepository.Update(produto);
            _uol.Commit();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _uol.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound();
            }

            _uol.ProdutoRepository.Delete(produto);
            _uol.Commit();

            return Ok();
        }
    }
}
