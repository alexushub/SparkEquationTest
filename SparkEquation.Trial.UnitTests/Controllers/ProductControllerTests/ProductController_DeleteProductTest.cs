using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SparkEquation.Trial.UnitTests.Common;
using SparkEquation.Trial.WebAPI.Controllers;
using SparkEquation.Trial.WebAPI.Data;
using SparkEquation.Trial.WebAPI.Data.Factory;
using SparkEquation.Trial.WebAPI.Data.Models;
using SparkEquation.Trial.WebAPI.Exceptions;
using SparkEquation.Trial.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SparkEquation.Trial.UnitTests.Controllers.ProductControllerTests
{
    public class ProductController_DeleteProductTest
    {
        [Fact]
        public async Task DeleteProduct_Success()
        {
            var context = TestHelper.GetDbContext();
            
            context.Brands.Add(new Brand(){Id = 1, Name = "Brand01", Country = "Country01"});
            context.Categories.Add(new Category(){ Id = 1, Name = "Category1" });
            context.Products.Add(new Product {Id = 1, Name = "Prod01", BrandId = 1});
            context.CategoryProducts.Add(new CategoryProduct(){ CategoryId = 1, ProductId = 1 });
            context.SaveChanges();
            
            var contextFactoryMock = new Mock<IContextFactory>();
            contextFactoryMock.Setup(m => m.GetContext()).Returns(context);

            var productService = new ProductsService(contextFactoryMock.Object);
            var productController = new ProductController(productService, new ProductBusinessLogic());
            var result = (JsonResult) await productController.DeleteProduct(1);
            var model = (bool)result.Value;

            Assert.True(model);
        }

        [Fact]
        public async Task DeleteProduct_NotFound()
        {
            var context = TestHelper.GetDbContext();
            var contextFactoryMock = new Mock<IContextFactory>();
            contextFactoryMock.Setup(m => m.GetContext()).Returns(context);

            var productService = new ProductsService(contextFactoryMock.Object);
            var productController = new ProductController(productService, new ProductBusinessLogic());
            var result = (BadRequestObjectResult) await productController.DeleteProduct(1);
            
            Assert.Equal(400, result.StatusCode);
        }
        
    }
}
