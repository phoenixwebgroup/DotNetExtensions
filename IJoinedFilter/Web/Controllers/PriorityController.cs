namespace MvcActionFilers.Controllers
{
	using System;
	using System.Web.Mvc;
	using AutoMapper;
	using Model;
	using ViewModels;

	public class PriorityController : Controller
	{
		static PriorityController()
		{
			Mapper.CreateMap<Person, PersonViewModel>();
		}

		public ActionResult Injected()
		{
			var person = new Person
			             {
			             	Name = "John Doe",
			             	BirthDate = new DateTime(1980, 1, 1),
			             	State = StatesService.GetStates()[1]
			             };

			return View(person);
		}

		public void NestedException()
		{
			throw new NestedException();
		}

		public void ExceptionBase()
		{
			throw new ExceptionBase();
		}
	}
}