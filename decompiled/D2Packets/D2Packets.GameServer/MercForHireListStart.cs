namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x4F - Merc For Hire List Start
/// <para>Sent before the 0x4E packets when interacting with a slaver town folk.</para>
/// </summary>
public class MercForHireListStart : GSPacket
{
	public MercForHireListStart(byte[] data)
		: base(data)
	{
	}

	public MercForHireListStart()
		: base(Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1] { 79 };
	}
}
