using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x25 - Swap Belt Item
/// <para>Place cursor item belt and replace it with the specified item in it's location. </para>
/// </summary>
public class SwapBeltItem : GCPacket
{
	protected readonly uint oldItemUID;

	protected readonly uint newItemUID;

	public uint OldItemUID => oldItemUID;

	public uint NewItemUID => newItemUID;

	public SwapBeltItem(byte[] data)
		: base(data)
	{
		oldItemUID = BitConverter.ToUInt32(data, 1);
		newItemUID = BitConverter.ToUInt32(data, 5);
	}

	public SwapBeltItem(uint oldItemUID, uint newItemUID)
		: base(Build(oldItemUID, newItemUID))
	{
		this.oldItemUID = oldItemUID;
		this.newItemUID = newItemUID;
	}

	public static byte[] Build(uint oldItemUID, uint newItemUID)
	{
		return new byte[9]
		{
			37,
			(byte)oldItemUID,
			(byte)(oldItemUID >> 8),
			(byte)(oldItemUID >> 16),
			(byte)(oldItemUID >> 24),
			(byte)newItemUID,
			(byte)(newItemUID >> 8),
			(byte)(newItemUID >> 16),
			(byte)(newItemUID >> 24)
		};
	}
}
