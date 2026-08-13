using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x9F - Merc Attribute Word
/// <para>Notifies of a mercenary's single base attribute as a word.</para>
/// </summary>
public class MercAttributeWord : MercAttributeNotification
{
	public static readonly bool WRAPPED = true;

	public MercAttributeWord(byte[] data)
		: base(data)
	{
		BaseStat baseStat = BaseStat.Get(data[1]);
		int val = ((data[6] == 0) ? data[7] : BitConverter.ToUInt16(data, 6));
		if (baseStat.Signed)
		{
			stat = new SignedStat(baseStat, val);
		}
		else
		{
			stat = new UnsignedStat(baseStat, (uint)val);
		}
	}
}
