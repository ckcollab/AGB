using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x38 - Town Folk Menu Select
/// <para>Choose an menu item / action type once interacted with the town folk.</para>
/// </summary>
public class TownFolkMenuSelect : GCPacket
{
	protected TownFolkMenuItem selection;

	protected uint uid;

	public TownFolkMenuItem Selection => selection;

	public uint UID => uid;

	public string Unknown9 => ByteConverter.ToHexString(data, 9, 4);

	public TownFolkMenuSelect(byte[] data)
		: base(data)
	{
		selection = (TownFolkMenuItem)BitConverter.ToUInt32(data, 1);
		uid = BitConverter.ToUInt32(data, 5);
	}

	public TownFolkMenuSelect(TownFolkMenuItem selection, uint uid, uint unknown9)
		: base(Build(selection, uid, unknown9))
	{
		this.selection = selection;
		this.uid = uid;
	}

	public static byte[] Build(TownFolkMenuItem selection, uint uid, uint unknown9)
	{
		return new byte[13]
		{
			56,
			(byte)selection,
			(byte)((uint)selection >> 8),
			(byte)((uint)selection >> 16),
			(byte)((uint)selection >> 24),
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)unknown9,
			(byte)(unknown9 >> 8),
			(byte)(unknown9 >> 16),
			(byte)(unknown9 >> 24)
		};
	}
}
