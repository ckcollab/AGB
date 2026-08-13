using System;
using System.Threading;
using AGB.Net;
using D2Packets.BnetClient;
using D2Packets.BnetServer;
using D2Packets.D2Packets;
using D2Packets.GameClient;
using D2Packets.GameServer;
using D2Packets.RealmClient;
using D2Packets.RealmServer;

namespace AGB.D2.Net.Packets;

public class D2PacketHandler
{
	private readonly PacketEventHandler[] BCAsyncPacketHandlers = new PacketEventHandler[131];

	private readonly PacketEventHandler[] BSAsyncPacketHandlers = new PacketEventHandler[131];

	private readonly PacketEventHandler[] RCAsyncPacketHandlers = new PacketEventHandler[32];

	private readonly PacketEventHandler[] RSAsyncPacketHandlers = new PacketEventHandler[32];

	private readonly PacketEventHandler[] GCAsyncPacketHandlers = new PacketEventHandler[110];

	private readonly PacketEventHandler[] GSAsyncPacketHandlers = new PacketEventHandler[177];

	private readonly AutoResetEvent[] BCPacketWaiters = new AutoResetEvent[131];

	private readonly AutoResetEvent[] BSPacketWaiters = new AutoResetEvent[131];

	private readonly AutoResetEvent[] RCPacketWaiters = new AutoResetEvent[32];

	private readonly AutoResetEvent[] RSPacketWaiters = new AutoResetEvent[32];

	private readonly AutoResetEvent[] GCPacketWaiters = new AutoResetEvent[110];

	private readonly AutoResetEvent[] GSPacketWaiters = new AutoResetEvent[177];

	private readonly int[] BCPacketWaitersCount = new int[131];

	private readonly int[] BSPacketWaitersCount = new int[131];

	private readonly int[] RCPacketWaitersCount = new int[32];

	private readonly int[] RSPacketWaitersCount = new int[32];

	private readonly int[] GCPacketWaitersCount = new int[110];

	private readonly int[] GSPacketWaitersCount = new int[177];

	private readonly D2Packet[] BCPackets = new D2Packet[131];

	private readonly D2Packet[] BSPackets = new D2Packet[131];

	private readonly D2Packet[] RCPackets = new D2Packet[32];

	private readonly D2Packet[] RSPackets = new D2Packet[32];

	private readonly D2Packet[] GCPackets = new D2Packet[110];

	private readonly D2Packet[] GSPackets = new D2Packet[177];

	private readonly PacketList BCPacketBuffer = new PacketList();

	private readonly PacketList BSPacketBuffer = new PacketList();

	private readonly PacketList RCPacketBuffer = new PacketList();

	private readonly PacketList RSPacketBuffer = new PacketList();

	private readonly PacketList GCPacketBuffer = new PacketList();

	private readonly PacketList GSPacketBuffer = new PacketList();

	public D2PacketHandler()
	{
		Thread interpretThread = new Thread(InterpretThread);
		interpretThread.Start();
	}

	private void InterpretThread()
	{
		while (true)
		{
			bool flag = true;
			while (BCPacketBuffer.Count > 0)
			{
				InterpretPacket(PacketOrigin.BattleNetClient, BCPacketBuffer.Dequeue());
			}
			while (BSPacketBuffer.Count > 0)
			{
				InterpretPacket(PacketOrigin.BattleNetServer, BSPacketBuffer.Dequeue());
			}
			while (RCPacketBuffer.Count > 0)
			{
				InterpretPacket(PacketOrigin.RealmClient, RCPacketBuffer.Dequeue());
			}
			while (RSPacketBuffer.Count > 0)
			{
				InterpretPacket(PacketOrigin.RealmServer, RSPacketBuffer.Dequeue());
			}
			while (GCPacketBuffer.Count > 0)
			{
				InterpretPacket(PacketOrigin.GameClient, GCPacketBuffer.Dequeue());
			}
			while (GSPacketBuffer.Count > 0)
			{
				InterpretPacket(PacketOrigin.GameServer, GSPacketBuffer.Dequeue());
			}
			Thread.Sleep(1);
		}
	}

