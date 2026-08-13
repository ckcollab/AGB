using System;
using System.Collections.Generic;

namespace AGB.Collections;

public class ThreadSafeList<T>
{
	protected object PadLock = new object();

	protected List<T> ItemList;

	public int Count => ItemList.Count;

	public ThreadSafeList()
	{
		ItemList = new List<T>();
	}

	public void Add(T item)
	{
		lock (PadLock)
		{
			ItemList.Add(item);
		}
	}

	public void Remove(T item)
	{
		lock (PadLock)
		{
			ItemList.Remove(item);
		}
	}

	public T GetAt(int i)
	{
		lock (PadLock)
		{
			return ItemList[i];
		}
	}

	public T Find(Predicate<T> predicate)
	{
		lock (PadLock)
		{
			return ItemList.Find(predicate);
		}
	}

	public List<T> FindAll(Predicate<T> predicate)
	{
		lock (PadLock)
		{
			return ItemList.FindAll(predicate);
		}
	}

	public List<T> GetCopy()
	{
		return new List<T>(ItemList);
	}

	public void Clear()
	{
		lock (PadLock)
		{
			ItemList.Clear();
		}
	}
}
