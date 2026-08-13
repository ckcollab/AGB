using System;
using ETUtils;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x26 - Use Belt Item
/// <para>Consume an item (potion or scroll) in belt.</para>
/// </summary>
public class UseBeltItem : GCPacket
{
	protected uint uid;

	protected bool toMerc;

	public bool ToMerc => toMerc;

	public uint UID => uid;

	public string Unknown9 => ByteConverter.ToHexString(data, 9, 4);

	public UseBeltItem(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		if (BitConverter.ToUInt32(data, 5) == 1)
		{
			toMerc = true;
		}
	}

	public UseBeltItem(uint uid, bool toMerc)
		: base(Build(uid, toMerc))
	{
		this.uid = uid;
		this.toMerc = toMerc;
	}

	public UseBeltItem(uint uid, bool toMerc, uint unknown9)
		: base(Build(uid, toMerc, unknown9))
	{
		this.uid = uid;
		this.toMerc = toMerc;
	}

	public static byte[] Build(uint itemUID, bool toMerc)
	{
		return Build(itemUID, toMerc, 0u);
	}

	public static byte[] Build(uint itemUID, bool toMerc, uint unknown9)
	{
		return new byte[13]
		{
			38,
			(byte)itemUID,
			(byte)(itemUID >> 8),
			(byte)(itemUID >> 16),
			(byte)(itemUID >> 24),
			(byte)(toMerc ? 1u : 0u),
			0,
			0,
			0,
			(byte)unknown9,
			(byte)(unknown9 >> 8),
			(byte)(unknown9 >> 16),
			(byte)(unknown9 >> 24)
		};
	}
}
