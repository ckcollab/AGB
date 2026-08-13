using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x0A - Recast Left Skill On Target Stopped
/// <para>Recasts the currently set left hand skill on a target unit when spamming.</para>
/// </summary>
public class RecastLeftSkillOnTargetStopped : GCPacket
{
	protected UnitType unitType;

	protected uint uid;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public RecastLeftSkillOnTargetStopped(byte[] data)
		: base(data)
	{
		unitType = (UnitType)BitConverter.ToUInt32(data, 1);
		uid = BitConverter.ToUInt32(data, 5);
	}

	public RecastLeftSkillOnTargetStopped(UnitType unitType, uint uid)
		: base(Build(unitType, uid))
	{
		this.unitType = unitType;
		this.uid = uid;
	}

	public static byte[] Build(UnitType unitType, uint uid)
	{
		return new byte[9]
		{
			10,
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
