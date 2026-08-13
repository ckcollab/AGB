using System;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x67 - NPC Move
/// <para>An NPC is moving to a new location in range of view.</para>
/// </summary>
public class NPCMove : GSPacket
{
	protected uint uid;

	protected byte unknown5;

	protected int x;

	protected int y;

	public uint UID => uid;

	public int X => x;

	public int Y => y;

	public byte Unknown5 => unknown5;

	public string Unknown10 => ByteConverter.ToHexString(data, 10, 2);

	public string Unknown12 => ByteConverter.ToHexString(data, 12, 4);

	public NPCMove(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		unknown5 = data[5];
		x = BitConverter.ToUInt16(data, 6);
		y = BitConverter.ToUInt16(data, 8);
	}
}
