using System;
using ETUtils;

namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x46 - News Info
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.BnetClient.NewsInfoRequest" />
/// </remarks>
public class NewsInfo : BSPacket
{
	protected int count;

	protected DateTime lastLogon;

	protected DateTime oldestEntry;

	protected DateTime newestEntry;

	protected NewsEntry[] entries;

	public DateTime LastLogon => lastLogon;

	public DateTime OldestEntry => oldestEntry;

	public DateTime NewestEntry => newestEntry;

	public int Count => count;

	public NewsEntry[] Entries => entries;

	public NewsInfo(byte[] data)
		: base(data)
	{
		count = data[4];
		lastLogon = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, 5));
		oldestEntry = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, 9));
		newestEntry = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, 13));
		entries = new NewsEntry[count];
		int offset = 17;
		for (int i = 0; i < entries.Length; i++)
		{
			ref NewsEntry reference = ref entries[i];
			reference = new NewsEntry(data, offset);
			offset += 5 + entries[i].Content.Length;
		}
	}
}
