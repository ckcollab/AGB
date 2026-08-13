using System;
using D2Data;
using ETUtils;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x22 - Notify Join
/// <para>Notifies Battle.net you have joined a game.</para>
/// </summary>
public class NotifyJoin : BCPacket
{
	protected BattleNetClient client;

	protected uint version;

	protected string name;

	protected string password;

	public BattleNetClient Client => client;

	public uint Version => version;

	public string Name => name;

	public string Password => password;

	public NotifyJoin(byte[] data)
		: base(data)
	{
		client = (BattleNetClient)BitConverter.ToUInt32(data, 4);
		version = BitConverter.ToUInt32(data, 8);
		name = ByteConverter.GetNullString(data, 12);
		password = ByteConverter.GetNullString(data, 13 + name.Length);
	}
}
