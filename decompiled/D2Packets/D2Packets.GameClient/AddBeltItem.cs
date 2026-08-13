using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x23 - Add Belt Item
/// <para>Place the item on cursor into specified position in belt. Must be a potion or scroll...</para>
/// </summary>
public class AddBeltItem : GCPacket
{
	protected uint uid;

	protected int x;

	protected int y;

	public uint UID => uid;

	public int X => x;

	public int Y => y;

	public AddBeltItem(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		x = (int)data[5] % 4;
		y = (int)data[5] / 4;
	}

	public AddBeltItem(uint uid, int x, int y)
		: base(Build(uid, x, y))
	{
		this.uid = uid;
		this.x = x;
		this.y = y;
	}

	public static byte[] Build(uint uid, int x, int y)
	{
		return new byte[9]
		{
			35,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)(y * 4 + x),
			0,
			0,
			0
		};
	}
}
