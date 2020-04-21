using SparkEquation.Trial.WebAPI.Models;
using SparkEquation.Trial.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SparkEquation.Trial.UnitTests.Controllers.BusinessLogic
{
    
    public class ProductBusinessLogicTest
    {
        [Fact]
        public void ProductBusinessLogic_ApplyBusinessRuleTest()
        {
            var model1  = new ProductModel()
            {
                Rating = 5,
                Featured = false,
            };

            var model2  = new ProductModel()
            {
                Rating = 9,
                Featured = false,
            };
            var bl = new ProductBusinessLogic();

            bl.ApplyBusinessRules(model1);
            bl.ApplyBusinessRules(model2);

            Assert.False(model1.Featured);
            Assert.True(model2.Featured);
        }
    }
}
