using SparkEquation.Trial.WebAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SparkEquation.Trial.WebAPI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Featured { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int ItemsInStock { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public double Rating { get; set; }
        public int BrandId { get; set; }
        public IList<int> CategoryIds { get; set; } = new List<int>();

        public Product ToEntity()
        {
            var entity = new Product()
            {
                Id = Id,
                Name = Name,
                Featured = Featured,
                ExpirationDate = ExpirationDate,
                ItemsInStock = ItemsInStock,
                ReceiptDate = ReceiptDate,
                Rating = Rating,
                BrandId = BrandId,
                CategoryProducts = new List<CategoryProduct>(),
            };

            return entity;
        }

        public static ProductModel CreateFromEntity(Product product)
        {
            var model = new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Featured = product.Featured,
                ExpirationDate = product.ExpirationDate,
                ItemsInStock = product.ItemsInStock,
                ReceiptDate = product.ReceiptDate,
                Rating = product.Rating,
                BrandId = product.BrandId,
                CategoryIds = product.CategoryProducts?.Select(m => m.CategoryId).ToList(),
            };

            return model;
        }
    }

    
}
