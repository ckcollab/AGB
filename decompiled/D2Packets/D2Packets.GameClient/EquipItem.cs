using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x1A - Equip Item
/// <para>Equip an item on the cursor to a specified body location.</para>
/// <para>Item must be on cursor, of right type for location and location must be empty.</para>
/// </summary>
public class EquipItem : GCPacket
{
	protected uint uid;

	protected EquipmentLocation location;

	public uint UID => uid;

	public EquipmentLocation Location => location;

	public EquipItem(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		location = (EquipmentLocation)data[5];
	}

	public EquipItem(uint uid, EquipmentLocation location)
		: base(Build(uid, location))
	{
		this.uid = uid;
		this.location = location;
	}

	public static byte[] Build(uint uid, EquipmentLocation location)
	{
		return new byte[9]
		{
			26,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)location,
			0,
			0,
			0
		};
	}
}
