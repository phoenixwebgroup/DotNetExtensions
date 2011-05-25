namespace MvcActionFilers.Controllers
{
	using System.Web.Mvc;

	public class OtherController : Controller
	{
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