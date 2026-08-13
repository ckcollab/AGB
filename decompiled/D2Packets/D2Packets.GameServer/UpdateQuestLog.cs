using D2Data;
using D2Packets.D2Packets;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x52 - Update Quest Log
/// <para>Provides quest states for the quest panel display.</para>
/// </summary>
public class UpdateQuestLog : GSPacket
{
	protected QuestLog[] quests;

	public QuestLog[] Quests => quests;

	public UpdateQuestLog(byte[] data)
		: base(data)
	{
		quests = new QuestLog[41];
		for (int i = 0; i < 41; i++)
		{
			quests[i] = new QuestLog((QuestType)i, data[i + 1]);
		}
	}
}
