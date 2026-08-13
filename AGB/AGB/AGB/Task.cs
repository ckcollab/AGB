using System.Threading;

namespace AGB;

public class Task
{
	public TaskDelegate TaskDelegate;

	public int Priority;

	public string Description;

	public AutoResetEvent IsFinished;

	public Task(int priority, string description, TaskDelegate taskDelegate)
	{
		TaskDelegate = taskDelegate;
		Priority = priority;
		Description = description;
		IsFinished = new AutoResetEvent(initialState: false);
	}

	public void Go()
	{
		TaskDelegate();
		IsFinished.Set();
	}
}
