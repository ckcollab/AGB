namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x00 - Keep Alive
/// <para>Keeps the connection alive.</para>
/// </summary>
public class KeepAlive : BCPacket
{
	public KeepAlive(byte[] data)
		: base(data)
	{
	}
}
