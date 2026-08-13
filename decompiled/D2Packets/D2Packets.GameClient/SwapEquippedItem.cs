using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x1D - Swap Equipped Item
/// <para>Swaps an equipped item with another one on cursor.</para>
/// </summary>
public class SwapEquippedItem : GCPacket
{
	protected uint uid;

	protected EquipmentLocation location;

	public uint UID => uid;

	public EquipmentLocation Location => location;

	public SwapEquippedItem(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		location = (EquipmentLocation)data[5];
	}

	public SwapEquippedItem(uint uid, EquipmentLocation location)
		: base(Build(uid, location))
	{
		this.uid = uid;
		this.location = location;
	}

	public static byte[] Build(uint uid, EquipmentLocation location)
	{
		return new byte[9]
		{
			29,
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
