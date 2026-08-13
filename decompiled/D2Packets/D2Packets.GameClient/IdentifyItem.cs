using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x27 - Identify Item
/// <para>Use an identify scroll on an item.</para>
/// <para>If already indentified, id cursor is canceled and scroll is not used.</para>
/// <para>If not clicking on an item, UID is that of scroll and action is canceled.</para>
/// </summary>
public class IdentifyItem : GCPacket
{
	protected uint itemUID;

	protected uint scrollUID;

	public uint ItemUID => itemUID;

	public uint ScrollUID => scrollUID;

	public IdentifyItem(byte[] data)
		: base(data)
	{
		itemUID = BitConverter.ToUInt32(data, 1);
		scrollUID = BitConverter.ToUInt32(data, 5);
	}

	public IdentifyItem(uint itemUID, uint scrollUID)
		: base(Build(itemUID, scrollUID))
	{
		this.itemUID = itemUID;
		this.scrollUID = scrollUID;
	}

	public static byte[] Build(uint itemUID, uint scrollUID)
	{
		return new byte[9]
		{
			39,
			(byte)itemUID,
			(byte)(itemUID >> 8),
			(byte)(itemUID >> 16),
			(byte)(itemUID >> 24),
			(byte)scrollUID,
			(byte)(scrollUID >> 8),
			(byte)(scrollUID >> 16),
			(byte)(scrollUID >> 24)
		};
	}
}
