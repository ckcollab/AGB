using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x0C - NPC Get Hit
/// <para>An NPC takes damage within your range of view.</para>
/// </summary>
public class NPCGetHit : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected int animation;

	protected byte life;

	public uint UID => uid;

	public byte Life => life;

	public int Animation => animation;

	public UnitType UnitType => unitType;

	public NPCGetHit(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		animation = BitConverter.ToUInt16(data, 6);
		life = data[8];
	}

	public NPCGetHit(UnitType type, uint uid, byte life, int anim)
		: base(Build(type, uid, life, anim))
	{
		unitType = type;
		this.uid = uid;
		animation = anim;
		this.life = life;
	}

	public static byte[] Build(UnitType type, uint uid, byte life, int anim)
	{
		if (life > 128)
		{
			throw new ArgumentOutOfRangeException("life");
		}
		return new byte[9]
		{
			12,
			(byte)type,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)anim,
			(byte)(anim >> 8),
			life
		};
	}
}
