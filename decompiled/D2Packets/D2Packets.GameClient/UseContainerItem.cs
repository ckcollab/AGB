using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x20 - Use Container Item
/// <para>Use an item (drink potion, use tome, open cube, etc.) in inventory.</para>
/// <para><see cref="P:D2Packets.GameClient.UseContainerItem.MeX" /> and <see cref="P:D2Packets.GameClient.UseContainerItem.MeY" /> 
/// are your standing location for the area you are in, not item position in inventory !</para>
/// </summary>
public class UseContainerItem : GCPacket
{
	protected uint itemUID;

	protected int meX;

	protected int meY;

	public uint ItemUID => itemUID;

	public int MeX => meX;

	public int MeY => meY;

	public UseContainerItem(byte[] data)
		: base(data)
	{
		itemUID = BitConverter.ToUInt32(data, 1);
		meX = BitConverter.ToInt32(data, 5);
		meY = BitConverter.ToInt32(data, 9);
	}

	public UseContainerItem(uint itemUID, int meX, int meY)
		: base(Build(itemUID, meX, meY))
	{
		this.itemUID = itemUID;
		this.meX = meX;
		this.meY = meY;
	}

	public static byte[] Build(uint itemUID, int meX, int meY)
	{
		return new byte[13]
		{
			32,
			(byte)itemUID,
			(byte)(itemUID >> 8),
			(byte)(itemUID >> 16),
			(byte)(itemUID >> 24),
			(byte)meX,
			(byte)(meX >> 8),
			(byte)(meX >> 16),
			(byte)(meX >> 24),
			(byte)meY,
			(byte)(meY >> 8),
			(byte)(meY >> 16),
			(byte)(meY >> 24)
		};
	}
}
