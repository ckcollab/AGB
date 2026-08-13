namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet Wrapper - Gain Experience
/// <para>Wrapper for <see cref="T:D2Packets.GameServer.ByteToExperience" />, <see cref="T:D2Packets.GameServer.WordToExperience" /> and 
/// <see cref="T:D2Packets.GameServer.DWordToExperience" />.</para>
/// <para>The first packet when joining game just notifies you of your current experience; don't add it to a cached value!</para>
/// </summary>
public class GainExperience : GSPacket
{
	protected uint experience;

	public uint Experience => experience;

	public GainExperience(byte[] data)
		: base(data)
	{
	}
}
