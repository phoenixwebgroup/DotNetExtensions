namespace MvcActionFilers.Controllers
{
	using System.Web.Mvc;
	using AutoMapper;
	using Filters;
	using ReflectedAutoMap;

	public class AutoMappedController : Controller
	{
		static AutoMappedController()
		{
			Mapper.CreateMap<Product, ProductViewModel>();
			Mapper.CreateMap<Product, OtherProductViewModel>();
		}

		public ActionResult Product()
		{
			var product = new Product
			              {
			              	Id = 1,
			              	Name = "Product from joined, reflected auto map filter"
			              };
			return View(product);
		}

		[AutoMap(typeof (Product), typeof (ProductViewModel))]
		public ActionResult ProductAutoMapFilter()
		{
			var product = new Product
			{
				Id = 1,
				Name = "Product auto map filter"
			};
			return View("Product",product);
		}

		[ReflectedAutoMap]
		public ActionResult ProductReflectedAutoMapFilter()
		{
			var product = new Product
			{
				Id = 1,
				Name = "Product reflected auto map filter"
			};
			return View("Product", product);
		}

		//Old way: [AutoMap(typeof(Product), typeof(OtherProductViewModel))]
		public ViewResult OtherProduct()
		{
			var product = new Product
			              {
			              	Id = 2,
			              	Name = "Product2"
			              };
			return View(product);
		}

		public PartialViewResult PartialProduct()
		{
			var product = new Product
			              {
			              	Id = 3,
			              	Name = "PartialProduct"
			              };

			return PartialView(product);
		}

		public object PartialProductWithObjectReturnType()
		{
			var product = new Product
			              {
			              	Id = 4,
			              	Name = "PartialProductWithObjectReturnType"
			              };

			return PartialView("PartialProduct", product);
		}

		public ActionResult SparkProduct()
		{
			var product = new Product
			{
				Id = 5,
				Name = "SparkProduct"
			};
			return View(product);
		}
	}

	public class OtherProductViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class ProductViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}