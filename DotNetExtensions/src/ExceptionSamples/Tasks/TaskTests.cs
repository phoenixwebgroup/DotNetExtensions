namespace DotNetExtensions.Services.Tasks
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using NUnit.Framework;

	[TestFixture]
	public class TaskTests : AssertionHelper
	{
		[SetUp]
		public void Setup()
		{
			GlobalTaskContext.ClearGlobalHanlders();
		}

		[Test]
		[ExpectedException(typeof(AggregateException))]
		public void Exception_WithNoHandlers_IsNotCaptured()
		{
			Action exceptionalAction = () => { throw new ArgumentException(); };

			new TaskContext("Exceptional Action")
				.Start(exceptionalAction)
				.Wait();
		}

		[Test]
		public void Exception_WithGlobalHandler_IsCaptured()
		{
			var errors = new List<ErrorEvent>();
			Action exceptionalAction = () => { throw new ArgumentException(); };
			GlobalTaskContext.RegisterGlobalHandler(errors.Add);
			var state = "Exceptional Action";
			var task = new TaskContext(state)
				.Start(exceptionalAction);

			task.Wait();

			Expect(errors.Count, Is.EqualTo(1));
			var errorEvent = errors.Single();
			Expect(errorEvent.Exception, Is.TypeOf<ArgumentException>());
			Expect(errorEvent.Context.State, Is.EqualTo(state));
		}

		[Test]
		public void Exception_WithLocalHandler_IsCaptured()
		{
			var errors = new List<ErrorEvent>();
			Action exceptionalAction = () => { throw new ArgumentException(); };
			var state = "Exceptional Action";
			var task = new TaskContext(state)
				.OnError(errors.Add)
				.Start(exceptionalAction);

			task.Wait();

			Expect(errors.Count, Is.EqualTo(1));
			var errorEvent = errors.Single();
			Expect(errorEvent.Exception, Is.TypeOf<ArgumentException>());
			Expect(errorEvent.Context.State, Is.EqualTo(state));
			
		}

		[Test]
		public void EnumerateUpTheContextChain_TwoLevels_EnumeratesBoth()
		{
			var nestedState = "Nested";
			var mainState = "Main";
			List<TaskContext> Contexts = null;
			Action nestedAction = () => Contexts = TaskContexts.GetCurrentTaskContext().EnumerateUpTheContextChain().ToList();
			Action mainAction = () => new TaskContext(nestedState)
			                            	.Start(nestedAction)
			                            	.Wait();

			new TaskContext(mainState)
				.Start(mainAction)
				.Wait();

			Expect(Contexts.Count, Is.EqualTo(2));
			var nestedContext = Contexts.First();
			Expect(nestedContext.State, Is.EqualTo(nestedState));
			var mainContext = Contexts.Last();
			Expect(mainContext.State, Is.EqualTo(mainState));
		}

		[Test]
		public void ChildError_OnErrorNotifyParents_NotifiesParent()
		{
			var nestedState = "Nested";
			var mainState = "Main";
			Action nestedAction = () => { throw new ArgumentException(); };
			Action mainAction = () => new TaskContext(nestedState)
			                          	.NotifyParentsOfErrors()
			                          	.Start(nestedAction)
			                          	.Wait();
			ErrorEvent childError = null;

			var mainContext = new TaskContext(mainState)
				.OnChildError(e => childError = e);
			mainContext
				.Start(mainAction)
				.Wait();

			Expect(childError, Is.Not.Null);
			Expect(childError.Context.State, Is.EqualTo(nestedState));
			Expect(childError.Exception, Is.TypeOf<ArgumentException>());
			Expect(mainContext.ChildErrors, Has.Member(childError));
		}

		[Test]
		public void ChildError_OnErrorNotifyAllParents_NotifiesAllParents()
		{
			var doubleNestedState = "Double";
			var nestedState = "Nested";
			var mainState = "Main";
			Action doubleNestedAction = () => { throw new ArgumentException(); };
			Action nestedAction = () => new TaskContext(doubleNestedState)
										.NotifyAllParentsOfErrors()
										.Start(doubleNestedAction)
										.Wait();

			ErrorEvent childErrorNotificationInNested = null;
			Action mainAction = () => new TaskContext(nestedState)
										.OnChildError(e => childErrorNotificationInNested = e)
										.Start(nestedAction)
										.Wait();
			ErrorEvent childErrorNotificationInMain = null;

			new TaskContext(mainState)
				.OnChildError(e => childErrorNotificationInMain = e)
				.Start(mainAction)
				.Wait();

			Expect(childErrorNotificationInMain, Is.EqualTo(childErrorNotificationInNested));
			Expect(childErrorNotificationInMain, Is.Not.Null);
			Expect(childErrorNotificationInMain.Context.State, Is.EqualTo(doubleNestedState));
			Expect(childErrorNotificationInMain.Exception, Is.TypeOf<ArgumentException>());
		}

		// do we want to just provide notify all parents, or do we need single notify one up and then some mechanism to forward, i feel this is too complex and so notify all parents probably captures our intent better and let the parent filter out what happened
		// create task, start self -> one test of this usage

		// should child errors be treated differently or just reported up the chain as regular errors?
		// template to wrap a child error easily?  (say unhandled exception in m2m trade, wrap in TradeM2MError(unhandledexception)?

		// tests of task  contexts??? a better name for this?

		// build up typical behaviors to easily plugin globally, maybe even have a registry concept for global registrations

		// robustness concerns, like to stringing things and what not in TaskContext, review and at least add, even if without any tests
	}
}