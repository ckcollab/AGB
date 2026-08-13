namespace D2Packets.GameClient;

/// <summary>
/// Trade action type.
/// <para>Used by <see cref="T:D2Packets.GameClient.BuyItem" /> and <see cref="T:D2Packets.GameClient.SellItem" />.</para>
/// </summary>
public enum TradeType : ushort
{
	BuyItem = 0,
	GambleItem = 2,
	SellItem = 4
}
