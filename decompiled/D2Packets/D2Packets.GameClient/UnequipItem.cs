using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x1C - Unequip Item
/// <para>Removes an equipped item from body and places on the cursor.</para>
/// </summary>
public class UnequipItem : GCPacket
{
	protected readonly EquipmentLocation location;

	public EquipmentLocation Location => location;

	public UnequipItem(byte[] data)
		: base(data)
	{
		location = (EquipmentLocation)data[1];
	}

	public UnequipItem(EquipmentLocation location)
		: base(Build(location))
	{
		this.location = location;
	}

	public static byte[] Build(EquipmentLocation location)
	{
		return new byte[3]
		{
			28,
			(byte)location,
			0
		};
	}
}
