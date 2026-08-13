using System;
using System.Threading;

namespace AGB.Net;

public class PacketHandler<T>
{
	private PacketEvent<T>[] AsyncPacketHandlers;

	private AutoResetEvent[] PacketWaiters;

	private int[] PacketWaitersCount;

	private T[] Packets;

	private PacketList PacketBuffer;

	public int PacketCount;

	public PacketHandler(int packetCount)
	{
		PacketCount = packetCount;
		AsyncPacketHandlers = new PacketEvent<T>[packetCount];
		PacketWaiters = new AutoResetEvent[packetCount];
		PacketWaitersCount = new int[packetCount];
		Packets = new T[packetCount];
		PacketBuffer = new PacketList();
	}

	public void AddPacket(int packetId, object sender, T packet)
	{
		if (packetId > PacketCount)
		{
			throw new ArgumentException("PacketID greater than packet count -- check your packet enum size?");
		}
		if (AsyncPacketHandlers[packetId] != null)
		{
			AsyncHelper.FireAsync(AsyncPacketHandlers[packetId], sender, packet);
		}
		if (PacketWaitersCount[packetId] > 0)
		{
			Packets[packetId] = packet;
			PacketWaiters[packetId].Set();
			PacketWaitersCount[packetId] = 0;
		}
	}

	public void AddAsyncListener(int packetId, PacketEvent<T> aDelegate)
	{
		if (packetId >= PacketCount)
		{
			throw new ArgumentException("PacketID greater than packet count -- check your packet enum size?");
		}
		PacketEvent<T>[] asyncPacketHandlers;
		PacketEvent<T>[] array = (asyncPacketHandlers = AsyncPacketHandlers);
		nint num = packetId;
		array[packetId] = (PacketEvent<T>)Delegate.Combine(asyncPacketHandlers[num], aDelegate);
	}

	public void RemoveAsyncListener(int packetId, PacketEvent<T> aDelegate)
	{
		if (packetId >= PacketCount)
		{
			throw new ArgumentException("PacketID greater than packet count -- check your packet enum size?");
		}
		PacketEvent<T>[] asyncPacketHandlers;
		PacketEvent<T>[] array = (asyncPacketHandlers = AsyncPacketHandlers);
		nint num = packetId;
		array[packetId] = (PacketEvent<T>)Delegate.Remove(asyncPacketHandlers[num], aDelegate);
	}

	public T WaitForPacket(int packetId, int timeout)
	{
		bool flag = false;
		PacketWaitersCount[packetId]++;
		if (PacketWaiters[packetId] == null)
		{
			PacketWaiters[packetId] = new AutoResetEvent(initialState: false);
		}
		if ((timeout <= 0) ? PacketWaiters[packetId].WaitOne() : PacketWaiters[packetId].WaitOne(timeout, exitContext: false))
		{
			return Packets[packetId];
		}
		PacketWaitersCount[packetId]--;
		return default(T);
	}
}
