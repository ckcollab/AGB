using System;
using ETUtils;

namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x3A - Bnet Logon Response
/// <para>Notifies client of a Bnet logon request result.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.BnetClient.BnetLogonRequest" />
/// </remarks>
public class BnetLogonResponse : BSPacket
{
	protected BnetLogonResult result;

	protected string reason;

	public BnetLogonResult Result => result;

	public string Reason => reason;

	public BnetLogonResponse(byte[] data)
		: base(data)
	{
		result = (BnetLogonResult)BitConverter.ToUInt32(data, 4);
		if (data.Length > 8)
		{
			reason = ByteConverter.GetNullString(data, 8);
		}
	}
}
