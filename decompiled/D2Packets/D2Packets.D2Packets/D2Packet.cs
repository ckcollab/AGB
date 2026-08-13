using System;
using System.Reflection;
using System.Text;
using ETUtils;

namespace D2Packets.D2Packets;

/// <summary>
/// Base class for Diablo II Packets
/// </summary>
public class D2Packet
{
	protected int packetID;

	protected PacketOrigin origin;

	protected byte[] data;

	protected bool writable;

	protected bool blocked;

	public static bool IncludeType = false;

	public static string NameValueSeparator = ": ";

	public static string ItemSeparator = "; ";

	public static string StartSeparator = ItemSeparator;

	public static string ItemFormat = "{0}: {1}";

	public static string LongItemSeparator = Environment.NewLine + "         ";

	public static string LongStartSeparator = LongItemSeparator;

	public static string LongItemFormat = "{0,-20}: {1}";

	/// <summary>
	/// The source of the packet: which of the 3 connections it applies to and whether it was sent by the server or client.
	/// </summary>
	public PacketOrigin Origin => origin;

	public int PacketID => packetID;

	/// <summary>
	/// The byte array containing the packet's raw data.
	/// </summary>
	public byte[] Data => data;

	/// <summary>
	/// If false, the packet can no longer be modified.
	/// </summary>
	public bool Writable => writable;

	/// <summary>
	/// If true, the packet will not be sent to the server.
	/// </summary>
	public bool Blocked => blocked;

	public D2Packet(byte[] data, PacketOrigin origin)
	{
		this.data = data;
		this.origin = origin;
		writable = true;
		blocked = false;
	}

	/// <summary>
	/// Sets <see cref="P:D2Packets.D2Packets.D2Packet.Writable" /> to false, preventing further modifications to the packet.
	/// </summary>
	public void Complete()
	{
		writable = false;
	}

	/// <summary>
	/// Sets <see cref="P:D2Packets.D2Packets.D2Packet.Blocked" /> to true, preventing the packet from reaching the server.
	/// </summary>
	public void Block()
	{
		blocked = true;
	}

	public void IsWritableEx()
	{
		if (!writable)
		{
			throw new PacketNotWritableException();
		}
	}

	public string ToDataString()
	{
		return ByteConverter.ToHexString(data);
	}

	public string ToLongDataString()
	{
		return ByteConverter.ToFormatedHexString(data);
	}

	public string ToLongInfoString()
	{
		return ToInfoString(IncludeType, LongItemFormat, LongItemSeparator, LongStartSeparator);
	}

	public string ToLongInfoString(bool includeType)
	{
		return ToInfoString(includeType, LongItemFormat, LongItemSeparator, LongStartSeparator);
	}

	public string ToLongInfoString(bool includeType, string itemFormat)
	{
		return ToInfoString(includeType, itemFormat, LongItemSeparator, LongStartSeparator);
	}

	public string ToInfoString()
	{
		return ToInfoString(IncludeType, ItemFormat, ItemSeparator, StartSeparator);
	}

	public string ToInfoString(bool includeType)
	{
		return ToInfoString(includeType, ItemFormat, ItemSeparator, StartSeparator);
	}

	public string ToInfoString(bool includeType, string itemFormat)
	{
		return ToInfoString(includeType, itemFormat, ItemSeparator, StartSeparator);
	}

	public string ToInfoString(bool includeType, string itemFormat, string itemSeparator)
	{
		return ToInfoString(includeType, itemSeparator, itemFormat, itemSeparator);
	}

	public string ToInfoString(bool includeType, string itemFormat, string itemSeparator, string startSeparator)
	{
		Type type = GetType();
		if ((object)type.BaseType == typeof(D2Packet))
		{
			return null;
		}
		StringBuilder sb = new StringBuilder();
		if (includeType)
		{
			sb.Append(type.BaseType!.Namespace);
			sb.Append(".");
			sb.Append(type.Name);
		}
		else
		{
			sb.Append(type.Name);
		}
		sb.Append(startSeparator);
		int inherited = (((object)type.GetField("WRAPPED", BindingFlags.Static | BindingFlags.Public) != null) ? 1 : 0);
		sb.Append(StringUtils.ToInfoString((object)this, inherited, false, includeType, itemFormat, itemSeparator));
		return sb.ToString();
	}
}
