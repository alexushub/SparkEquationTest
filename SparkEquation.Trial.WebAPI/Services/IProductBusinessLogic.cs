using SparkEquation.Trial.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkEquation.Trial.WebAPI.Services
{
    public interface IProductBusinessLogic
    {
        void ApplyBusinessRules(ProductModel model);
    }
}
