using System;
using D2Data;
using D2Packets.D2Packets;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x28 - Update Quest Info
/// <para>Provides info about current quest states.</para>
/// <para>Sent when entering game, opening the quest panel and when interacting with a town folk.</para>
/// </summary>
public class UpdateQuestInfo : GSPacket
{
	public static readonly uint NULL_UInt32 = 0u;

	protected QuestInfo[] quests;

	protected QuestInfoUpdateType type;

	protected uint uid;

	public QuestInfo[] Quests => quests;

	public QuestInfoUpdateType Type => type;

	public uint UID => uid;

	public string Unknown88 => ByteConverter.ToHexString(data, 88);

	public UpdateQuestInfo(byte[] data)
		: base(data)
	{
		type = (QuestInfoUpdateType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		quests = new QuestInfo[41];
		for (int i = 0; i < 41; i++)
		{
			quests[i] = new QuestInfo((QuestType)i, (QuestState)data[6 + i * 2], (QuestStanding)data[7 + i * 2]);
		}
	}
}
