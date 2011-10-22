namespace BclExtensionMethods.Msmq
{
	using System;
	using System.Messaging;

	public static class MsmqHelper
	{
		/// <summary>
		/// 	Checks for one second to ensure the queue is empty
		/// </summary>
		public static bool IsQueueEmpty(string queuePath)
		{
			try
			{
				var queue = new MessageQueue(queuePath, QueueAccessMode.Peek);
				queue.Peek(TimeSpan.FromSeconds(1));
			}
			catch (MessageQueueException exception)
			{
				if (exception.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
				{
					return true;
				}
				throw;
			}
			return false;
		}
	}
}