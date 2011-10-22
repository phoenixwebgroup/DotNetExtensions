namespace BclExtensionMethodTests.Msmq
{
	using System.Messaging;
	using BclExtensionMethods.Msmq;
	using NUnit.Framework;

	[TestFixture]
	public class MsmqHelperTests : AssertionHelper
	{
		private string _QueueName;

		[SetUp]
		public void Setup()
		{
			_QueueName = @".\private$\TestQueue-FB4E2BFA-1AC0-4B59-9ABD-A5938625BC6A";
			SetupNewQueue(_QueueName);
		}

		[TearDown]
		public void Teardown()
		{
			DeleteQueueIfItExists(_QueueName);
		}

		[Test]
		public void IsQueueEmpty_EmptyQueue_ReturnsTrue()
		{
			var isEmpty = MsmqHelper.IsQueueEmpty(_QueueName);

			Expect(isEmpty);
		}

		[Test]
		public void IsQueueEmpty_NotEmptyQueue_ReturnsFalse()
		{
			var message = new Message();
			var queue = new MessageQueue(_QueueName);
			queue.Send(message);

			var isEmpty = MsmqHelper.IsQueueEmpty(_QueueName);

			Expect(isEmpty, Is.False);
		}

		[Test]
		[ExpectedException(typeof (MessageQueueException))]
		public void IsQueueEmpty_InvalidQueue_ThrowsException()
		{
			var invalidQueueName = @".\private$\invalidQueueName";

			MsmqHelper.IsQueueEmpty(invalidQueueName);
		}

		private void SetupNewQueue(string queueName)
		{
			DeleteQueueIfItExists(queueName);
			MessageQueue.Create(queueName);
		}

		private static void DeleteQueueIfItExists(string queueName)
		{
			if (MessageQueue.Exists(queueName))
			{
				MessageQueue.Delete(queueName);
			}
		}
	}
}