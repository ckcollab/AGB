using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x99 - Item Trigger Skill
/// <para>An item's chance to cast a skill on event takes effect.</para>
/// </summary>
public class ItemTriggerSkill : GSPacket
{
	protected UnitType ownerType;

	protected uint ownerUID;

	protected SkillType skill;

	protected byte level;

	protected UnitType targetType;

	protected uint targetUID;

	protected ItemEventCause cause;

	public SkillType Skill => skill;

	public byte Level => level;

	public ItemEventCause Cause => cause;

	public UnitType OwnerType => ownerType;

	public uint OwnerUID => ownerUID;

	public UnitType TargetType => targetType;

	public uint TargetUID => targetUID;

	public ItemTriggerSkill(byte[] data)
		: base(data)
	{
		ownerType = (UnitType)data[1];
		ownerUID = BitConverter.ToUInt32(data, 2);
		skill = (SkillType)BitConverter.ToUInt16(data, 6);
		level = data[8];
		targetType = (UnitType)data[9];
		targetUID = BitConverter.ToUInt32(data, 10);
		cause = (ItemEventCause)BitConverter.ToUInt16(data, 14);
	}
}
