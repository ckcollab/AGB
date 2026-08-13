using System;
using ETUtils;

namespace D2Packets.RealmServer;

/// <summary>
/// Realm Server Packet 0x05 - Game List
/// <para>Adds an available game to list (sent once for each game.)</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.RealmClient.GameListRequest" />
/// </remarks>
public class GameList : RSPacket
{
	protected ushort requestID;

	protected uint index;

	protected byte playerCount;

	protected GameFlags flags;

	protected string name;

	protected string description = null;

	public GameFlags Flags => flags;

	public byte PlayerCount => playerCount;

	public string Name => name;

	public string Description => description;

	public ushort RequestID => requestID;

	/// <summary>
	/// The game's index on the server.
	/// </summary>
	public uint Index => index;

	public GameList(byte[] data)
		: base(data)
	{
		requestID = BitConverter.ToUInt16(data, 3);
		index = BitConverter.ToUInt32(data, 5);
		playerCount = data[9];
		flags = (GameFlags)BitConverter.ToUInt32(data, 10);
		if ((flags & GameFlags.Valid) == GameFlags.Valid)
		{
			name = ByteConverter.GetNullString(data, 14);
			if (data.Length > 16 + name.Length)
			{
				description = ByteConverter.GetNullString(data, 15 + name.Length);
			}
		}
	}
}
