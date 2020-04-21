using SparkEquation.Trial.WebAPI.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SparkEquation.Trial.UnitTests.Common
{
    public static class TestHelper
    {
        public static MainDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "MainDatabase" + new Random().Next(1000))
            .Options;

            var context = new MainDbContext(options);

            return context;
        }
    }
}
