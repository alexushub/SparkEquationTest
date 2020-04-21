using SparkEquation.Trial.WebAPI.Data.Models;
using SparkEquation.Trial.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SparkEquation.Trial.UnitTests.Models.ProductModelTests
{
    public class ProductModelTests
    {
        [Fact]
        public void ProductModel_ToEntityTest()
        {
            var model = new ProductModel()
            {
                Id = 1,
                ItemsInStock = 2,
                ExpirationDate = null,
                ReceiptDate = null,
                BrandId = 3, 
                CategoryIds = { 1, 2 },
                Featured = true,
                Name = "Name",
                Rating = 5,
            };

            var entity = model.ToEntity();

            Assert.Equal(1, entity.Id);
            Assert.Equal(2, entity.ItemsInStock);
            Assert.Null(entity.ExpirationDate);
            Assert.Null(entity.ReceiptDate);
            Assert.Equal(3, entity.BrandId);
            Assert.True(entity.Featured);
            Assert.Equal("Name", entity.Name);
            Assert.Equal(5, entity.Rating);
            Assert.Empty(entity.CategoryProducts);
        }

        [Fact]
        public void ProductModel_CreateFromEntityTest()
        {
            var entity = new Product()
            {
                Id = 1,
                ItemsInStock = 2,
                ExpirationDate = null,
                ReceiptDate = null,
                BrandId = 3, 
                CategoryProducts = new List<CategoryProduct>{ new CategoryProduct(){ CategoryId = 1 }, new CategoryProduct(){ CategoryId = 2 } },
                Featured = true,
                Name = "Name",
                Rating = 5,
            };

            var model = ProductModel.CreateFromEntity(entity);

            Assert.Equal(1, model.Id);
            Assert.Equal(2, model.ItemsInStock);
            Assert.Null(model.ExpirationDate);
            Assert.Null(model.ReceiptDate);
            Assert.Equal(3, model.BrandId);
            Assert.True(model.Featured);
            Assert.Equal("Name", model.Name);
            Assert.Equal(5, model.Rating);
            Assert.Collection(model.CategoryIds, m => Assert.Equal(1, m), m => Assert.Equal(2, m));
        }
    }
}