	public void AddAsyncListener(BnetClientPacket packet, PacketEventHandler aDelegate)
	{
		if (packet >= BnetClientPacket.Invalid)
		{
			throw new ArgumentException();
		}
		if (BCAsyncPacketHandlers != null)
		{
			PacketEventHandler[] bCAsyncPacketHandlers;
			PacketEventHandler[] array = (bCAsyncPacketHandlers = BCAsyncPacketHandlers);
			nint num = (nint)packet;
			array[(int)packet] = (PacketEventHandler)Delegate.Combine(bCAsyncPacketHandlers[num], aDelegate);
		}
	}

	public void RemoveAsyncListener(BnetClientPacket packet, PacketEventHandler aDelegate)
	{
		if (packet >= BnetClientPacket.Invalid)
		{
			throw new ArgumentException();
		}
		if (BCAsyncPacketHandlers != null)
		{
			PacketEventHandler[] bCAsyncPacketHandlers;
			PacketEventHandler[] array = (bCAsyncPacketHandlers = BCAsyncPacketHandlers);
			nint num = (nint)packet;
			array[(int)packet] = (PacketEventHandler)Delegate.Remove(bCAsyncPacketHandlers[num], aDelegate);
		}
	}

	public D2Packet WaitForPacket(BnetClientPacket packet, int timeout)
	{
		if (BCPacketWaitersCount != null)
		{
			BCPacketWaitersCount[(int)packet]++;
		}
		if (BCPacketWaiters[(int)packet] == null)
		{
			BCPacketWaiters[(int)packet] = new AutoResetEvent(initialState: false);
		}
		if ((timeout <= 0) ? BCPacketWaiters[(int)packet].WaitOne() : BCPacketWaiters[(int)packet].WaitOne(timeout, exitContext: false))
		{
			return BCPackets[(int)packet];
		}
		if (BCPacketWaitersCount != null)
		{
			BCPacketWaitersCount[(int)packet]--;
		}
		return null;
	}

	public void AddAsyncListener(BnetServerPacket packet, PacketEventHandler aDelegate)
	{
		if (packet >= BnetServerPacket.Invalid)
		{
			throw new ArgumentException();
		}
		PacketEventHandler[] bSAsyncPacketHandlers;
		PacketEventHandler[] array = (bSAsyncPacketHandlers = BSAsyncPacketHandlers);
		nint num = (nint)packet;
		array[(int)packet] = (PacketEventHandler)Delegate.Combine(bSAsyncPacketHandlers[num], aDelegate);
	}

	public void RemoveAsyncListener(BnetServerPacket packet, PacketEventHandler aDelegate)
	{
		if (packet >= BnetServerPacket.Invalid)
		{
			throw new ArgumentException();
		}
		PacketEventHandler[] bSAsyncPacketHandlers;
		PacketEventHandler[] array = (bSAsyncPacketHandlers = BSAsyncPacketHandlers);
		nint num = (nint)packet;
		array[(int)packet] = (PacketEventHandler)Delegate.Remove(bSAsyncPacketHandlers[num], aDelegate);
	}

	public D2Packet WaitForPacket(BnetServerPacket packet, int timeout)
	{
		BSPacketWaitersCount[(int)packet]++;
		if (BSPacketWaiters[(int)packet] == null)
		{
			BSPacketWaiters[(int)packet] = new AutoResetEvent(initialState: false);
		}
		if ((timeout <= 0) ? BSPacketWaiters[(int)packet].WaitOne() : BSPacketWaiters[(int)packet].WaitOne(timeout, exitContext: false))
		{
			return BSPackets[(int)packet];
		}
		BSPacketWaitersCount[(int)packet]--;
		return null;
	}

