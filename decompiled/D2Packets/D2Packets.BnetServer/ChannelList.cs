using System.Collections.Generic;
using ETUtils;

namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x0B - Channel List
/// <para>Provides a listing of available channels.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.BnetClient.ChannelListRequest" />
/// </remarks>
public class ChannelList : BSPacket
{
	protected List<string> channels;

	public List<string> Channels => channels;

	public ChannelList(byte[] data)
		: base(data)
	{
		channels = new List<string>();
		int offset = 4;
		int count = 0;
		while (offset < data.Length - 1)
		{
			channels.Add(ByteConverter.GetNullString(data, offset));
			offset += channels[count].Length + 1;
			count++;
		}
	}
}
