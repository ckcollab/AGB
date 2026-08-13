using System;
using D2Packets.D2Packets;

namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x40 - Query Realms Response
/// <para>Contains a list of available realms.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.BnetClient.QueryRealms" />
/// </remarks>
public class QueryRealmsResponse : BSPacket
{
	protected uint unknown;

	protected uint count;

	protected RealmInfo[] realms;

	public uint Count => count;

	/// <summary>
	/// Typically 0...
	/// </summary>
	public uint Unknown => unknown;

	public RealmInfo[] Realms => realms;

	public QueryRealmsResponse(byte[] data)
		: base(data)
	{
		unknown = BitConverter.ToUInt32(data, 4);
		count = BitConverter.ToUInt32(data, 8);
		realms = new RealmInfo[count];
		int offset = 12;
		for (int i = 0; i < count; i++)
		{
			ref RealmInfo reference = ref realms[i];
			reference = new RealmInfo(data, offset);
			offset += 6 + realms[i].Name.Length + realms[i].Description.Length;
		}
	}
}
