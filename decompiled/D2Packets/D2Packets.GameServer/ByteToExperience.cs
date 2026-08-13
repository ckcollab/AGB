namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x1A - Byte To Experience
/// </summary>
public class ByteToExperience : GainExperience
{
	public static readonly bool WRAPPED = true;

	public ByteToExperience(byte[] data)
		: base(data)
	{
		experience = data[1];
	}
}
