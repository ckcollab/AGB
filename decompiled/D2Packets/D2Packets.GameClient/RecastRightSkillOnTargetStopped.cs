using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x11 - Recast Right Skill On Target Stopped
/// <para>Recasts the currently set right hand skill on a target unit when spamming.</para>
/// </summary>
public class RecastRightSkillOnTargetStopped : GCPacket
{
	protected UnitType unitType;

	protected uint uid;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public RecastRightSkillOnTargetStopped(byte[] data)
		: base(data)
	{
		unitType = (UnitType)BitConverter.ToUInt32(data, 1);
		uid = BitConverter.ToUInt32(data, 5);
	}

	public RecastRightSkillOnTargetStopped(UnitType unitType, uint uid)
		: base(Build(unitType, uid))
	{
		this.unitType = unitType;
		this.uid = uid;
	}

	public static byte[] Build(UnitType target, uint uid)
	{
		return new byte[9]
		{
			17,
			(byte)target,
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
