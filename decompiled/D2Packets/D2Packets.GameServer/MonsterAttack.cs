using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x6C - Monster Attack
/// <para>A monster is attacking something in your range of view.</para>
/// </summary>
public class MonsterAttack : GSPacket
{
	protected uint uid;

	protected ushort attackType;

	protected uint targetUID;

	protected UnitType targetType;

	protected int x;

	protected int y;

	public uint UID => uid;

	public ushort AttackType => attackType;

	public uint TargetUID => targetUID;

	public UnitType TargetType => targetType;

	public int X => x;

	public int Y => y;

	public MonsterAttack(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		attackType = BitConverter.ToUInt16(data, 5);
		targetUID = BitConverter.ToUInt32(data, 7);
		targetType = (UnitType)data[11];
		x = BitConverter.ToUInt16(data, 12);
		y = BitConverter.ToUInt16(data, 14);
	}
}
