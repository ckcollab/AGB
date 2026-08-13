using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x7B - Assign Skill Hotkey
/// <para>Sent on game join to assign each skill to a hotkey slot.</para>
/// </summary>
public class AssignSkillHotkey : GSPacket
{
	public static readonly uint NULL_UInt32 = uint.MaxValue;

	protected byte slot;

	protected SkillType skill;

	protected uint chargedItemUID;

	public byte Slot => slot;

	public SkillType Skill => skill;

	public uint ChargedItemUID => chargedItemUID;

	public AssignSkillHotkey(byte[] data)
		: base(data)
	{
		slot = data[1];
		skill = (SkillType)BitConverter.ToUInt16(data, 2);
		chargedItemUID = BitConverter.ToUInt32(data, 4);
	}

	public AssignSkillHotkey(byte slot, SkillType skill)
		: this(slot, skill, uint.MaxValue)
	{
	}

	public AssignSkillHotkey(byte slot, SkillType skill, uint itemUID)
		: base(Build(slot, skill, itemUID))
	{
		this.slot = slot;
		this.skill = skill;
		chargedItemUID = itemUID;
	}

	public static byte[] Build(byte slot, SkillType skill)
	{
		return Build(slot, skill, uint.MaxValue);
	}

	public static byte[] Build(byte slot, SkillType skill, uint itemUID)
	{
		return new byte[8]
		{
			123,
			slot,
			(byte)skill,
			(byte)((int)skill >> 8),
			(byte)itemUID,
			(byte)(itemUID >> 8),
			(byte)(itemUID >> 16),
			(byte)(itemUID >> 24)
		};
	}
}
