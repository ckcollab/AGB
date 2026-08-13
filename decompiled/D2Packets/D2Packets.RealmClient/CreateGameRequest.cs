using System;
using D2Data;
using ETUtils;

namespace D2Packets.RealmClient;

/// <summary>
/// Realm Client Packet 0x03 - Create Game Request
/// <para>Request creating a new game to the server. This will not automatically join the game.</para>
/// <para>The game name and password are capitalized before being sent (e.g. "aBc DeF" becomes "Abc Def".)</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.RealmServer.CreateGameResponse" />
/// </remarks>
public class CreateGameRequest : RCPacket
{
	public static readonly sbyte NULL_SByte = -1;

	protected ushort requestID;

	protected byte unknown1;

	protected GameDifficulty difficulty;

	protected ushort unknown2;

	protected byte unknown3;

	protected sbyte levelRestriction;

	protected byte maxPlayers;

	protected string name;

	protected string password = null;

	protected string description = null;

	/// <summary>
	/// Starts at 2 at first game and increments by 2 for each consecutive game creation.
	/// </summary>
	public ushort RequestID => requestID;

	public GameDifficulty Difficulty => difficulty;

	public sbyte LevelRestriction => levelRestriction;

	public byte MaxPlayers => maxPlayers;

	public string Name => name;

	public string Password => password;

	public string Description => description;

	/// <summary>
	/// Possibly unused (0)
	/// </summary>
	public byte Unknown1 => unknown1;

	/// <summary>
	/// Possibly unused (0)
	/// </summary>
	public ushort Unknown2 => unknown2;

	/// <summary>
	/// Always 1 ?
	/// </summary>
	public byte Unknown3 => unknown3;

	public CreateGameRequest(byte[] data)
		: base(data)
	{
		requestID = BitConverter.ToUInt16(data, 3);
		unknown1 = data[5];
		difficulty = (GameDifficulty)(data[6] >> 4);
		unknown2 = BitConverter.ToUInt16(data, 7);
		unknown3 = data[9];
		levelRestriction = (sbyte)data[10];
		maxPlayers = data[11];
		name = ByteConverter.GetNullString(data, 12);
		if (data.Length > 15 + name.Length)
		{
			password = ByteConverter.GetNullString(data, 13 + name.Length);
		}
		if (data.Length > 15 + name.Length + ((password != null) ? password.Length : 0))
		{
			description = ByteConverter.GetNullString(data, 14 + name.Length + ((password != null) ? Password.Length : 0));
		}
	}
}
