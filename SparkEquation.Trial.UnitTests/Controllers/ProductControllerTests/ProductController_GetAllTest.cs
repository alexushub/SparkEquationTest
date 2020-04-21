using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SparkEquation.Trial.UnitTests.Common;
using SparkEquation.Trial.WebAPI.Controllers;
using SparkEquation.Trial.WebAPI.Data;
using SparkEquation.Trial.WebAPI.Data.Factory;
using SparkEquation.Trial.WebAPI.Data.Models;
using SparkEquation.Trial.WebAPI.Models;
using SparkEquation.Trial.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SparkEquation.Trial.UnitTests.Controllers.ProductControllerTests
{
    public class ProductController_GetAllTest
    {
        [Fact]
        public async Task GetAll()
        {
            var context = TestHelper.GetDbContext();
            
            context.Brands.Add(new Brand(){Id = 1, Name = "Brand01", Country = "Country01"});
            context.Products.Add(new Product {Id = 1, Name = "Prod01", BrandId = 1});
            context.SaveChanges();
            
            var contextFactoryMock = new Mock<IContextFactory>();
            contextFactoryMock.Setup(m => m.GetContext()).Returns(context);

            var productService = new ProductsService(contextFactoryMock.Object);
            var productController = new ProductController(productService, new ProductBusinessLogic());
            var result = (JsonResult)await productController.GetAll();
            var model = (List<ProductModel>)result.Value;
            Assert.NotNull(model);
            Assert.Collection(model, m => 
            {
                Assert.Equal(1, m.Id);
                Assert.Equal("Prod01", m.Name);
                Assert.Equal(1, m.BrandId);
            });
        }
        
    }
}
