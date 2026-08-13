using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x3F - Use Stackable Item
/// <para>Use an item like a scroll, tome or potion.</para>
/// </summary>
public class UseStackableItem : GSPacket
{
	protected StackableItemClickType1 type1;

	protected uint uid;

	protected StackableItemClickType2 type2;

	public uint UID => uid;

	public StackableItemClickType1 Type1 => type1;

	public StackableItemClickType2 Type2 => type2;

	public UseStackableItem(byte[] data)
		: base(data)
	{
		type1 = (StackableItemClickType1)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		type2 = (StackableItemClickType2)BitConverter.ToInt16(data, 6);
	}
}
