using System;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x25 - Bnet Pong
/// <para>Response to BnetPing, used to calculate the ping time.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.BnetServer.BnetPing" />
/// </remarks>
public class BnetPong : BCPacket
{
	protected uint timestamp;

	public uint Timestamp => timestamp;

	public BnetPong(byte[] data)
		: base(data)
	{
		timestamp = BitConverter.ToUInt32(data, 4);
	}
}
