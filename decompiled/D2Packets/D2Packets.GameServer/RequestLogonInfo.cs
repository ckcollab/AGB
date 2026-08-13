namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0xAF - Request Logon Info
/// <para>First packet sent by Game Server when connection is established. Sent uncompressed !</para>
/// </summary>
public class RequestLogonInfo : GSPacket
{
	protected byte protocolVersion;

	public byte ProtocolVersion => protocolVersion;

	public RequestLogonInfo(byte[] data)
		: base(data)
	{
		protocolVersion = data[1];
	}
}
