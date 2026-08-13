using System;
using System.Threading;
using D2Data;
using D2Packets.D2Packets;
using D2Packets.GameClient;

namespace AGB.D2;

public class Object : Unit
{
	public GameObjectClass Id;

	public GameObjectMode State;

	public GameObjectInteractType InteractType;

	public AreaLevel Destination;

	public Object(Game game)
		: base(game)
	{
	}

	public void Interact()
	{
		Game.Socket.Game.Send(new RunToTarget(UnitType.GameObject, Uid).Data);
		Game.Socket.Game.Send(new UnitInteract(UnitType.GameObject, Uid).Data);
	}

	public bool PortalInteractWait(int timeOut)
	{
		Interact();
		Game.Socket.Game.Send(new RequestReassign(UnitType.NPC, Game.Hero.Uid).Data);
		if (Destination != 0)
		{
			return Game.Hero.WaitForReassign(Destination, timeOut);
		}
		return Game.Hero.WaitForReassign(timeOut);
	}

	public bool WaypointInteractWait(WaypointDestination destination, int timeOut)
	{
		return WaypointInteractWait((byte)destination, timeOut);
	}

	public bool WaypointInteractWait(byte destination, int timeOut)
	{
		if (!OpenWaypoint(timeOut))
		{
			return false;
		}
		byte[] data = new byte[9]
		{
			73,
			(byte)(Uid & 0xFFu),
			(byte)((Uid >> 8) & 0xFFu),
			(byte)((Uid >> 16) & 0xFFu),
			(byte)(Uid >> 24),
			destination,
			0,
			0,
			0
		};
		Game.Socket.Game.Send(data);
		if (!Game.Hero.WaitForReassign((AreaLevel)destination, timeOut))
		{
			return false;
		}
		CloseWaypoint();
		return true;
	}

	public bool OpenWaypoint(int timeOut)
	{
		bool receivedOpenWaypoint = false;
		PacketEventHandler openWaypointDelg = delegate
		{
			receivedOpenWaypoint = true;
		};
		Game.Socket.PacketHandler.AddAsyncListener(GameServerPacket.OpenWaypoint, openWaypointDelg);
		DateTime watchstart = DateTime.Now;
		while (DateTime.Now.Subtract(watchstart).TotalMilliseconds < (double)timeOut)
		{
			Interact();
			if (receivedOpenWaypoint)
			{
				break;
			}
			Thread.Sleep(1000);
		}
		Game.Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.OpenWaypoint, openWaypointDelg);
		return receivedOpenWaypoint;
	}

	public void CloseWaypoint()
	{
		Game.Socket.Game.Send(new WaypointInteract(Uid, WaypointDestination.CloseWaypoint).Data);
	}
}
