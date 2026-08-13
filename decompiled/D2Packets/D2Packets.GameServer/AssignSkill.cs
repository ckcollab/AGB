using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x23 - Assign Skill
/// <para>Sent in response to GameClient.SelectSkill when selecting a skill.</para>
/// </summary>
public class AssignSkill : GSPacket
{
	public static readonly uint NULL_UInt32 = uint.MaxValue;

	protected UnitType unitType;

	protected uint uid;

	protected SkillHand hand;

	protected SkillType skill;

	protected uint chargedItemUID;

	public SkillHand Hand => hand;

	public SkillType Skill => skill;

	public uint ChargedItemUID => chargedItemUID;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public AssignSkill(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		hand = (SkillHand)data[6];
		skill = (SkillType)BitConverter.ToUInt16(data, 7);
		chargedItemUID = BitConverter.ToUInt32(data, 9);
	}
}
