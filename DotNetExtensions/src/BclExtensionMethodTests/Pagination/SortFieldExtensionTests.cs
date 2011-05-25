namespace BclExtensionMethodTests.Pagination
{
	using System.Linq;
	using BclExtensionMethods.Pagination;
	using NUnit.Framework;

	[TestFixture]
	public class SortFieldExtensionTests : AssertionHelper
	{
		private class Person
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
		}

		[Test]
		public void Sort_MultipleSort_Sorts()
		{
			var people = new[]
			             	{
			             		new Person {FirstName = "Jane", LastName = "Moe"},
			             		new Person {FirstName = "Bob", LastName = "Doe"},
			             		new Person {FirstName = "Jane", LastName = "Doe"},
			             		new Person {FirstName = "Dan", LastName = "Doe"}
			             	};
			var sortByFirstName = new SortField("FirstName", SortField.SortDirectionType.ASC);
			var sortByLastName = new SortField("LastName", SortField.SortDirectionType.ASC);

			var peopleSortedByFirstThenLastName = people.AsQueryable().SortBySortField(sortByFirstName, sortByLastName);

			var lastPerson = peopleSortedByFirstThenLastName.Last();
			Expect(lastPerson.FirstName == "Jane");
			Expect(lastPerson.LastName == "Moe");
		}

		[Test]
		public void Sort_Descending_SortsDescending()
		{
			var people = new[]
			             	{
			             		new Person {FirstName = "Bob"},
			             		new Person {FirstName = "Jane"},
			             		new Person {FirstName = "Dan"}
			             	};
			var firstNameDescending = new SortField("FirstName", SortField.SortDirectionType.DESC);

			var peopleSortedByFirstNameDescending = people.AsQueryable().SortBySortField(firstNameDescending);

			var firstPerson = peopleSortedByFirstNameDescending.First();
			Expect(firstPerson.FirstName == "Jane");
		}

		[Test]
		[ExpectedException(typeof (InvalidSortException))]
		public void Sort_InvalidSort_ThrowsException()
		{
			var people = new Person[0];
			var invalidSort = new SortField("First", SortField.SortDirectionType.ASC);

			people.AsQueryable().SortBySortField(invalidSort);
		}
	}
}