using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x5B - Player In Game
/// <para>Sent for each player in game (including you) on join.</para>
/// <para>It is also sent for every player that joins afterwards, followed by a 0x5A packet of type JoinedGame.</para>
/// </summary>
public class PlayerInGame : GSPacket
{
	protected string name;

	protected CharacterClass charClass;

	protected short level;

	protected short partyID;

	protected uint uid;

	public string Name => name;

	public CharacterClass Class => charClass;

	public short Level => level;

	public short PartyID => partyID;

	public uint UID => uid;

	public string Unknown2 => ByteConverter.ToHexString(data, 28);

	public PlayerInGame(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 3);
		charClass = (CharacterClass)data[7];
		name = ByteConverter.GetNullString(data, 8);
		level = BitConverter.ToInt16(data, 24);
		partyID = BitConverter.ToInt16(data, 26);
	}

	public PlayerInGame(uint uid, CharacterClass charClass, string name, short level, short partyID)
		: base(Build(uid, charClass, name, level, partyID))
	{
		this.uid = uid;
		this.charClass = charClass;
		this.name = name;
		this.level = level;
		this.partyID = partyID;
	}

	public static byte[] Build(uint uid, CharacterClass charClass, string name, short level, short partyID)
	{
		if (name == null || name.Length == 0 || name.Length > 16)
		{
			throw new ArgumentException("name");
		}
		byte[] bytes = new byte[34]
		{
			91,
			34,
			0,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			(byte)level,
			(byte)(level >> 8),
			(byte)partyID,
			(byte)(partyID >> 8),
			0,
			0,
			0,
			0,
			0,
			0
		};
		for (int i = 0; i < name.Length; i++)
		{
			bytes[8 + i] = (byte)name[i];
		}
		return bytes;
	}
}
