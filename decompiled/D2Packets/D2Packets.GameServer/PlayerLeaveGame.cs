using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x5C - Player Leave Game
/// <para>Sent when another player leaves the game.</para>
/// </summary>
public class PlayerLeaveGame : GSPacket
{
	protected uint uid;

	public uint UID => uid;

	public PlayerLeaveGame(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
	}

	public PlayerLeaveGame(uint uid)
		: base(Build(uid))
	{
		this.uid = uid;
	}

	public static byte[] Build(uint uid)
	{
		return new byte[5]
		{
			92,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
