using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x69 - Set NPC Mode
/// <para>Sets an NPC's state when it comes into view or updates it afterwards.</para>
/// </summary>
public class SetNPCMode : GSPacket
{
	protected uint uid;

	protected NPCMode mode;

	protected int x;

	protected int y;

	protected byte life;

	protected byte unknown11;

	public uint UID => uid;

	public NPCMode Mode => mode;

	public int X => x;

	public int Y => y;

	public byte Life => life;

	public byte Unknown11 => unknown11;

	public SetNPCMode(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		mode = (NPCMode)data[5];
		x = BitConverter.ToUInt16(data, 6);
		y = BitConverter.ToUInt16(data, 8);
		life = data[10];
		unknown11 = data[11];
	}
}
