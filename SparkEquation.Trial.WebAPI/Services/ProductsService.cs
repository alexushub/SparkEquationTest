using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SparkEquation.Trial.WebAPI.Data;
using SparkEquation.Trial.WebAPI.Data.Factory;
using SparkEquation.Trial.WebAPI.Data.Models;
using SparkEquation.Trial.WebAPI.Exceptions;
using SparkEquation.Trial.WebAPI.Models;

namespace SparkEquation.Trial.WebAPI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IContextFactory _factory;
        
        public ProductsService(IContextFactory contextFactory)
        {
            _factory = contextFactory;
        }

        public int CreateProduct(ProductModel productModel)
        {
            productModel.CategoryIds = productModel.CategoryIds ?? new List<int>();
            var product = productModel.ToEntity();

            using (var context = _factory.GetContext())
            {
                CheckIfAllCategoriesExist(productModel.CategoryIds, context);
                var result = context.Products.Add(product);
                context.SaveChanges();

                AddCategoriesToProduct(product.Id, productModel.CategoryIds, context);

                context.SaveChanges();

                return product.Id;
            }
        }

        public bool UpdateProduct(ProductModel productModel)
        {
            productModel.CategoryIds = productModel.CategoryIds ?? new List<int>();
            using (var context = _factory.GetContext())
            {
                //check that Product with given Id is already exists
                var existingProduct = GetProduct(productModel.Id, context);
                context.Entry(existingProduct).State = EntityState.Detached;
                //create new entity from model
                var product = productModel.ToEntity();

                CheckIfAllCategoriesExist(productModel.CategoryIds, context);

                var existingCategoriesIds = context.CategoryProducts.Where(m => m.ProductId == productModel.Id).Select(m => m.CategoryId);
                var categoriesIdsToDelete = existingCategoriesIds.Where(m => !productModel.CategoryIds.Contains(m));
                var categoriesIdsToAdd = productModel.CategoryIds.Where(m => !existingCategoriesIds.Contains(m));

                DeleteCategoriesFromProduct(product.Id, categoriesIdsToDelete, context);
                AddCategoriesToProduct(product.Id, categoriesIdsToAdd, context);
                context.SaveChanges();

                context.Products.Update(product);

                context.SaveChanges();

                return true;
            }
        }

        public bool DeleteProduct(int id)
        {
            using (var context = _factory.GetContext())
            {
                var product = GetProduct(id, context);

                DeleteCategoriesFromProduct(id, product.CategoryProducts.Select(m => m.CategoryId), context);

                context.Products.Remove(product);
                
                context.SaveChanges();
                
                return true;
            }
        }

        public List<ProductModel> GetAllProductsData()
        {
            using (var context = _factory.GetContext())
            {
                return context.Products.Include(m => m.CategoryProducts).Select(m => ProductModel.CreateFromEntity(m)).ToList();
            }
        }

        public ProductModel GetProductData(int id)
        {
            using (var context = _factory.GetContext())
            {
                var product = GetProduct(id, context);
                return ProductModel.CreateFromEntity(product);
            }
        }

        private void CheckIfAllCategoriesExist(IEnumerable<int> categoryIds, MainDbContext context)
        {
            foreach (var categoryId in categoryIds)
            {
                var categoryExists = context.Categories.FirstOrDefault(n => n.Id == categoryId) != null;
                if (!categoryExists)
                {
                    throw new NotFoundException($"Category with id {categoryId} not found");
                }
            }
        }

        private void DeleteCategoriesFromProduct(int productId, IEnumerable<int> categoriesIds, MainDbContext context)
        {
            foreach (var categoryId in categoriesIds)
            {
                var categoryProduct = context.CategoryProducts.FirstOrDefault(m => m.CategoryId == categoryId && m.ProductId == productId);
                context.CategoryProducts.Remove(categoryProduct);
            }
        }

        private void AddCategoriesToProduct(int productId, IEnumerable<int> categoriesIds, MainDbContext context)
        {
            foreach (var categoryId in categoriesIds)
            {
                var categoryProduct = new CategoryProduct()
                {
                    CategoryId = categoryId,
                    ProductId = productId,
                };
                context.CategoryProducts.Add(categoryProduct);
            }
        }

        private Product GetProduct(int id, MainDbContext context)
        {
            var product = context.Products.Include(m => m.CategoryProducts).FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                throw new NotFoundException($"Product with id {id} not found");
            }

            return product;
        }
    }
}