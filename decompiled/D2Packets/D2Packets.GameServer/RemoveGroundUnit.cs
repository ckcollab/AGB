using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x0A - Remove Ground Unit
/// <para>Usually because it's no longer in proximity or was picked up.</para>
/// </summary>
public class RemoveGroundUnit : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public RemoveGroundUnit(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
	}

	public RemoveGroundUnit(UnitType type, uint uid)
		: base(Build(type, uid))
	{
		unitType = type;
		this.uid = uid;
	}

	public static byte[] Build(UnitType type, uint uid)
	{
		return new byte[6]
		{
			10,
			(byte)type,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
