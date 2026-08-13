using System;
using ETUtils;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x3E - Realm Logon Request
/// <para>Request Realm server information to establish a connection.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.BnetServer.RealmLogonResponse" />
/// </remarks>
public class RealmLogonRequest : BCPacket
{
	protected uint cookie;

	protected string realm;

	public string Realm => realm;

	public uint Cookie => cookie;

	public RealmLogonRequest(byte[] data)
		: base(data)
	{
		cookie = BitConverter.ToUInt32(data, 4);
		realm = ByteConverter.GetNullString(data, 28);
	}
}
