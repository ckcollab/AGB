using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x1F - Swap Container Item
/// <para>Swap cursor (<see cref="P:D2Packets.GameClient.SwapContainerItem.SubjectUID" />) 
/// item with another item in a container (<see cref="P:D2Packets.GameClient.SwapContainerItem.ObjectUID" />).</para>
/// <para>Don't send this packet to replace items in trade as trade doesn't support item replacement.</para>
/// </summary>
public class SwapContainerItem : GCPacket
{
	protected uint subjectUID;

	protected uint objectUID;

	protected int x;

	protected int y;

	public uint SubjectUID => subjectUID;

	public uint ObjectUID => objectUID;

	public int X => x;

	public int Y => y;

	public SwapContainerItem(byte[] data)
		: base(data)
	{
		subjectUID = BitConverter.ToUInt32(data, 1);
		objectUID = BitConverter.ToUInt32(data, 5);
		x = BitConverter.ToInt32(data, 9);
		y = BitConverter.ToInt32(data, 13);
	}

	public SwapContainerItem(uint subjectUID, uint objectUID, int x, int y)
		: base(Build(subjectUID, objectUID, x, y))
	{
		this.subjectUID = subjectUID;
		this.objectUID = objectUID;
		this.x = x;
		this.y = y;
	}

	public static byte[] Build(uint subjectUID, uint objectUID, int x, int y)
	{
		return new byte[17]
		{
			31,
			(byte)subjectUID,
			(byte)(subjectUID >> 8),
			(byte)(subjectUID >> 16),
			(byte)(subjectUID >> 24),
			(byte)objectUID,
			(byte)(objectUID >> 8),
			(byte)(objectUID >> 16),
			(byte)(objectUID >> 24),
			(byte)x,
			(byte)(x >> 8),
			(byte)(x >> 16),
			(byte)(x >> 24),
			(byte)y,
			(byte)(y >> 8),
			(byte)(y >> 16),
			(byte)(y >> 24)
		};
	}
}
