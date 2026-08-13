using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x0D - Cast Right Skill On Target
/// <para>Casts the currently set right hand skill on a target unit.</para>
/// </summary>
public class CastRightSkillOnTarget : GCPacket
{
	protected UnitType unitType;

	protected uint uid;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public CastRightSkillOnTarget(byte[] data)
		: base(data)
	{
		unitType = (UnitType)BitConverter.ToUInt32(data, 1);
		uid = BitConverter.ToUInt32(data, 5);
	}

	public CastRightSkillOnTarget(UnitType unitType, uint uid)
		: base(Build(unitType, uid))
	{
		this.unitType = unitType;
		this.uid = uid;
	}

	public static byte[] Build(UnitType unitType, uint uid)
	{
		return new byte[9]
		{
			13,
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
