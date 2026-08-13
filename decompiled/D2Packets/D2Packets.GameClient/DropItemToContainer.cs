using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x18 - Drop Item To Container
/// <para>Drop an item from cursor into a container.</para>
/// </summary>
public class DropItemToContainer : GCPacket
{
	protected uint uid;

	protected int x;

	protected int y;

	protected ItemContainerGC container;

	public uint UID => uid;

	public ItemContainerGC Container => container;

	public int X => x;

	public int Y => y;

	public DropItemToContainer(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		x = data[5];
		y = data[9];
		container = (ItemContainerGC)data[13];
	}

	public DropItemToContainer(uint uid, ItemContainerGC container, int x, int y)
		: base(Build(uid, container, x, y))
	{
		this.uid = uid;
		this.container = container;
		this.x = x;
		this.y = y;
	}

	public static byte[] Build(uint uid, ItemContainerGC container, int x, int y)
	{
		return new byte[17]
		{
			24,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)x,
			0,
			0,
			0,
			(byte)y,
			0,
			0,
			0,
			(byte)container,
			0,
			0,
			0
		};
	}
}
