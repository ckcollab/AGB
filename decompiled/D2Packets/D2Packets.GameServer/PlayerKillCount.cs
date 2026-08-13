using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x65 - Player Kill Count
/// <para>Updates the player's kill count for the current game.</para>
/// <para>Sent on game join with value of 0 and after each kill.</para>
/// </summary>
public class PlayerKillCount : GSPacket
{
	protected uint uid;

	protected short count;

	/// <summary>
	/// The UID of the player killer.
	/// </summary>
	public uint UID => uid;

	public short Count => count;

	public PlayerKillCount(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		count = BitConverter.ToInt16(data, 5);
	}
}
