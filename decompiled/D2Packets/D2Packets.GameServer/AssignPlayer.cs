using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x59 - Assign Player
/// <para>Notifies that a player is standing/coming into your range of view.</para>
/// <para>X and Y will be null if player is the receiving player.</para>
/// </summary>
public class AssignPlayer : GSPacket
{
	protected uint uid;

	protected CharacterClass charClass;

	protected string name;

	protected int x;

	protected int y;

	public uint UID => uid;

	public CharacterClass Class => charClass;

	public string Name => name;

	public int X => x;

	public int Y => y;

	public AssignPlayer(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		charClass = (CharacterClass)data[5];
		name = ByteConverter.GetNullString(data, 6, 16);
		x = BitConverter.ToUInt16(data, 22);
		y = BitConverter.ToUInt16(data, 24);
	}
}
