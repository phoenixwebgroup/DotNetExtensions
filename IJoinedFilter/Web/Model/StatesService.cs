namespace MvcActionFilers.Model
{
	using System.Collections.Generic;

	public class StatesService
	{
		public static IList<State> GetStates()
		{
			return new List<State>
			       {
			       	new State {Name = "Nebraska", Id = 1},
			       	new State {Name = "Iowa", Id = 2},
			       	new State {Name = "Kansas", Id = 3}
			       };
		}
	}
}