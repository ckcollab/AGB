using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x07 - Map Add
/// <para>Marks a room as active.</para>
/// </summary>
public class MapAdd : GSPacket
{
	protected int x;

	protected int y;

	protected AreaLevel area;

	public AreaLevel Area => area;

	public int X => x;

	public int Y => y;

	public MapAdd(byte[] data)
		: base(data)
	{
		x = BitConverter.ToUInt16(data, 1);
		y = BitConverter.ToUInt16(data, 3);
		area = (AreaLevel)data[5];
	}

	public MapAdd(AreaLevel area, int x, int y)
		: base(Build(area, x, y))
	{
		this.x = x;
		this.y = y;
		this.area = area;
	}

	public static byte[] Build(AreaLevel area, int x, int y)
	{
		return new byte[6]
		{
			7,
			(byte)x,
			(byte)(x >> 8),
			(byte)y,
			(byte)(y >> 8),
			(byte)area
		};
	}
}
