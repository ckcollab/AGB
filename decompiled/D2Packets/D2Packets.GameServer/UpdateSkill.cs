using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x21 - Update Skill
/// <para>Sent when assigning a stat point to a skill or equipping an item proving a new skill.</para>
/// <para>Note that when equipping an item providing a new skill, the actual item's +skills bonuses will not be counted in the Bonus value 
/// (e.g. CtA skills bonus value will be 1 less than real value.)</para>
/// <para>Also this is not sent for charged skill which are handled differently or for skills you already have when equipping items.</para>
/// </summary>
public class UpdateSkill : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected SkillType skill;

	protected int baseLevel;

	protected int bonus;

	public SkillType Skill => skill;

	public int BaseLevel => baseLevel;

	public int Bonus => bonus;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	/// <summary>
	///             TODO: seems to have something to do with skill being a class skill / having a BaseLevel
	///             It's always 0 for skills provided by CtA and was 6f for an actual skill point spent on my zon...
	/// </summary>
	public string Unknown11 => ByteConverter.ToHexString(data, 11, 1);

	public UpdateSkill(byte[] data)
		: base(data)
	{
		unitType = (UnitType)BitConverter.ToUInt16(data, 1);
		uid = BitConverter.ToUInt32(data, 3);
		skill = (SkillType)BitConverter.ToUInt16(data, 7);
		baseLevel = data[9];
		bonus = data[10];
	}
}
