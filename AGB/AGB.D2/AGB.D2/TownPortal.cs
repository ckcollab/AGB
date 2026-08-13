using D2Data;
using D2Packets.GameClient;

namespace AGB.D2;

public class TownPortal : Unit
{
	public string OwnerName;

	public uint OwnerUid;

	public uint PortalLocalUid;

	public uint PortalRemoteUid;

	public TownPortal(Game game)
		: base(game)
	{
		Game = game;
		OwnerName = "";
	}

	public bool PortalInteractWait(int timeOut)
	{
		Game.Socket.Game.Send(new RunToTarget(UnitType.GameObject, PortalLocalUid).Data);
		Game.Socket.Game.Send(new UnitInteract(UnitType.GameObject, PortalLocalUid).Data);
		Game.Socket.Game.Send(new RequestReassign(UnitType.NPC, Game.Hero.Uid).Data);
		return Game.Hero.WaitForReassign(timeOut);
	}
}
