using System;
using D2Data;
using D2Packets.D2Packets;
using ETUtils;

namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x0A - Enter Chat Response
/// <para>Returns a unique username and stat string in response to EnterChat.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.BnetClient.EnterChatRequest" />
/// </remarks>
public class EnterChatResponse : BSPacket
{
	protected string username;

	protected BattleNetClient client;

	protected string realm;

	protected string account;

	protected string name;

	protected int clientVersion = -1;

	protected BattleNetCharacter characterType = BattleNetCharacter.Unknown;

	protected int characterLevel = -1;

	protected CharacterFlags characterFlags = CharacterFlags.None;

	protected int characterAct = -1;

	protected CharacterTitle characterTitle = CharacterTitle.None;

	public string Username => username;

	public BattleNetClient Client => client;

	public string Realm => realm;

	public string Account => account;

	public string Name => name;

	public int ClientVersion => clientVersion;

	public BattleNetCharacter CharacterType => characterType;

	public int CharacterLevel => characterLevel;

	public CharacterFlags CharacterFlags => characterFlags;

	public int CharacterAct => characterAct;

	public CharacterTitle CharacterTitle => characterTitle;

	public EnterChatResponse(byte[] data)
		: base(data)
	{
		username = ByteConverter.GetNullString(data, 4);
		int index = 5 + username.Length;
		client = (BattleNetClient)BitConverter.ToUInt32(data, index);
		if (data[index += 4] == 0)
		{
			account = ByteConverter.GetNullString(data, index + 1);
			return;
		}
		realm = ByteConverter.GetString(data, index, -1, 44);
		index += 1 + realm.Length;
		name = ByteConverter.GetString(data, index, -1, 44);
		index += 1 + name.Length;
		int length = ByteConverter.GetByteOffset(data, 0, index);
		account = ByteConverter.GetNullString(data, index + length + 1);
		if (client == BattleNetClient.Diablo2LoD)
		{
			characterFlags |= CharacterFlags.Expansion;
		}
		StatString.ParseD2StatString(data, index, ref clientVersion, ref characterType, ref characterLevel, ref characterFlags, ref characterAct, ref characterTitle);
	}
}
