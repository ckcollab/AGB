using System;

namespace D2Packets.GameServer;

/// <summary>
/// Town Portal state flags used by <see cref="T:D2Packets.GameServer.PortalInfo" />.
/// </summary>
[Flags]
public enum TownPortalState
{
	None = 0,
	Unknown1 = 1,
	IsOtherSide = 2,
	Used = 4
}
