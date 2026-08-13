namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0xA1 - Merc Byte To Experience
/// </summary>
public class MercByteToExperience : GainExperience
{
	public static readonly bool WRAPPED = true;

	public MercByteToExperience(byte[] data)
		: base(data)
	{
		experience = data[6];
	}
}
