using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x19 - Pick Item From Container
/// <para>Remove an item from a container and place it on cursor.</para>
/// </summary>
public class PickItemFromContainer : GCPacket
{
	protected uint uid;

	public uint UID => uid;

	public PickItemFromContainer(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
	}

	public PickItemFromContainer(uint uid)
		: base(Build(uid))
	{
		this.uid = uid;
	}

	public static byte[] Build(uint uid)
	{
		return new byte[5]
		{
			25,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
