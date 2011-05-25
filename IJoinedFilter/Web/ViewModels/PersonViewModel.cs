namespace MvcActionFilers.ViewModels
{
	using System.Collections.Generic;
	using Model;

	public class PersonViewModel
	{
		public string Name { get; set; }
		public string BirthDate { get; set; }
		public string StateId { get; set; }
		public IList<State> AllStates { get; set; }
	}
}