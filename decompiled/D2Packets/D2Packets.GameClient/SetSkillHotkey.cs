using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x51 - Set Skill Hotkey
/// <para>Assigns or unassigns a skill to a hotkey.</para>
/// </summary>
public class SetSkillHotkey : GCPacket
{
	public static readonly uint NULL_UInt32 = uint.MaxValue;

	protected SkillType skill;

	protected ushort slot;

	protected uint chargedItemUID;

	public ushort Slot => slot;

	public SkillType Skill => skill;

	public uint ChargedItemUID => chargedItemUID;

	public SetSkillHotkey(byte[] data)
		: base(data)
	{
		skill = (SkillType)BitConverter.ToUInt16(data, 1);
		slot = BitConverter.ToUInt16(data, 3);
		chargedItemUID = BitConverter.ToUInt32(data, 5);
	}

	public SetSkillHotkey(ushort slot, SkillType skill)
		: this(slot, skill, uint.MaxValue)
	{
	}

	public SetSkillHotkey(ushort slot, SkillType skill, uint itemUID)
		: base(Build(slot, skill, itemUID))
	{
		this.slot = slot;
		this.skill = skill;
		chargedItemUID = itemUID;
	}

	public static byte[] Build(ushort slot, SkillType skill)
	{
		return Build(slot, skill, uint.MaxValue);
	}

	public static byte[] Build(ushort slot, SkillType skill, uint itemUID)
	{
		return new byte[9]
		{
			81,
			(byte)skill,
			(byte)((int)skill >> 8),
			(byte)slot,
			(byte)(slot >> 8),
			(byte)itemUID,
			(byte)(itemUID >> 8),
			(byte)(itemUID >> 16),
			(byte)(itemUID >> 24)
		};
	}
}
