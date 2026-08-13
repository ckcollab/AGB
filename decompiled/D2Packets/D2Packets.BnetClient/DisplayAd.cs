using System;
using D2Data;
using ETUtils;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x21 - Display Ad
/// <para>Notifies Battle.net an ad has been displayed.</para>
/// </summary>
public class DisplayAd : BCPacket
{
	protected BattleNetPlatform platform;

	protected BattleNetClient client;

	protected uint id;

	protected string filename;

	protected string url;

	public BattleNetPlatform Platform => platform;

	public BattleNetClient Client => client;

	public uint ID => id;

	public string Filename => filename;

	public string URL => url;

	public DisplayAd(byte[] data)
		: base(data)
	{
		platform = (BattleNetPlatform)BitConverter.ToUInt32(data, 4);
		client = (BattleNetClient)BitConverter.ToUInt32(data, 8);
		id = BitConverter.ToUInt32(data, 12);
		if (data[16] != 0)
		{
			filename = ByteConverter.GetNullString(data, 16);
		}
		int offset = 17 + ((filename != null) ? filename.Length : 0);
		if (data[offset] != 0)
		{
			url = ByteConverter.GetNullString(data, offset);
		}
	}
}
