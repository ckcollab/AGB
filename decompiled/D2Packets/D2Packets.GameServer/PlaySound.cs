using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x2C - Play Sound
/// </summary>
public class PlaySound : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected GameSound sound;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public GameSound Sound => sound;

	public PlaySound(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		sound = (GameSound)BitConverter.ToUInt16(data, 6);
	}

	public PlaySound(UnitType unitType, uint uid, GameSound sound)
		: base(Build(unitType, uid, sound))
	{
		this.unitType = unitType;
		this.uid = uid;
		this.sound = sound;
	}

	public static byte[] Build(UnitType unitType, uint uid, GameSound sound)
	{
		return new byte[8]
		{
			44,
			(byte)unitType,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)sound,
			(byte)((ushort)sound >> 8)
		};
	}
}