	public void AddAsyncListener(RealmClientPacket packet, PacketEventHandler aDelegate)
	{
		if (packet >= RealmClientPacket.Invalid)
		{
			throw new ArgumentException();
		}
		PacketEventHandler[] rCAsyncPacketHandlers;
		PacketEventHandler[] array = (rCAsyncPacketHandlers = RCAsyncPacketHandlers);
		nint num = (nint)packet;
		array[(int)packet] = (PacketEventHandler)Delegate.Combine(rCAsyncPacketHandlers[num], aDelegate);
	}

	public void RemoveAsyncListener(RealmClientPacket packet, PacketEventHandler aDelegate)
	{
		if (packet >= RealmClientPacket.Invalid)
		{
			throw new ArgumentException();
		}
		PacketEventHandler[] rCAsyncPacketHandlers;
		PacketEventHandler[] array = (rCAsyncPacketHandlers = RCAsyncPacketHandlers);
		nint num = (nint)packet;
		array[(int)packet] = (PacketEventHandler)Delegate.Remove(rCAsyncPacketHandlers[num], aDelegate);
	}

	public D2Packet WaitForPacket(RealmClientPacket packet, int timeout)
	{
		if (RCPacketWaitersCount != null)
		{
			RCPacketWaitersCount[(int)packet]++;
		}
		if (RCPacketWaiters[(int)packet] == null)
		{
			RCPacketWaiters[(int)packet] = new AutoResetEvent(initialState: false);
		}
		if ((timeout <= 0) ? RCPacketWaiters[(int)packet].WaitOne() : RCPacketWaiters[(int)packet].WaitOne(timeout, exitContext: false))
		{
			return RCPackets[(int)packet];
		}
		if (RCPacketWaitersCount != null)
		{
			RCPacketWaitersCount[(int)packet]--;
		}
		return null;
	}

	public void AddAsyncListener(RealmServerPacket packet, PacketEventHandler aDelegate)
	{
		if (packet >= RealmServerPacket.Invalid)
		{
			throw new ArgumentException();
		}
		PacketEventHandler[] rSAsyncPacketHandlers;
		PacketEventHandler[] array = (rSAsyncPacketHandlers = RSAsyncPacketHandlers);
		nint num = (nint)packet;
		array[(int)packet] = (PacketEventHandler)Delegate.Combine(rSAsyncPacketHandlers[num], aDelegate);
	}

	public void RemoveAsyncListener(RealmServerPacket packet, PacketEventHandler aDelegate)
	{
		if (packet >= RealmServerPacket.Invalid)
		{
			throw new ArgumentException();
		}
		PacketEventHandler[] rSAsyncPacketHandlers;
		PacketEventHandler[] array = (rSAsyncPacketHandlers = RSAsyncPacketHandlers);
		nint num = (nint)packet;
		array[(int)packet] = (PacketEventHandler)Delegate.Remove(rSAsyncPacketHandlers[num], aDelegate);
	}

	public D2Packet WaitForPacket(RealmServerPacket packet, int timeout)
	{
		RSPacketWaitersCount[(int)packet]++;
		if (RSPacketWaiters[(int)packet] == null)
		{
			RSPacketWaiters[(int)packet] = new AutoResetEvent(initialState: false);
		}
		if ((timeout <= 0) ? RSPacketWaiters[(int)packet].WaitOne() : RSPacketWaiters[(int)packet].WaitOne(timeout, exitContext: false))
		{
			return RSPackets[(int)packet];
		}
		RSPacketWaitersCount[(int)packet]--;
		return null;
	}

	public void AddAsyncListener(GameClientPacket packet, PacketEventHandler aDelegate)
	{
		if (packet >= GameClientPacket.Invalid)
		{
			throw new ArgumentException();
		}
		PacketEventHandler[] gCAsyncPacketHandlers;
		PacketEventHandler[] array = (gCAsyncPacketHandlers = GCAsyncPacketHandlers);
		nint num = (nint)packet;
		array[(int)packet] = (PacketEventHandler)Delegate.Combine(gCAsyncPacketHandlers[num], aDelegate);
	}

