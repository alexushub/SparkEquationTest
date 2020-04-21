using FluentValidation;
using SparkEquation.Trial.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkEquation.Trial.WebAPI.Validators
{
    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
		public ProductModelValidator()
		{
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.CategoryIds).Must(m => m != null && m.Count > 0 && m.Count < 6).WithMessage("Product can have from 1 to 5 categories");
			RuleFor(x => x.BrandId).NotEqual(0);
			RuleFor(x => x.ExpirationDate).Must(m => m.Value > DateTime.Now.AddDays(30)).When(x => x.ExpirationDate.HasValue);
		}
    }
}

