using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x15 - Player Reassign
/// <para>Assigns a player or his pets at a location.</para>
/// <para>Sent when out of sync, joining game, teleporting or using a warp, portal or waypoint.</para>
/// </summary>
public class PlayerReassign : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected int x;

	protected int y;

	protected bool reassign;

	/// <summary>
	/// UnitType.Player if the information is about the player or UnitType.NPC if it's about his pets.
	/// <list type="bullet">
	/// <item>Offset: 1</item>
	/// <item>Length: BYTE</item>
	/// </list>
	/// </summary>
	public UnitType UnitType => unitType;

	/// <summary>
	/// UID of the player, even if information is for his pets.
	/// <list type="bullet">
	/// <item>Offset: 2</item>
	/// <item>Length: DWORD</item>
	/// </list>
	/// </summary>
	public uint UID => uid;

	/// <summary>
	/// <list type="bullet">
	/// <item>Offset: 6</item>
	/// <item>Length: WORD</item>
	/// </list>
	/// </summary>
	public int X => x;

	/// <summary>
	/// <list type="bullet">
	/// <item>Offset: 8</item>
	/// <item>Length: WORD</item>
	/// </list>
	/// </summary>
	public int Y => y;

	/// <summary>
	/// If reassign is false then this packet should be ignored ??
	/// <para>Actually seems to only control flashing...</para>
	/// <list type="bullet">
	/// <item>Offset: 10</item>
	/// <item>Length: BYTE</item>
	/// </list>
	/// </summary>
	public bool Reassign => reassign;

	public PlayerReassign(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		x = BitConverter.ToUInt16(data, 6);
		y = BitConverter.ToUInt16(data, 8);
		reassign = ((data[10] != 0) ? true : false);
	}

	public PlayerReassign(UnitType type, uint uid, int x, int y, bool reassign)
		: base(Build(type, uid, x, y, reassign))
	{
		unitType = type;
		this.uid = uid;
	}

	public static byte[] Build(UnitType type, uint uid, int x, int y, bool reassign)
	{
		return new byte[11]
		{
			21,
			(byte)type,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)x,
			(byte)(y >> 8),
			(byte)y,
			(byte)(y >> 8),
			(byte)(reassign ? 1u : 0u)
		};
	}
}
