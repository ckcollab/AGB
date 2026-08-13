using System;
using System.Threading;
using D2Data;
using D2Packets.D2Packets;
using D2Packets.GameClient;
using D2Packets.GameServer;

namespace AGB.D2;

public class Warp : Unit
{
	public UnitType UnitType;

	public WarpType Id;

	public Warp(Game game)
		: base(game)
	{
	}

	public bool InteractWait(int timeOut)
	{
		bool receivedValidReassign = false;
		PacketEventHandler playerReassignDelg = delegate(D2Packet packet)
		{
			PlayerReassign playerReassign = new PlayerReassign(packet.Data);
			if (playerReassign.UID == Game.Hero.Uid)
			{
				receivedValidReassign = true;
			}
		};
		Game.Socket.PacketHandler.AddAsyncListener(GameServerPacket.PlayerReassign, playerReassignDelg);
		DateTime watchstart = DateTime.Now;
		while (DateTime.Now.Subtract(watchstart).TotalMilliseconds < (double)timeOut)
		{
			Game.Socket.Game.Send(new RunToTarget(UnitType, Uid).Data);
			Game.Socket.Game.Send(new UnitInteract(UnitType, Uid).Data);
			Thread.Sleep(1000);
			if (receivedValidReassign)
			{
				break;
			}
		}
		Game.Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.PlayerReassign, playerReassignDelg);
		return receivedValidReassign;
	}
}
