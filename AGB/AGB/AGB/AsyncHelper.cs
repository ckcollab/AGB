using System;
using System.Threading;

namespace AGB;

public class AsyncHelper
{
	private class Target
	{
		private readonly Delegate TargetDelegate;

		private readonly object[] Args;

		public Target(Delegate d, object[] args)
		{
			TargetDelegate = d;
			Args = args;
		}

		public void ExecuteDelegate(object o)
		{
			TargetDelegate.DynamicInvoke(Args);
		}
	}

	private static void FireAndForget(Delegate d, params object[] args)
	{
		Target target = new Target(d, args);
		ThreadPool.QueueUserWorkItem(target.ExecuteDelegate);
	}

	public static void FireAsync(Delegate del, params object[] args)
	{
		if ((object)del != null)
		{
			Delegate[] delegates = del.GetInvocationList();
			Delegate[] array = delegates;
			foreach (Delegate receiver in array)
			{
				FireAndForget(receiver, args);
			}
		}
	}
}
