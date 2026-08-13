using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0xA0 - Merc Attribute DWord
/// <para>Notifies of a mercenary's single base attribute as a DWord.</para>
/// </summary>
public class MercAttributeDWord : MercAttributeNotification
{
	public static readonly bool WRAPPED = true;

	public MercAttributeDWord(byte[] data)
		: base(data)
	{
		BaseStat baseStat = BaseStat.Get(data[1]);
		int val = BitConverter.ToInt32(data, 6);
		if (baseStat.ValShift > 0)
		{
			val >>= baseStat.ValShift;
		}
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
