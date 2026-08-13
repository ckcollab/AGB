using System.Collections.Generic;

namespace AGB.Collections;

public class PriorityQueueB<T> : IPriorityQueue<T>
{
	protected List<T> InnerList = new List<T>();

	protected IComparer<T> mComparer;

	public int Count => InnerList.Count;

	public T this[int index]
	{
		get
		{
			return InnerList[index];
		}
		set
		{
			InnerList[index] = value;
			Update(index);
		}
	}

	public PriorityQueueB()
	{
		mComparer = Comparer<T>.Default;
	}

	public PriorityQueueB(IComparer<T> comparer)
	{
		mComparer = comparer;
	}

	public PriorityQueueB(IComparer<T> comparer, int capacity)
	{
		mComparer = comparer;
		InnerList.Capacity = capacity;
	}

	protected void SwitchElements(int i, int j)
	{
		T h = InnerList[i];
		InnerList[i] = InnerList[j];
		InnerList[j] = h;
	}

	protected virtual int OnCompare(int i, int j)
	{
		return mComparer.Compare(InnerList[i], InnerList[j]);
	}

	public int Push(T item)
	{
		int p = InnerList.Count;
		InnerList.Add(item);
		while (p != 0)
		{
			int p2 = (p - 1) / 2;
			if (OnCompare(p, p2) <= 0)
			{
				SwitchElements(p, p2);
				p = p2;
				bool flag = true;
				continue;
			}
			break;
		}
		return p;
	}

	public T Pop()
	{
		T result = InnerList[0];
		int p = 0;
		InnerList[0] = InnerList[InnerList.Count - 1];
		InnerList.RemoveAt(InnerList.Count - 1);
		while (true)
		{
			int pn = p;
			int p2 = 2 * p + 1;
			int p3 = 2 * p + 2;
			if (InnerList.Count > p2 && OnCompare(p, p2) > 0)
			{
				p = p2;
			}
			if (InnerList.Count > p3 && OnCompare(p, p3) > 0)
			{
				p = p3;
			}
			if (p == pn)
			{
				break;
			}
			SwitchElements(p, pn);
			bool flag = true;
		}
		return result;
	}

	public void Update(int i)
	{
		int p = i;
		while (p != 0)
		{
			int p3 = (p - 1) / 2;
			if (OnCompare(p, p3) < 0)
			{
				SwitchElements(p, p3);
				p = p3;
				bool flag = true;
				continue;
			}
			break;
		}
		if (p < i)
		{
			return;
		}
		while (true)
		{
			int pn = p;
			int p2 = 2 * p + 1;
			int p3 = 2 * p + 2;
			if (InnerList.Count > p2 && OnCompare(p, p2) > 0)
			{
				p = p2;
			}
			if (InnerList.Count > p3 && OnCompare(p, p3) > 0)
			{
				p = p3;
			}
			if (p == pn)
			{
				break;
			}
			SwitchElements(p, pn);
			bool flag = true;
		}
	}

	public T Peek()
	{
		if (InnerList.Count > 0)
		{
			return InnerList[0];
		}
		return default(T);
	}

	public void Clear()
	{
		InnerList.Clear();
	}

	public void RemoveLocation(T item)
	{
		int index = -1;
		for (int i = 0; i < InnerList.Count; i++)
		{
			if (mComparer.Compare(InnerList[i], item) == 0)
			{
				index = i;
			}
		}
		if (index != -1)
		{
			InnerList.RemoveAt(index);
		}
	}

	public void Reverse()
	{
		InnerList.Reverse();
	}
}
