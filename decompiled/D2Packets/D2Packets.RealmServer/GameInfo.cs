using System;
using D2Packets.D2Packets;
using ETUtils;

namespace D2Packets.RealmServer;

/// <summary>
/// Realm Server Packet 0x06 - Game Info
/// <para>Provides information about a particular game.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.RealmClient.GameInfoRequest" />
/// </remarks>
public class GameInfo : RSPacket
{
	public static readonly int NULL_Int32 = -1;

	protected ushort requestID;

	protected GameFlags flags;

	protected TimeSpan uptime;

	protected int maxPlayers;

	protected int playerCount;

	protected int creatorLevel;

	protected int levelRestriction;

	protected int minLevel = -1;

	protected int maxLevel = -1;

	protected CharacterBaseInfo[] players;

	public ushort RequestID => requestID;

	public GameFlags Flags => flags;

	public TimeSpan Uptime => uptime;

	public int MaxPlayers => maxPlayers;

	public int PlayerCount => playerCount;

	public int CreatorLevel => creatorLevel;

	public int LevelRestriction => levelRestriction;

	public int MinLevel => minLevel;

	public int MaxLevel => maxLevel;

	public CharacterBaseInfo[] Players => players;

	public GameInfo(byte[] data)
		: base(data)
	{
		requestID = BitConverter.ToUInt16(data, 3);
		flags = (GameFlags)BitConverter.ToUInt32(data, 5);
		uptime = new TimeSpan((long)BitConverter.ToUInt32(data, 9) * 10000000L);
		creatorLevel = data[13];
		levelRestriction = (sbyte)data[14];
		if (data[14] != byte.MaxValue)
		{
			minLevel = Math.Max(1, data[13] - data[14]);
			maxLevel = Math.Min(99, data[13] + data[14]);
		}
		maxPlayers = data[15];
		playerCount = data[16];
		players = new CharacterBaseInfo[playerCount];
		int namePos = 50;
		for (int i = 0; i < playerCount; i++)
		{
			players[i] = new CharacterBaseInfo(ByteConverter.GetNullString(data, namePos), data[17 + i], data[33 + i]);
			namePos += players[i].Name.Length + 1;
		}
	}
}
