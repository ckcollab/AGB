using System.Collections.Generic;

namespace AGB.Net;

public class PacketBuffer
{
	private object PadLock = new object();

	private Queue<byte> ByteQueue;

	public int Count
	{
		get
		{
			lock (PadLock)
			{
				return ByteQueue.Count;
			}
		}
	}

	public PacketBuffer()
	{
		ByteQueue = new Queue<byte>();
	}

	public byte Peek()
	{
		lock (PadLock)
		{
			if (ByteQueue.Count == 0)
			{
				return 0;
			}
			return ByteQueue.Peek();
		}
	}

	public byte[] ToArray()
	{
		lock (PadLock)
		{
			return ByteQueue.ToArray();
		}
	}

	public void Enqueue(byte data)
	{
		lock (PadLock)
		{
			ByteQueue.Enqueue(data);
		}
	}

	public void Enqueue(byte[] data)
	{
		Enqueue(data, data.Length);
	}

	public void Enqueue(byte[] data, int length)
	{
		Enqueue(data, 0, length);
	}

	public void Enqueue(byte[] data, int offset, int length)
	{
		lock (PadLock)
		{
			for (int i = offset; i < length; i++)
			{
				ByteQueue.Enqueue(data[i]);
			}
		}
	}

	public byte Dequeue()
	{
		lock (PadLock)
		{
			return ByteQueue.Dequeue();
		}
	}

	public byte[] Dequeue(int length)
	{
		lock (PadLock)
		{
			byte[] tmpBuf = new byte[length];
			for (int i = 0; i < length; i++)
			{
				tmpBuf[i] = ByteQueue.Dequeue();
			}
			return tmpBuf;
		}
	}

	public void Clear()
	{
		lock (PadLock)
		{
			ByteQueue.Clear();
		}
	}
}
