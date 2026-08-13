using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x62 - Resurrect Merc
/// <para>Resurrect your current mercenary at a slaver town folk.</para>
/// </summary>
public class ResurrectMerc : GCPacket
{
	protected uint dealerUID;

	public uint DealerUID => dealerUID;

	public ResurrectMerc(byte[] data)
		: base(data)
	{
		dealerUID = BitConverter.ToUInt32(data, 1);
	}

	public ResurrectMerc(uint dealerUID)
		: base(Build(dealerUID))
	{
		this.dealerUID = dealerUID;
	}

	public static byte[] Build(uint dealerUID)
	{
		return new byte[5]
		{
			98,
			(byte)dealerUID,
			(byte)(dealerUID >> 8),
			(byte)(dealerUID >> 16),
			(byte)(dealerUID >> 24)
		};
	}
}
