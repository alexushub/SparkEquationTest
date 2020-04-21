using SparkEquation.Trial.WebAPI.Data;
using SparkEquation.Trial.WebAPI.Data.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SparkEquation.Trial.UnitTests.Controllers.Factories
{
    public class ContextFactoryTest
    {
        [Fact]
        public void ContextFactory_GetContextTest()
        {
            var factory = new ContextFactory();
            var context = factory.GetContext();

            Assert.IsType<MainDbContext>(context);
        }
    }
}
