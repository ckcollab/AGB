using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x61 - Change Merc Equipment
/// <para>Equip or unequip merc item.</para>
/// </summary>
public class ChangeMercEquipment : GCPacket
{
	protected EquipmentLocation location;

	protected bool unequip = false;

	public EquipmentLocation Location => location;

	public bool Unequip => unequip;

	public ChangeMercEquipment(byte[] data)
		: base(data)
	{
		location = (EquipmentLocation)BitConverter.ToUInt16(data, 1);
		if (location != 0)
		{
			unequip = true;
		}
	}

	public ChangeMercEquipment(EquipmentLocation location)
		: base(Build(location))
	{
		this.location = location;
		if (location != 0)
		{
			unequip = true;
		}
	}

	public static byte[] Build(EquipmentLocation location)
	{
		return new byte[3]
		{
			97,
			(byte)location,
			(byte)((ushort)location >> 8)
		};
	}
}
