using APICatalogo.Controllers;
using APICatalogo.Data;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogoxUnitTests
{
    public class CategoriasUnitTestController
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        public static string connectionString =
            "server=localhost;userid=root;password=Teste@123;database=CatalogoDB";

        static CategoriasUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;
        }

        public CategoriasUnitTestController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();

            var context = new AppDbContext(dbContextOptions);

            _uof = new UnitOfWork(context);
        }

        //testes unitários

        //testar o método Get

        [Fact]
        public void GetCategorias_Return_OkResult()
        {
            //Arrange
            var controller = new CategoriasController(_uof, _mapper);

            //act
            var data = controller.GetCategoriasProdutos();

            //Assert
            Assert.IsType<List<CategoriaDTO>>(data.Value);
        }

        //Get -- BadRequest
        [Fact]
        public void GetCategorias_Return_BadRequestResult()
        {
            //Arrange
            var controller = new CategoriasController(_uof, _mapper);

            //act
            var data = controller.GetCategoriasProdutos();

            //Assert
            Assert.IsType<BadRequestResult>(data.Result);
        }
    }
}
