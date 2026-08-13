using System.Collections.Generic;

namespace AGB.Collections;

public class ThreadSafeQueue<T>
{
	protected object PadLock = new object();

	protected Queue<T> ItemList;

	public int Count => ItemList.Count;

	public ThreadSafeQueue()
	{
		ItemList = new Queue<T>();
	}

	public void Enqueue(T item)
	{
		lock (PadLock)
		{
			ItemList.Enqueue(item);
		}
	}

	public T Dequeue()
	{
		lock (PadLock)
		{
			return ItemList.Dequeue();
		}
	}

	public void Clear()
	{
		lock (PadLock)
		{
			ItemList.Clear();
		}
	}
}