	public void RemoveAsyncListener(GameClientPacket packet, PacketEventHandler aDelegate)
	{
		if (packet >= GameClientPacket.Invalid)
		{
			throw new ArgumentException();
		}
		PacketEventHandler[] gCAsyncPacketHandlers;
		PacketEventHandler[] array = (gCAsyncPacketHandlers = GCAsyncPacketHandlers);
		nint num = (nint)packet;
		array[(int)packet] = (PacketEventHandler)Delegate.Remove(gCAsyncPacketHandlers[num], aDelegate);
	}

	public D2Packet WaitForPacket(GameClientPacket packet, int timeout)
	{
		GCPacketWaitersCount[(int)packet]++;
		if (GCPacketWaiters[(int)packet] == null)
		{
			GCPacketWaiters[(int)packet] = new AutoResetEvent(initialState: false);
		}
		if ((timeout <= 0) ? GCPacketWaiters[(int)packet].WaitOne() : GCPacketWaiters[(int)packet].WaitOne(timeout, exitContext: false))
		{
			return GCPackets[(int)packet];
		}
		GCPacketWaitersCount[(int)packet]--;
		return null;
	}

	public void AddAsyncListener(GameServerPacket packet, PacketEventHandler aDelegate)
	{
		if (packet >= GameServerPacket.Invalid)
		{
			throw new ArgumentException();
		}
		PacketEventHandler[] gSAsyncPacketHandlers;
		PacketEventHandler[] array = (gSAsyncPacketHandlers = GSAsyncPacketHandlers);
		nint num = (nint)packet;
		array[(int)packet] = (PacketEventHandler)Delegate.Combine(gSAsyncPacketHandlers[num], aDelegate);
	}

	public void RemoveAsyncListener(GameServerPacket packet, PacketEventHandler aDelegate)
	{
		if (packet >= GameServerPacket.Invalid)
		{
			throw new ArgumentException();
		}
		PacketEventHandler[] gSAsyncPacketHandlers;
		PacketEventHandler[] array = (gSAsyncPacketHandlers = GSAsyncPacketHandlers);
		nint num = (nint)packet;
		array[(int)packet] = (PacketEventHandler)Delegate.Remove(gSAsyncPacketHandlers[num], aDelegate);
	}

	public D2Packet WaitForPacket(GameServerPacket packet, int timeout)
	{
		GSPacketWaitersCount[(int)packet]++;
		if (GSPacketWaiters[(int)packet] == null)
		{
			GSPacketWaiters[(int)packet] = new AutoResetEvent(initialState: false);
		}
		if ((timeout <= 0) ? GSPacketWaiters[(int)packet].WaitOne() : GSPacketWaiters[(int)packet].WaitOne(timeout, exitContext: false))
		{
			return GSPackets[(int)packet];
		}
		GSPacketWaitersCount[(int)packet] = 0;
		return null;
	}

	public void AddPacket(PacketOrigin origin, byte[] data)
	{
		switch (origin)
		{
		case PacketOrigin.BattleNetClient:
			BCPacketBuffer.Enqueue(data);
			break;
		case PacketOrigin.BattleNetServer:
			BSPacketBuffer.Enqueue(data);
			break;
		case PacketOrigin.RealmClient:
			RCPacketBuffer.Enqueue(data);
			break;
		case PacketOrigin.RealmServer:
			RSPacketBuffer.Enqueue(data);
			break;
		case PacketOrigin.GameClient:
			GCPacketBuffer.Enqueue(data);
			break;
		case PacketOrigin.GameServer:
			GSPacketBuffer.Enqueue(data);
			break;
		}
	}

