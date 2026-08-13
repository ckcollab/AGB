namespace AGB.D2.Net.Packets;

public class Welcome : AGBPacket
{
	public override byte[] Data => new byte[0];

	public override PacketType Type => PacketType.Welcome;

	public static Welcome Parse(byte[] packetData, int offset)
	{
		return new Welcome();
	}
}
