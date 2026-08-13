using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x3B - Increment Skill
/// <para>Raise a skill by one point.</para>
/// </summary>
public class IncrementSkill : GCPacket
{
	protected SkillType skill;

	public SkillType Skill => skill;

	public IncrementSkill(byte[] data)
		: base(data)
	{
		skill = (SkillType)BitConverter.ToUInt16(data, 1);
	}

	public IncrementSkill(SkillType skill)
		: base(Build(skill))
	{
		this.skill = skill;
	}

	public static byte[] Build(SkillType skill)
	{
		return new byte[3]
		{
			59,
			(byte)skill,
			(byte)((int)skill >> 8)
		};
	}
}
