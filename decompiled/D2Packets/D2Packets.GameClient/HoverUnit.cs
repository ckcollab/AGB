using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x3D - Hover Unit
/// <para>Notifies the server client want's to be informed of special mode changes for unit.</para>
/// <para>This is used to update doors' text to "Blocked Door" when a unit passes over it...</para>
/// </summary>
public class HoverUnit : GCPacket
{
	protected uint uid;

	public uint UID => uid;

	public HoverUnit(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
	}

	public HoverUnit(uint uid)
		: base(Build(uid))
	{
		this.uid = uid;
	}

	public static byte[] Build(uint uid)
	{
		return new byte[5]
		{
			61,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
