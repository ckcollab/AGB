using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x6B - NPC Action
/// <para>A NPC does a action at a location.</para>
/// </summary>
public class NPCAction : GSPacket
{
	protected uint uid;

	protected ushort actionType;

	protected int x;

	protected int y;

	public uint UID => uid;

	/// <summary>
	/// A few action types:
	/// <para>Warriv:    0x0C = has a strech</para>
	/// <para>Charsi:    0x0C = Cast/Hit a wepon</para>
	/// </summary>
	public ushort ActionType => actionType;

	public int X => x;

	public int Y => y;

	public NPCAction(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		actionType = data[5];
		x = BitConverter.ToUInt16(data, 12);
		y = BitConverter.ToUInt16(data, 14);
	}
}
