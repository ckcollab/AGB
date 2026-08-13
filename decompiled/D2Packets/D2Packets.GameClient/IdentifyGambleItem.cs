using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x37 - Identify Gamble Item
/// <para>Identify a gambled item after you bought it.</para>
/// </summary>
public class IdentifyGambleItem : GCPacket
{
	protected uint uid;

	public uint UID => uid;

	public IdentifyGambleItem(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
	}

	public IdentifyGambleItem(uint uid)
		: base(Build(uid))
	{
		this.uid = uid;
	}

	public static byte[] Build(uint uid)
	{
		return new byte[5]
		{
			55,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
