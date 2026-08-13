using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x33 - Sell Item
/// <para>Sell an item to a town folk.</para>
/// </summary>
public class SellItem : GCPacket
{
	protected uint dealerUID;

	protected uint itemUID;

	protected TradeType tradeType;

	protected uint cost;

	public TradeType TradeType => tradeType;

	public uint Cost => cost;

	public uint ItemUID => itemUID;

	public uint DealerUID => dealerUID;

	public SellItem(byte[] data)
		: base(data)
	{
		dealerUID = BitConverter.ToUInt32(data, 1);
		itemUID = BitConverter.ToUInt32(data, 5);
		tradeType = (TradeType)BitConverter.ToUInt32(data, 9);
		cost = BitConverter.ToUInt32(data, 13);
	}

	public SellItem(uint dealerUID, uint itemUID, uint cost)
		: base(Build(dealerUID, itemUID, cost))
	{
		this.dealerUID = dealerUID;
		this.itemUID = itemUID;
		this.cost = cost;
		tradeType = TradeType.SellItem;
	}

	public static byte[] Build(uint dealerUID, uint itemUID, uint cost)
	{
		return new byte[17]
		{
			51,
			(byte)dealerUID,
			(byte)(dealerUID >> 8),
			(byte)(dealerUID >> 16),
			(byte)(dealerUID >> 24),
			(byte)itemUID,
			(byte)(itemUID >> 8),
			(byte)(itemUID >> 16),
			(byte)(itemUID >> 24),
			4,
			0,
			0,
			0,
			(byte)cost,
			(byte)(cost >> 8),
			(byte)(cost >> 16),
			(byte)(cost >> 24)
		};
	}
}
