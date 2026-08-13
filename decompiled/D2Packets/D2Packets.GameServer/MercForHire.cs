using System;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x4E - Merc For Hire
/// <para>Sent once for each merc available when interacting with a town folk selling mercs.</para>
/// </summary>
public class MercForHire : GSPacket
{
	protected int mercID;

	public int MercID => mercID;

	public string Unknown3 => ByteConverter.ToHexString(data, 3, 4);

	public MercForHire(byte[] data)
		: base(data)
	{
		mercID = BitConverter.ToUInt16(data, 1);
	}
}
