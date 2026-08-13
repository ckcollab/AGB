using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x9E - Merc Attribute Byte
/// <para>Notifies of a mercenary's single base attribute as a single byte.</para>
/// </summary>
public class MercAttributeByte : MercAttributeNotification
{
	public static readonly bool WRAPPED = true;

	public MercAttributeByte(byte[] data)
		: base(data)
	{
		BaseStat baseStat = BaseStat.Get(data[1]);
		if (baseStat.Signed)
		{
			stat = new SignedStat(baseStat, data[6]);
		}
		else
		{
			stat = new UnsignedStat(baseStat, data[6]);
		}
	}
}
