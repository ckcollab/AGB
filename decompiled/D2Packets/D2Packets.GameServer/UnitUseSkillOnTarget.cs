using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x4C - Unit Use Skill On Target
/// <para>Another unit uses a skill on another target unit.</para>
/// </summary>
public class UnitUseSkillOnTarget : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected SkillType skill;

	protected uint targetUID;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public SkillType Skill => skill;

	public uint TargetUID => targetUID;

	public string Unknown8 => ByteConverter.ToHexString(data, 8, 2);

	public string Unknown14 => ByteConverter.ToHexString(data, 14, 2);

	public UnitUseSkillOnTarget(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		skill = (SkillType)BitConverter.ToUInt16(data, 6);
		targetUID = BitConverter.ToUInt32(data, 10);
	}
}
