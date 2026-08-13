using System;
using ETUtils;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x3A - Bnet Logon Request
/// <para>Actual logon request containing username and password.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.BnetServer.BnetLogonResponse" />
/// </remarks>
public class BnetLogonRequest : BCPacket
{
	protected uint clientToken;

	protected uint serverToken;

	protected string username;

	public uint ClientToken => clientToken;

	public uint ServerToken => serverToken;

	public string Username => username;

	public BnetLogonRequest(byte[] data)
		: base(data)
	{
		clientToken = BitConverter.ToUInt32(data, 4);
		serverToken = BitConverter.ToUInt32(data, 8);
		username = ByteConverter.GetNullString(data, 32);
	}
}
