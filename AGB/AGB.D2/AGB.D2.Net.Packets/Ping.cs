namespace AGB.D2.Net.Packets;

public class Ping : AGBPacket
{
	public override byte[] Data => new byte[0];

	public override PacketType Type => PacketType.Ping;

	public static Ping Parse(byte[] packetData, int offset)
	{
		return new Ping();
	}
}
