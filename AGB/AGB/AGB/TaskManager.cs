using System.Threading;

namespace AGB;

public class TaskManager
{
	private TaskQueue Queue;

	private Thread TaskThread;

	private object PadLock = new object();

	private bool IsSuspended = false;

	public event TaskEvent TaskAdded;

	public event TaskEvent TaskBeganExecuting;

	public event TaskEvent TaskExecuted;

	public event TaskExceptionEvent TaskException;

	public TaskManager()
	{
		Queue = new TaskQueue();
		Reset();
	}

	public void AddTask(int priority, string description, TaskDelegate del)
	{
		AddTask(new Task(priority, description, del));
	}

	public void AddTask(Task task)
	{
		if (task == null)
		{
			return;
		}
		lock (PadLock)
		{
			Queue.Enqueue(task);
			if (this.TaskAdded != null)
			{
				this.TaskAdded(task);
			}
		}
	}

	public void Reset()
	{
		Reset(null);
	}

	public void Reset(Task task)
	{
		Thread destroyThread = new Thread((ThreadStart)delegate
		{
			if (TaskThread != null)
			{
				TaskThread.Abort();
			}
			Queue.Clear();
			TaskThread = new Thread(TaskLoop);
			TaskThread.Start();
			AddTask(task);
		});
		destroyThread.Start();
	}

	public void Clear()
	{
		Queue.Clear();
	}

	public void Suspend()
	{
		IsSuspended = true;
	}

	public void Unsuspend()
	{
		IsSuspended = false;
	}

	private void TaskLoop()
	{
		while (true)
		{
			bool flag = true;
			Task task;
			if (!Queue.IsEmpty())
			{
				lock (PadLock)
				{
					if (IsSuspended && Queue.Peek().Priority != int.MaxValue)
					{
						continue;
					}
					task = Queue.Dequeue();
					goto IL_006d;
				}
			}
			goto IL_00cf;
			IL_00cf:
			Thread.Sleep(1);
			continue;
			IL_006d:
			try
			{
				if (this.TaskBeganExecuting != null)
				{
					this.TaskBeganExecuting(task);
				}
				task.Go();
				if (this.TaskExecuted != null)
				{
					this.TaskExecuted(task);
				}
			}
			catch (TaskException e)
			{
				if (this.TaskException != null)
				{
					this.TaskException(task, e);
				}
			}
			goto IL_00cf;
		}
	}

	~TaskManager()
	{
		if (TaskThread != null)
		{
			TaskThread.Abort();
		}
	}
}
