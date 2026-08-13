using System;

namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x25 - Bnet Ping
/// <para>Sends a timestamp to client to be returned in other to calculate ping.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.BnetClient.BnetPong" />
/// </remarks>
public class BnetPing : BSPacket
{
	protected uint timestamp;

	public uint Timestamp => timestamp;

	public BnetPing(byte[] data)
		: base(data)
	{
		timestamp = BitConverter.ToUInt32(data, 4);
	}
}
