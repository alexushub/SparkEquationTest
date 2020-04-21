using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SparkEquation.Trial.WebAPI.Data.Models;
using SparkEquation.Trial.WebAPI.Exceptions;
using SparkEquation.Trial.WebAPI.Models;
using SparkEquation.Trial.WebAPI.Services;
using SparkEquation.Trial.WebAPI.Validators;

namespace SparkEquation.Trial.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly IProductBusinessLogic _productBusinessLogic;

        public ProductController(IProductsService productsService, IProductBusinessLogic productBusinessLogic)
        {
            _productsService = productsService;
            _productBusinessLogic = productBusinessLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = _productsService.GetAllProductsData();
            return new JsonResult(results);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var results = _productsService.GetProductData(id);
                return new JsonResult(results);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var results = _productsService.DeleteProduct(id);

                return new JsonResult(results);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel model)
        {
            _productBusinessLogic.ApplyBusinessRules(model);

            try
            {
                var results = _productsService.CreateProduct(model);

                return new JsonResult(results);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductModel model)
        {
            _productBusinessLogic.ApplyBusinessRules(model);

            try
            {
                var results = _productsService.UpdateProduct(model);

                return new JsonResult(results);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}