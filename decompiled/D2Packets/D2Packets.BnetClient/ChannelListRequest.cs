using System;
using D2Data;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x0B - Channel List Request
/// <para>Requests a listing of channels.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.BnetServer.ChannelList" />
/// </remarks>
public class ChannelListRequest : BCPacket
{
	protected BattleNetClient client;

	public BattleNetClient Client => client;

	public ChannelListRequest(byte[] data)
		: base(data)
	{
		client = (BattleNetClient)BitConverter.ToUInt32(data, 4);
	}
}
