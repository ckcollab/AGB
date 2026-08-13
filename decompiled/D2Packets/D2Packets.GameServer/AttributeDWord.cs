using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x1F - Attribute DWord
/// <para>Notifies you of base attributes with a rating no higher than four bytes.</para>
/// </summary>
public class AttributeDWord : AttributeNotification
{
	public static readonly bool WRAPPED = true;

	public AttributeDWord(byte[] data)
		: base(data)
	{
		BaseStat baseStat = BaseStat.Get(data[1]);
		int val = BitConverter.ToInt32(data, 2);
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
