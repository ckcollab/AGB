using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x34 - Cain Identify Items
/// <para>Select the Identify Items option when interacted with Cain.</para>
/// </summary>
public class CainIdentifyItems : GCPacket
{
	protected uint uid;

	public uint UID => uid;

	public CainIdentifyItems(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
	}

	public CainIdentifyItems(uint uid)
		: base(Build(uid))
	{
		this.uid = uid;
	}

	public static byte[] Build(uint uid)
	{
		return new byte[5]
		{
			52,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
