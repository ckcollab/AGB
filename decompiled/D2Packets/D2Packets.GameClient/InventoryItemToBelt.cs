using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x63 - Inventory Item To Belt
/// <para>Move an item (potion or scroll) directly from inventory to belt.</para>
/// </summary>
public class InventoryItemToBelt : GCPacket
{
	protected uint uid;

	public uint UID => uid;

	public InventoryItemToBelt(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
	}

	public InventoryItemToBelt(uint uid)
		: base(Build(uid))
	{
		this.uid = uid;
	}

	public static byte[] Build(uint uid)
	{
		return new byte[5]
		{
			99,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
