using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x68 - Game Logon Request
/// <para>This is the first packet sent to D2GS, using info from RS (0x04 - JoinGame)</para>
/// <para>This is the main packet for the D2GS logon where you need to send your char Information to the game server.
/// This packet must be 37 bytes long, so if your char name is not 15 letters in lengh, you will need to add the correct number of bytes after your Char name.
/// Your char name should be inbedded to the start of the following hex strand:
/// Code: 00000000B5D6779A81B36F4B00000000
/// The strand must be 16 bytes long and contain your character name with a null terminator.
/// The server will not answer this packet if it is not 37 bytes long, doesn't contain the D2GS hash or D2GS token or doesn't contain your char name.</para>
/// </summary>
public class GameLogonRequest : GCPacket
{
	protected uint d2GShash;

	protected ushort d2GSToken;

	protected CharacterClass charClass;

	protected uint version;

	protected string name;

	public uint Version => version;

	public CharacterClass Class => charClass;

	public string Name => name;

	public uint D2GShash => d2GShash;

	public ushort D2GSToken => d2GSToken;

	public string Unknown12 => ByteConverter.ToHexString(data, 12, 9);

	public GameLogonRequest(byte[] data)
		: base(data)
	{
		d2GShash = BitConverter.ToUInt32(data, 1);
		d2GSToken = BitConverter.ToUInt16(data, 5);
		charClass = (CharacterClass)data[7];
		version = BitConverter.ToUInt32(data, 8);
		name = ByteConverter.GetNullString(data, 21);
	}
}
