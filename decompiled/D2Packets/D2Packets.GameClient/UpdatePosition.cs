using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x5F - Update Position
/// </summary>
public class UpdatePosition : GCPacket
{
	protected int x;

	protected int y;

	public int X => x;

	public int Y => y;

	public UpdatePosition(byte[] data)
		: base(data)
	{
		x = BitConverter.ToUInt16(data, 1);
		y = BitConverter.ToUInt16(data, 3);
	}

	public UpdatePosition(int x, int y)
		: base(Build(x, y))
	{
		this.x = x;
		this.y = y;
	}

	public static byte[] Build(int x, int y)
	{
		return new byte[5]
		{
			95,
			(byte)x,
			(byte)(x >> 8),
			(byte)y,
			(byte)(y >> 8)
		};
	}
}
