namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x8F - Pong
/// </summary>
public class Pong : GSPacket
{
	public Pong(byte[] data)
		: base(data)
	{
	}

	public Pong()
		: base(Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1] { 143 };
	}
}
