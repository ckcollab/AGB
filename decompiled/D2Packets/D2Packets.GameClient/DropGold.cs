using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x50 - Drop Gold
/// <para>Remove gold from inventory and drop it on the ground.</para>
/// </summary>
public class DropGold : GCPacket
{
	protected uint meUID;

	protected uint amount;

	public uint Amount => amount;

	public uint MeUID => meUID;

	public DropGold(byte[] data)
		: base(data)
	{
		meUID = BitConverter.ToUInt32(data, 1);
		amount = BitConverter.ToUInt32(data, 5);
	}

	public DropGold(uint amount, uint meUID)
		: base(Build(amount, meUID))
	{
		this.amount = amount;
		this.meUID = meUID;
	}

	public static byte[] Build(uint amount, uint meUID)
	{
		return new byte[9]
		{
			80,
			(byte)meUID,
			(byte)(meUID >> 8),
			(byte)(meUID >> 16),
			(byte)(meUID >> 24),
			(byte)amount,
			(byte)(amount >> 8),
			(byte)(amount >> 16),
			(byte)(amount >> 24)
		};
	}
}
