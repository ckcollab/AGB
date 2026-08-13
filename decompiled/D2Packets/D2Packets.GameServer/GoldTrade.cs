using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x79 - Gold Trade
/// <para>Updates the amount of gold the trading partner is offering.</para>
/// <para>Will be sent with a value of 0 when entering trade and every time the the amount or trade status is changing.</para>
/// </summary>
public class GoldTrade : GSPacket
{
	protected bool myGold;

	protected uint amount;

	public bool MyGold => myGold;

	public uint Amount => amount;

	public GoldTrade(byte[] data)
		: base(data)
	{
		myGold = BitConverter.ToBoolean(data, 1);
		amount = BitConverter.ToUInt32(data, 2);
	}
}
