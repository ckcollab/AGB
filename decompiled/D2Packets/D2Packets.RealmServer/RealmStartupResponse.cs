using System;

namespace D2Packets.RealmServer;

/// <summary>
/// Realm Server Packet 0x01 - Realm Startup Response
/// <para>Notifies client of the connection startup result.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.RealmClient.RealmStartupRequest" />
/// </remarks>
public class RealmStartupResponse : RSPacket
{
	protected RealmStartupResult result;

	public RealmStartupResult Result => result;

	public RealmStartupResponse(byte[] data)
		: base(data)
	{
		result = (RealmStartupResult)BitConverter.ToUInt32(data, 3);
	}
}
