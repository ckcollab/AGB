namespace AGB.D2.Net.Packets;

public class Pong : AGBPacket
{
	public override byte[] Data => new byte[0];

	public override PacketType Type => PacketType.Pong;

	public static Pong Parse(byte[] packetData, int offset)
	{
		return new Pong();
	}
}
