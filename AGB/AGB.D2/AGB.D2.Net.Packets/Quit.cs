namespace AGB.D2.Net.Packets;

public class Quit : AGBPacket
{
	public override byte[] Data => new byte[0];

	public override PacketType Type => PacketType.Quit;

	public static Welcome Parse(byte[] packetData, int offset)
	{
		return new Welcome();
	}
}
