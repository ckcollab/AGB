namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x00 - Keep Alive
/// <para>Keeps the connection alive.</para>
/// </summary>
public class KeepAlive : BSPacket
{
	public KeepAlive(byte[] data)
		: base(data)
	{
	}
}
