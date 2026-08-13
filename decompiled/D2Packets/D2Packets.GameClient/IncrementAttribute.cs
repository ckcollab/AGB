using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x3A - Increment Attribute
/// <para>Raise an attribute by one point.</para>
/// </summary>
public class IncrementAttribute : GCPacket
{
	protected StatType attribute;

	public StatType Attribute => attribute;

	public IncrementAttribute(byte[] data)
		: base(data)
	{
		attribute = (StatType)BitConverter.ToUInt16(data, 1);
	}

	public IncrementAttribute(StatType attribute)
		: base(Build(attribute))
	{
		this.attribute = attribute;
	}

	public static byte[] Build(StatType attribute)
	{
		return new byte[3]
		{
			58,
			(byte)attribute,
			(byte)((ushort)attribute >> 8)
		};
	}
}
