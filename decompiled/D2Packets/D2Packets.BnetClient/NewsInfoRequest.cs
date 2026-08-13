using System;
using ETUtils;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x46 - News Info Request
/// <para>Request news...</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.BnetServer.NewsInfo" />
/// </remarks>
public class NewsInfoRequest : BCPacket
{
	protected DateTime since;

	public DateTime Since => since;

	public NewsInfoRequest(byte[] data)
		: base(data)
	{
		since = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, 4));
	}
}
