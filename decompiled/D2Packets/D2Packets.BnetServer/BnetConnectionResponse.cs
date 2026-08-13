using System;
using ETUtils;

namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x50 - Bnet Connection Response
/// <para>Response to Bnet Connection Request.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.BnetClient.BnetConnectionRequest" />
/// </remarks>
public class BnetConnectionResponse : BSPacket
{
	protected uint logonType;

	protected int serverToken;

	protected uint udpValue;

	protected DateTime versionFileTime;

	protected string versionFileName;

	protected string versionFormulae;

	public uint LogonType => logonType;

	public int ServerToken => serverToken;

	public uint UDPValue => udpValue;

	public DateTime VersionFileTime => versionFileTime;

	public string VersionFileName => versionFileName;

	public string VersionFormulae => versionFormulae;

	public BnetConnectionResponse(byte[] data)
		: base(data)
	{
		logonType = BitConverter.ToUInt32(data, 4);
		serverToken = BitConverter.ToInt32(data, 8);
		udpValue = BitConverter.ToUInt32(data, 12);
		versionFileTime = DateTime.FromFileTimeUtc(BitConverter.ToInt64(data, 16));
		versionFileName = ByteConverter.GetNullString(data, 24);
		versionFormulae = ByteConverter.GetNullString(data, 25 + VersionFileName.Length);
	}
}
