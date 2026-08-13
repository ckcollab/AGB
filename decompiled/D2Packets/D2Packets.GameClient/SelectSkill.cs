using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x3C - SelectSkill
/// <para>Sets a given skill as the active one for specified hand.</para>
/// </summary>
public class SelectSkill : GCPacket
{
	public static readonly uint NULL_UInt32 = 0u;

	protected SkillType skill;

	protected SkillHand hand = SkillHand.Right;

	protected uint chargedItemUID;

	public SkillType Skill => skill;

	public SkillHand Hand => hand;

	public uint ChargedItemUID => chargedItemUID;

	public SelectSkill(byte[] data)
		: base(data)
	{
		skill = (SkillType)BitConverter.ToUInt16(data, 1);
		if (data[4] == 128)
		{
			hand = SkillHand.Left;
		}
		chargedItemUID = BitConverter.ToUInt32(data, 5);
		if (chargedItemUID == uint.MaxValue)
		{
			chargedItemUID = 0u;
		}
	}

	public SelectSkill(SkillType skill, SkillHand hand)
		: base(Build(skill, hand))
	{
		this.skill = skill;
		this.hand = hand;
		chargedItemUID = 0u;
	}

	public SelectSkill(SkillType skill, SkillHand hand, uint chargedItemUID)
		: base(Build(skill, hand, chargedItemUID))
	{
		this.skill = skill;
		this.hand = hand;
		this.chargedItemUID = chargedItemUID;
	}

	public static byte[] Build(SkillType skill, SkillHand hand)
	{
		return Build(skill, hand, uint.MaxValue);
	}

	public static byte[] Build(SkillType skill, SkillHand hand, uint chargedItemUID)
	{
		return new byte[9]
		{
			60,
			(byte)skill,
			(byte)((int)skill >> 8),
			0,
			(byte)((hand == SkillHand.Left) ? 128u : 0u),
			(byte)chargedItemUID,
			(byte)(chargedItemUID >> 8),
			(byte)(chargedItemUID >> 16),
			(byte)(chargedItemUID >> 24)
		};
	}
}