	private void InterpretPacket(PacketOrigin origin, byte[] data)
	{
		switch (origin)
		{
		case PacketOrigin.BattleNetClient:
		{
			if (data.Length < 2)
			{
				break;
			}
			BCPacket bcPacket = new BCPacket(data);
			if (bcPacket.PacketID <= 131)
			{
				if (BCAsyncPacketHandlers[bcPacket.PacketID] != null)
				{
					AsyncHelper.FireAsync(BCAsyncPacketHandlers[bcPacket.PacketID], bcPacket);
				}
				if (BCPacketWaitersCount[bcPacket.PacketID] > 0)
				{
					BCPackets[bcPacket.PacketID] = bcPacket;
					BCPacketWaiters[bcPacket.PacketID].Set();
					BCPacketWaitersCount[bcPacket.PacketID] = 0;
				}
			}
			break;
		}
		case PacketOrigin.BattleNetServer:
		{
			BSPacket bsPacket = new BSPacket(data);
			if (bsPacket.PacketID <= 131)
			{
				if (BSAsyncPacketHandlers[bsPacket.PacketID] != null)
				{
					AsyncHelper.FireAsync(BSAsyncPacketHandlers[bsPacket.PacketID], bsPacket);
				}
				if (BSPacketWaitersCount[bsPacket.PacketID] > 0)
				{
					BSPackets[bsPacket.PacketID] = bsPacket;
					BSPacketWaiters[bsPacket.PacketID].Set();
					BSPacketWaitersCount[bsPacket.PacketID] = 0;
				}
			}
			break;
		}
		case PacketOrigin.RealmClient:
		{
			if (data.Length < 2)
			{
				break;
			}
			RCPacket rcPacket = new RCPacket(data);
			if (rcPacket.PacketID <= 32)
			{
				if (RCAsyncPacketHandlers[rcPacket.PacketID] != null)
				{
					AsyncHelper.FireAsync(RCAsyncPacketHandlers[rcPacket.PacketID], rcPacket);
				}
				if (RCPacketWaitersCount[rcPacket.PacketID] > 0)
				{
					RCPackets[rcPacket.PacketID] = rcPacket;
					RCPacketWaiters[rcPacket.PacketID].Set();
					RCPacketWaitersCount[rcPacket.PacketID] = 0;
				}
			}
			break;
		}
		case PacketOrigin.RealmServer:
		{
			RSPacket rsPacket = new RSPacket(data);
			if (rsPacket.PacketID <= 32)
			{
				if (RSAsyncPacketHandlers[rsPacket.PacketID] != null)
				{
					AsyncHelper.FireAsync(RSAsyncPacketHandlers[rsPacket.PacketID], rsPacket);
				}
				if (RSPacketWaitersCount[rsPacket.PacketID] > 0)
				{
					RSPackets[rsPacket.PacketID] = rsPacket;
					RSPacketWaiters[rsPacket.PacketID].Set();
					RSPacketWaitersCount[rsPacket.PacketID] = 0;
				}
			}
			break;
		}
		case PacketOrigin.GameClient:
		{
			GCPacket gcPacket = new GCPacket(data);
			if (gcPacket.PacketID <= 110)
			{
				if (GCAsyncPacketHandlers[gcPacket.PacketID] != null)
				{
					AsyncHelper.FireAsync(GCAsyncPacketHandlers[gcPacket.PacketID], gcPacket);
				}
				if (GCPacketWaitersCount[gcPacket.PacketID] > 0)
				{
					GCPackets[gcPacket.PacketID] = gcPacket;
					GCPacketWaiters[gcPacket.PacketID].Set();
					GCPacketWaitersCount[gcPacket.PacketID] = 0;
				}
			}
			break;
		}
		case PacketOrigin.GameServer:
		{
			GSPacket gsPacket = new GSPacket(data);
			if (gsPacket.PacketID <= 177)
			{
				if (GSAsyncPacketHandlers[gsPacket.PacketID] != null)
				{
					AsyncHelper.FireAsync(GSAsyncPacketHandlers[gsPacket.PacketID], gsPacket);
				}
				if (GSPacketWaitersCount[gsPacket.PacketID] > 0)
				{
					GSPackets[gsPacket.PacketID] = gsPacket;
					GSPacketWaiters[gsPacket.PacketID].Set();
					GSPacketWaitersCount[gsPacket.PacketID] = 0;
				}
			}
			break;
		}
		default:
			throw new Exception("This protocol is not yet supported in Packets.AddPacket(D2SocketType type, byte[] data)!");
		}
	}
}
