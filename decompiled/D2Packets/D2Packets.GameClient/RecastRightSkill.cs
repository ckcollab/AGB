using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x0F - Recast Right Skill
/// <para>Recasts the currently set right hand skill on a target location when spamming.</para>
/// </summary>
public class RecastRightSkill : GCPacket
{
	protected int x;

	protected int y;

	public int X => x;

	public int Y => y;

	public RecastRightSkill(byte[] data)
		: base(data)
	{
		x = BitConverter.ToUInt16(data, 1);
		y = BitConverter.ToUInt16(data, 3);
	}

	public RecastRightSkill(int x, int y)
		: base(Build(x, y))
	{
		this.x = x;
		this.y = y;
	}

	public static byte[] Build(int x, int y)
	{
		return new byte[5]
		{
			15,
			(byte)x,
			(byte)(x >> 8),
			(byte)y,
			(byte)(y >> 8)
		};
	}
}
