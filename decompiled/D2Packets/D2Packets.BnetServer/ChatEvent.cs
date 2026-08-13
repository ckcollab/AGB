using System;
using D2Data;
using D2Packets.D2Packets;
using ETUtils;

namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x0F - Chat Event
/// <para>Various chat related events (channel join, leave, message, whisper, etc.)</para>
/// </summary>
public class ChatEvent : BSPacket
{
	public static readonly int NULL_Int32 = -1;

	public static readonly int NULL_UInt32 = 0;

	protected ChatEventType eventType;

	protected uint flags;

	protected uint ping;

	protected string account;

	protected string name = null;

	protected string message = null;

	protected string realm = null;

	protected BattleNetClient client = BattleNetClient.Unknown;

	protected int clientVersion = -1;

	protected BattleNetCharacter characterType = BattleNetCharacter.Unknown;

	protected int characterLevel = -1;

	protected CharacterFlags characterFlags = CharacterFlags.None;

	protected int characterAct = -1;

	protected CharacterTitle characterTitle = CharacterTitle.None;

	public ChatEventType Event => eventType;

	public uint Flags => flags;

	public uint Ping => ping;

	public string Account => account;

	public string Name => name;

	public string Message => message;

	public string Realm => realm;

	public BattleNetClient Client => client;

	public int ClientVersion => clientVersion;

	public BattleNetCharacter CharacterType => characterType;

	public int CharacterLevel => characterLevel;

	public CharacterFlags CharacterFlags => characterFlags;

	public int CharacterAct => characterAct;

	public CharacterTitle CharacterTitle => characterTitle;

	public ChatEvent(byte[] data)
		: base(data)
	{
		eventType = (ChatEventType)BitConverter.ToUInt32(data, 4);
		flags = BitConverter.ToUInt32(data, 8);
		ping = BitConverter.ToUInt32(data, 12);
		int index = ByteConverter.GetByteOffset(data, 0, 28);
		int pos = ByteConverter.GetByteOffset(data, 42, 28, index);
		if (pos > 0)
		{
			name = ByteConverter.GetString(data, 28, pos);
			index -= pos + 1;
			pos += 29;
		}
		else if (pos == 0)
		{
			pos = 29;
			index--;
			characterType = BattleNetCharacter.OpenCharacter;
		}
		else
		{
			pos = 28;
		}
		account = ByteConverter.GetString(data, pos, index);
		index += pos + 1;
		if (eventType == ChatEventType.ChannelLeave)
		{
			return;
		}
		if (eventType == ChatEventType.ChannelJoin || eventType == ChatEventType.ChannelUser)
		{
			if (data.Length - index > 3)
			{
				client = (BattleNetClient)BitConverter.ToUInt32(data, index);
				index += 4;
			}
			if (client == BattleNetClient.StarcraftShareware || client == BattleNetClient.Starcraft || client == BattleNetClient.StarcraftBroodWar || (client != BattleNetClient.Diablo2 && client != BattleNetClient.Diablo2LoD))
			{
				return;
			}
			if (client == BattleNetClient.Diablo2LoD)
			{
				characterFlags |= CharacterFlags.Expansion;
			}
			if (data.Length - index < 4)
			{
				return;
			}
			realm = ByteConverter.GetString(data, index, -1, 44);
			index += realm.Length + 1;
			if (data.Length >= index)
			{
				index += ByteConverter.GetByteOffset(data, 44, index) + 1;
				if (index != -1 && data.Length > index && data.Length - index >= 33)
				{
					StatString.ParseD2StatString(data, index, ref clientVersion, ref characterType, ref characterLevel, ref characterFlags, ref characterAct, ref characterTitle);
				}
			}
		}
		else
		{
			message = ByteConverter.GetNullString(data, index);
		}
	}
}
