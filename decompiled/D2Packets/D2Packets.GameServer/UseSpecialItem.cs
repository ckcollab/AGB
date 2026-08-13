using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x7C - Use Special Item
/// <para>Only known when identify / portal scroll / tome is used.</para>
/// <para>This is sent twice, with an UpdateContainerItem in between...</para>
/// </summary>
public class UseSpecialItem : GSPacket
{
	protected SpecialItemType action;

	protected uint uid;

	public SpecialItemType Action => action;

	public uint UID => uid;

	public UseSpecialItem(byte[] data)
		: base(data)
	{
		action = (SpecialItemType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
	}

	public UseSpecialItem(SpecialItemType action, uint uid)
		: base(Build(action, uid))
	{
		this.action = action;
		this.uid = uid;
	}

	public static byte[] Build(SpecialItemType action, uint uid)
	{
		return new byte[6]
		{
			124,
			(byte)action,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
