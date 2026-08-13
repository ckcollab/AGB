using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x1B - Word To Experience
/// </summary>
public class WordToExperience : GainExperience
{
	public static readonly bool WRAPPED = true;

	public WordToExperience(byte[] data)
		: base(data)
	{
		experience = BitConverter.ToUInt16(data, 1);
	}
}
