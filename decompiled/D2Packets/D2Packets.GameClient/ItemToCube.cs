using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x2A - Item To Cube
/// <para>Drop an item in the Horadric cube by clicking on it with the item on cursor.</para>
/// <para>Note that a regular ItemToContainer packet is used if the item is dropped at a location in the cube and not on it.</para>
/// </summary>
public class ItemToCube : GCPacket
{
	protected uint itemUID;

	protected uint cubeUID;

	public uint ItemUID => itemUID;

	public uint CubeUID => cubeUID;

	public ItemToCube(byte[] data)
		: base(data)
	{
		itemUID = BitConverter.ToUInt32(data, 1);
		cubeUID = BitConverter.ToUInt32(data, 5);
	}

	public ItemToCube(uint itemUID, uint cubeUID)
		: base(Build(itemUID, cubeUID))
	{
		this.itemUID = itemUID;
		this.cubeUID = cubeUID;
	}

	public static byte[] Build(uint itemUID, uint cubeUID)
	{
		return new byte[9]
		{
			42,
			(byte)itemUID,
			(byte)(itemUID >> 8),
			(byte)(itemUID >> 16),
			(byte)(itemUID >> 24),
			(byte)cubeUID,
			(byte)(cubeUID >> 8),
			(byte)(cubeUID >> 16),
			(byte)(cubeUID >> 24)
		};
	}
}
