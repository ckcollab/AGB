using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0xA2 - Merc Word To Experience
/// </summary>
public class MercWordToExperience : GainExperience
{
	public static readonly bool WRAPPED = true;

	public MercWordToExperience(byte[] data)
		: base(data)
	{
		experience = BitConverter.ToUInt16(data, 6);
	}
}
