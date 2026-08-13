using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x90 - Party Member Pulse
/// <para>You should receive this packet every second for each member of your party not in range of view.</para>
/// </summary>
public class PartyMemberPulse : GSPacket
{
	protected uint uid;

	protected int x;

	protected int y;

	public uint UID => uid;

	public int X => x;

	public int Y => y;

	public PartyMemberPulse(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		x = BitConverter.ToInt32(data, 5);
		y = BitConverter.ToInt32(data, 9);
	}

	public PartyMemberPulse(uint uid, int x, int y)
		: base(Build(uid, x, y))
	{
		this.uid = uid;
		this.x = x;
		this.y = y;
	}

	public static byte[] Build(uint uid, int x, int y)
	{
		return new byte[13]
		{
			144,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)x,
			(byte)(x >> 8),
			(byte)(x >> 16),
			(byte)(x >> 24),
			(byte)y,
			(byte)(y >> 8),
			(byte)(y >> 16),
			(byte)(y >> 24)
		};
	}
}
