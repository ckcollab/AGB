using System;
using ETUtils;

namespace D2Packets.BnetServer;

/// <summary>
/// Structure containing a single Battle.net news entry.
/// <para>Used by <see cref="T:D2Packets.BnetServer.NewsInfo" />.</para>
/// </summary>
public struct NewsEntry
{
	private DateTime timestamp;

	private string content;

	public DateTime Timestamp => timestamp;

	public string Content => content;

	public NewsEntry(byte[] data, int offset)
	{
		timestamp = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, offset));
		content = ByteConverter.GetNullString(data, offset + 4);
	}

	public override string ToString()
	{
		return $"Timestamp: {Timestamp}, Content: {Content}";
	}
}
