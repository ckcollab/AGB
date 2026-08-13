using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x36 - Hire Mercenary
/// <para>Hire a mercenary from a town folk.</para>
/// </summary>
public class HireMercenary : GCPacket
{
	protected uint dealerUID;

	protected uint mercID;

	public uint DealerUID => dealerUID;

	public uint MercID => mercID;

	public HireMercenary(byte[] data)
		: base(data)
	{
		dealerUID = BitConverter.ToUInt32(data, 1);
		mercID = BitConverter.ToUInt32(data, 5);
	}

	public HireMercenary(uint dealerUID, uint mercID)
		: base(Build(dealerUID, mercID))
	{
		this.dealerUID = dealerUID;
		this.mercID = mercID;
	}

	public static byte[] Build(uint dealerUID, uint mercID)
	{
		return new byte[9]
		{
			54,
			(byte)dealerUID,
			(byte)(dealerUID >> 8),
			(byte)(dealerUID >> 16),
			(byte)(dealerUID >> 24),
			(byte)mercID,
			(byte)(mercID >> 8),
			(byte)(mercID >> 16),
			(byte)(mercID >> 24)
		};
	}
}
