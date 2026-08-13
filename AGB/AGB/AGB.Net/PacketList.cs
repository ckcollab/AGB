using System.Collections.Generic;

namespace AGB.Net;

public class PacketList
{
	private Queue<byte[]> PacketQueue;

	public int Count
	{
		get
		{
			lock (PacketQueue)
			{
				return PacketQueue.Count;
			}
		}
	}

	public PacketList()
	{
		PacketQueue = new Queue<byte[]>();
	}

	public void Enqueue(byte[] item)
	{
		lock (PacketQueue)
		{
			PacketQueue.Enqueue(item);
		}
	}

	public byte[] Dequeue()
	{
		lock (PacketQueue)
		{
			return PacketQueue.Dequeue();
		}
	}
}
