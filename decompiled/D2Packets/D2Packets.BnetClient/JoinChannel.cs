using System;
using ETUtils;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x0C - Join Channel
/// <para>Request joining a chat channel.</para>
/// </summary>
public class JoinChannel : BCPacket
{
	protected JoinChannelFlags flags;

	protected string name;

	public JoinChannelFlags Flags => flags;

	public string Name => name;

	public JoinChannel(byte[] data)
		: base(data)
	{
		flags = (JoinChannelFlags)BitConverter.ToUInt32(data, 4);
		name = ByteConverter.GetNullString(data, 8);
	}
}
