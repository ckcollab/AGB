using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x24 - Remove Belt Item
/// <para>Remove an item (potion or scroll) from belt and place it on cursor.</para>
/// </summary>
public class RemoveBeltItem : GCPacket
{
	protected uint uid;

	public uint UID => uid;

	public RemoveBeltItem(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
	}

	public RemoveBeltItem(uint uid)
		: base(Build(uid))
	{
		this.uid = uid;
	}

	public static byte[] Build(uint uid)
	{
		return new byte[5]
		{
			36,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
