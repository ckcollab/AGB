using System;

namespace D2Packets.GameClient;

/// <summary>
/// Extra action flags used by <see cref="T:D2Packets.GameClient.BuyItem" />.
/// </summary>
[Flags]
public enum BuyFlags : ushort
{
	None = 0,
	FillStack = 0x8000
}
