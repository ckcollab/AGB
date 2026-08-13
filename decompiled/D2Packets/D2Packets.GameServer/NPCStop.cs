using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x6D - NPC Stop
/// <para>A NPC needs to be redrawn in a stand still state.</para>
/// <para>This is a very common packet when a computer player does any form of move, as afterwards
/// (for a split second in most case's) the computer player must be drawn in a stand still state.</para>
/// </summary>
public class NPCStop : GSPacket
{
	protected uint uid;

	protected int x;

	protected int y;

	protected byte life;

	public uint UID => uid;

	public int X => x;

	public int Y => y;

	public byte Life => life;

	public NPCStop(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		x = BitConverter.ToUInt16(data, 5);
		y = BitConverter.ToUInt16(data, 7);
		life = data[9];
	}
}
