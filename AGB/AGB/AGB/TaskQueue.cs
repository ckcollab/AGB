namespace AGB;

internal class TaskQueue
{
	private TaskQueueNode Head;

	public void Enqueue(Task task)
	{
		if (IsEmpty())
		{
			Head = new TaskQueueNode
			{
				Value = task
			};
			return;
		}
		TaskQueueNode node = Head;
		TaskQueueNode previous = null;
		while (node.Next != null && node.Value.Priority >= task.Priority)
		{
			previous = node;
			node = node.Next;
		}
		TaskQueueNode taskQueueNode = new TaskQueueNode();
		taskQueueNode.Value = task;
		TaskQueueNode newNode = taskQueueNode;
		if (node.Value.Priority < task.Priority)
		{
			newNode.Next = node;
			if (Head == node)
			{
				Head = newNode;
			}
			else
			{
				previous.Next = newNode;
			}
		}
		else
		{
			newNode.Next = node.Next;
			node.Next = newNode;
		}
	}

	public Task Dequeue()
	{
		Task task = Head.Value;
		Head = Head.Next;
		return task;
	}

	public bool IsEmpty()
	{
		return Head == null;
	}

	public void Clear()
	{
		Head = null;
	}

	public Task Peek()
	{
		return Head.Value;
	}
}
