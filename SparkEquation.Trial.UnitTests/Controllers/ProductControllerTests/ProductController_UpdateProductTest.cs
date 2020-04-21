using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SparkEquation.Trial.UnitTests.Common;
using SparkEquation.Trial.WebAPI.Controllers;
using SparkEquation.Trial.WebAPI.Data;
using SparkEquation.Trial.WebAPI.Data.Factory;
using SparkEquation.Trial.WebAPI.Data.Models;
using SparkEquation.Trial.WebAPI.Exceptions;
using SparkEquation.Trial.WebAPI.Models;
using SparkEquation.Trial.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SparkEquation.Trial.UnitTests.Controllers.ProductControllerTests
{
    public class ProductController_UpdateProductTest
    {
        [Fact]
        public async Task UpdateProduct_Success()
        {
            var context = TestHelper.GetDbContext();
            
            context.Brands.Add(new Brand(){Id = 1, Name = "Brand01", Country = "Country01"});
            context.Categories.Add(new Category(){ Id = 1, Name = "Category1" });
            context.Categories.Add(new Category(){ Id = 2, Name = "Category2" });
            context.Products.Add(new Product {Id = 1, Name = "Prod01", BrandId = 1});
            context.CategoryProducts.Add(new CategoryProduct(){ CategoryId = 1, ProductId = 1 });
            context.SaveChanges();
            
            var contextFactoryMock = new Mock<IContextFactory>();
            contextFactoryMock.Setup(m => m.GetContext()).Returns(context);

            var model = new ProductModel()
            {
                Id = 1,
                BrandId = 1,
                CategoryIds = { 2 },
                Name = "ChangedProduct01",
            };

            var productService = new ProductsService(contextFactoryMock.Object);
            var productController = new ProductController(productService, new ProductBusinessLogic());
            var result = (JsonResult) await productController.UpdateProduct(model);
            var ret = (bool)result.Value;

            Assert.True(ret);
        }

        [Fact]
        public async Task UpdateProduct_NotFound()
        {
            var context = TestHelper.GetDbContext();
            var contextFactoryMock = new Mock<IContextFactory>();
            contextFactoryMock.Setup(m => m.GetContext()).Returns(context);

            var model = new ProductModel()
            {
                Id = 0,
                BrandId = 1,
                CategoryIds = { 1 },
                Name = "Product01",
            };

            var productService = new ProductsService(contextFactoryMock.Object);
            var productController = new ProductController(productService, new ProductBusinessLogic());
            var result = (BadRequestObjectResult) await productController.UpdateProduct(model);

            Assert.Equal(400, result.StatusCode);
        }
    }
}
