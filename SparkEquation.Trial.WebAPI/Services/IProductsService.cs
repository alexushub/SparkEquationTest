using System.Collections.Generic;
using SparkEquation.Trial.WebAPI.Data.Models;
using SparkEquation.Trial.WebAPI.Models;

namespace SparkEquation.Trial.WebAPI.Services
{
    public interface IProductsService
    {
        List<ProductModel> GetAllProductsData();

        ProductModel GetProductData(int id);

        bool DeleteProduct(int id);

        bool UpdateProduct(ProductModel product);

        int CreateProduct(ProductModel product);
    }
}