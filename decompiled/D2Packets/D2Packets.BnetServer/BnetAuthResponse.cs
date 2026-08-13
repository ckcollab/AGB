using System;
using ETUtils;

namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x51 - Bnet Auth Response
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.BnetClient.BnetAuthRequest" />
/// </remarks>
public class BnetAuthResponse : BSPacket
{
	protected BnetAuthResult result;

	protected string info;

	public BnetAuthResult Result => result;

	public string Info => info;

	public BnetAuthResponse(byte[] data)
		: base(data)
	{
		result = (BnetAuthResult)BitConverter.ToUInt32(data, 4);
		if (data.Length > 9)
		{
			info = ByteConverter.GetNullString(data, 8);
		}
	}
}
