using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x1D - Attribute Byte
/// <para>Notifies you of base attributes with a rating no higher than a single byte.</para>
/// </summary>
public class AttributeByte : AttributeNotification
{
	public static readonly bool WRAPPED = true;

	public AttributeByte(byte[] data)
		: base(data)
	{
		BaseStat baseStat = BaseStat.Get(data[1]);
		if (baseStat.Signed)
		{
			stat = new SignedStat(baseStat, data[2]);
		}
		else
		{
			stat = new UnsignedStat(baseStat, data[2]);
		}
	}
}
