using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x17 - DropItem
/// <para>Drop the cursor item to the ground.</para>
/// </summary>
public class DropItem : GCPacket
{
	protected uint uid;

	public uint UID => uid;

	public DropItem(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
	}

	public DropItem(uint uid)
		: base(Build(uid))
	{
		this.uid = uid;
	}

	public static byte[] Build(uint uid)
	{
		return new byte[5]
		{
			23,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
