using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x42 - Player Clear Cursor
/// <para>Player's cursor is emptied (e.g. when selling an item or give it to merc, 
/// but not when dropped to ground or container...)</para>
/// <para>Only sent for the receiving player for that purpose, but may have other uses too !?</para>
/// </summary>
public class PlayerClearCursor : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public PlayerClearCursor(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
	}

	public PlayerClearCursor(UnitType unitType, uint uid)
		: base(Build(unitType, uid))
	{
		this.unitType = unitType;
		this.uid = uid;
	}

	public static byte[] Build(UnitType unitType, uint uid)
	{
		return new byte[6]
		{
			66,
			(byte)unitType,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
