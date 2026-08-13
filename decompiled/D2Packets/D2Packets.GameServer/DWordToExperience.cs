using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x1C - DWord To Experience
/// </summary>
public class DWordToExperience : GainExperience
{
	public static readonly bool WRAPPED = true;

	public DWordToExperience(byte[] data)
		: base(data)
	{
		experience = BitConverter.ToUInt32(data, 1);
	}
}
