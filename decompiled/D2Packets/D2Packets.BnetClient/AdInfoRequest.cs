using System;
using D2Data;
using ETUtils;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x15 - Ad Info Request
/// <para>Requests next ad...</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.BnetServer.AdInfo" />
/// </remarks>
public class AdInfoRequest : BCPacket
{
	protected BattleNetPlatform platform;

	protected BattleNetClient client;

	protected uint id;

	protected DateTime timestamp;

	public BattleNetPlatform Platform => platform;

	public BattleNetClient Client => client;

	public uint ID => id;

	public DateTime Timestamp => timestamp;

	public AdInfoRequest(byte[] data)
		: base(data)
	{
		platform = (BattleNetPlatform)BitConverter.ToUInt32(data, 4);
		client = (BattleNetClient)BitConverter.ToUInt32(data, 8);
		id = BitConverter.ToUInt32(data, 12);
		timestamp = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, 16));
	}
}
