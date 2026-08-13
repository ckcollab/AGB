using ETUtils;

namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x4A - Extra Work Info
/// <para>Specifies the "ExtraWork" file that the client should download / run.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.BnetClient.ExtraWorkResponse" />
/// </remarks>
public class ExtraWorkInfo : BSPacket
{
	protected string filename;

	public string Filename => filename;

	public ExtraWorkInfo(byte[] data)
		: base(data)
	{
		filename = ByteConverter.GetNullString(data, 4);
	}
}
