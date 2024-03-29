﻿using APICatalogo.Data;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters)
        {
            //return Get()
            //    .OrderBy(on => on.Nome)
            //    .Skip((categoriasParameters.PageNumber - 1) * categoriasParameters.PageSize)
            //    .Take(categoriasParameters.PageSize)
            //    .ToList();

            return PagedList<Categoria>
                .ToPagedList(Get()
                .OrderBy(on => on.Nome), categoriasParameters.PageNumber, categoriasParameters.PageSize);
        }

        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            return Get().Include(x => x.Produtos);
        }
    }
}
