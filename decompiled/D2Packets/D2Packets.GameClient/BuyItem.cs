using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x32 - Buy Item
/// <para>Buy an item from a town folk.</para>
/// </summary>
public class BuyItem : GCPacket
{
	protected uint dealerUID;

	protected uint itemUID;

	protected TradeType tradeType;

	protected BuyFlags flags;

	protected uint cost;

	public TradeType TradeType => tradeType;

	public BuyFlags Flags => flags;

	public uint Cost => cost;

	public uint ItemUID => itemUID;

	public uint DealerUID => dealerUID;

	public BuyItem(byte[] data)
		: base(data)
	{
		dealerUID = BitConverter.ToUInt32(data, 1);
		itemUID = BitConverter.ToUInt32(data, 5);
		tradeType = (TradeType)BitConverter.ToUInt16(data, 9);
		flags = (BuyFlags)BitConverter.ToUInt16(data, 11);
		cost = BitConverter.ToUInt32(data, 13);
	}

	public BuyItem(uint dealerUID, uint itemUID, uint cost, BuyFlags flags)
		: base(Build(dealerUID, itemUID, cost, flags))
	{
		this.dealerUID = dealerUID;
		this.itemUID = itemUID;
		this.cost = cost;
		tradeType = TradeType.BuyItem;
		this.flags = flags;
	}

	public static byte[] Build(uint dealerUID, uint itemUID, uint cost, BuyFlags flags)
	{
		return new byte[17]
		{
			50,
			(byte)dealerUID,
			(byte)(dealerUID >> 8),
			(byte)(dealerUID >> 16),
			(byte)(dealerUID >> 24),
			(byte)itemUID,
			(byte)(itemUID >> 8),
			(byte)(itemUID >> 16),
			(byte)(itemUID >> 24),
			0,
			0,
			(byte)flags,
			(byte)((int)flags >> 8),
			(byte)cost,
			(byte)(cost >> 8),
			(byte)(cost >> 16),
			(byte)(cost >> 24)
		};
	}
}
