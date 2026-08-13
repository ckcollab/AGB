using System;
using ETUtils;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x35 - Town Folk Repair
/// <para>Ask a blacksmith to repair one or all item(s).</para>
/// </summary>
public class TownFolkRepair : GCPacket
{
	public static readonly uint NULL_UInt32 = 0u;

	protected uint dealerUID;

	protected uint itemUID;

	protected RepairType repairType;

	public uint DealerUID => dealerUID;

	public uint ItemUID => itemUID;

	public RepairType RepairType => repairType;

	public string Unknown9 => ByteConverter.ToHexString(data, 13, 4);

	public TownFolkRepair(byte[] data)
		: base(data)
	{
		dealerUID = BitConverter.ToUInt32(data, 1);
		itemUID = BitConverter.ToUInt32(data, 5);
		repairType = (RepairType)BitConverter.ToUInt32(data, 9);
	}

	public TownFolkRepair(uint dealerUID)
		: base(Build(dealerUID))
	{
		this.dealerUID = dealerUID;
		itemUID = 0u;
		repairType = RepairType.RepairAll;
	}

	/// <summary>
	/// Builds a repair all items packet for a town folk NPC.
	/// </summary>
	/// <param name="dealerUID">The UID of the NPC to repair at.</param>
	public static byte[] Build(uint dealerUID)
	{
		return new byte[17]
		{
			53,
			(byte)dealerUID,
			(byte)(dealerUID >> 8),
			(byte)(dealerUID >> 16),
			(byte)(dealerUID >> 24),
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			128
		};
	}
}
