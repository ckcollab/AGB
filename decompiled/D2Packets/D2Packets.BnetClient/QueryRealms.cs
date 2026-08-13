namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x40 - Query Realms
/// <para>Request a list of realms.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.BnetServer.QueryRealmsResponse" />
/// </remarks>
public class QueryRealms : BCPacket
{
	public QueryRealms(byte[] data)
		: base(data)
	{
	}
}
