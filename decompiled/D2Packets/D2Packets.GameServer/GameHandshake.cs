using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x0B - Game Handshake
/// <para>Sent as part of game join data... UID will always be that of the receiving player.</para>
/// </summary>
public class GameHandshake : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public GameHandshake(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
	}

	public GameHandshake(UnitType type, uint uid)
		: base(Build(type, uid))
	{
		unitType = type;
		this.uid = uid;
	}

	public static byte[] Build(UnitType type, uint uid)
	{
		return new byte[6]
		{
			11,
			(byte)type,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
