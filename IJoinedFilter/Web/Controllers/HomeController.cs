namespace MvcActionFilers.Controllers
{
	using System.Web.Mvc;

	public class HomeController : Controller
	{
		public ActionResult About()
		{
			var data = new
			           {
			           	message = "about"
			           };

			return Json(data);
		}

		public ActionResult World()
		{
			var data = new
			           {
			           	message = "world"
			           };

			return Json(data);
		}
	}
}