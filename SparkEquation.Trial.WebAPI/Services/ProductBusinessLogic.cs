using SparkEquation.Trial.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkEquation.Trial.WebAPI.Services
{
    public class ProductBusinessLogic : IProductBusinessLogic
    {
        public void ApplyBusinessRules(ProductModel model)
        {
            if (model.Rating > 8)
            {
                model.Featured = true;
            }
        }
    }
}
