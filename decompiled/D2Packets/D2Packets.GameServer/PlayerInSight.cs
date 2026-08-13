using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x76 - Player In Sight
/// <para>Can be used to grab other players' UID when they come in your sight.</para>
/// </summary>
public class PlayerInSight : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public PlayerInSight(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
	}

	public PlayerInSight(UnitType unitType, uint uid)
		: base(Build(unitType, uid))
	{
		this.unitType = unitType;
		this.uid = uid;
	}

	public static byte[] Build(UnitType unitType, uint uid)
	{
		return new byte[6]
		{
			118,
			(byte)unitType,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
