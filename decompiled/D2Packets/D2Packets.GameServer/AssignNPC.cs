using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0xAC - Assign NPC
/// <para>An NPC is being assigned to a location with in your range of view.</para>
/// </summary>
public class AssignNPC : GSPacket
{
	protected uint uid;

	protected NPCClass id;

	protected int x;

	protected int y;

	protected byte life;

	public uint UID => uid;

	public NPCClass ID => id;

	public int X => x;

	public int Y => y;

	public byte Life => life;

	public string Unknown13 => ByteConverter.ToHexString(data, 13);

	public AssignNPC(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		id = (NPCClass)BitConverter.ToUInt16(data, 5);
		x = BitConverter.ToUInt16(data, 7);
		y = BitConverter.ToUInt16(data, 9);
		life = data[11];
	}
}
