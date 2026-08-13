using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x08 - Recast Left Skill
/// <para>Recasts the currently set left hand skill on a target location when spamming.</para>
/// </summary>
public class RecastLeftSkill : GCPacket
{
	protected int x;

	protected int y;

	public int X => x;

	public int Y => y;

	public RecastLeftSkill(byte[] data)
		: base(data)
	{
		x = BitConverter.ToUInt16(data, 1);
		y = BitConverter.ToUInt16(data, 3);
	}

	public RecastLeftSkill(int x, int y)
		: base(Build(x, y))
	{
		this.x = x;
		this.y = y;
	}

	public static byte[] Build(int x, int y)
	{
		return new byte[5]
		{
			8,
			(byte)x,
			(byte)(x >> 8),
			(byte)y,
			(byte)(y >> 8)
		};
	}
}
