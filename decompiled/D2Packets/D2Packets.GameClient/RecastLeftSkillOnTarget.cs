using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x09 - Recast Left Skill On Target
/// <para>Recasts the currently set left hand skill on a target unit when spamming.</para>
/// </summary>
public class RecastLeftSkillOnTarget : GCPacket
{
	protected UnitType unitType;

	protected uint uid;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public RecastLeftSkillOnTarget(byte[] data)
		: base(data)
	{
		unitType = (UnitType)BitConverter.ToUInt32(data, 1);
		uid = BitConverter.ToUInt32(data, 5);
	}

	public RecastLeftSkillOnTarget(UnitType unitType, uint uid)
		: base(Build(unitType, uid))
	{
		this.unitType = unitType;
		this.uid = uid;
	}

	public static byte[] Build(UnitType unitType, uint uid)
	{
		return new byte[9]
		{
			9,
			(byte)unitType,
			0,
			0,
			0,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
