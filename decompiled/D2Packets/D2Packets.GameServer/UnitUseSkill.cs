using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x4D - Unit Use Skill
/// <para>Another unit uses a skill not targeted at a unit.</para>
/// <para>Also a player activating a shrine (in witch case X / Y are null.)</para>
/// </summary>
public class UnitUseSkill : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected SkillType skill;

	protected int x;

	protected int y;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public SkillType Skill => skill;

	public int X => x;

	public int Y => y;

	public string Unknown10 => ByteConverter.ToHexString(data, 10, 1);

	public string Unknown15 => ByteConverter.ToHexString(data, 15, 2);

	public UnitUseSkill(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		if (unitType != UnitType.GameObject)
		{
			skill = (SkillType)BitConverter.ToUInt32(data, 6);
			x = BitConverter.ToUInt16(data, 11);
			y = BitConverter.ToUInt16(data, 13);
		}
	}
}
