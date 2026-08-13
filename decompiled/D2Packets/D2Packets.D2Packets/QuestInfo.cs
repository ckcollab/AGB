using D2Data;

namespace D2Packets.D2Packets;

/// <summary>
/// Structure containing information about a single quest.
/// <para>Used by <see cref="T:D2Packets.GameServer.UpdateQuestInfo" />.</para>
/// </summary>
public class QuestInfo
{
	public QuestType Type;

	public QuestState State;

	public QuestStanding Standing;

	public QuestInfo(QuestType type, QuestState state, QuestStanding standing)
	{
		Type = type;
		State = state;
		Standing = standing;
	}

	public override string ToString()
	{
		return $"{Type}: {State}. {Standing}";
	}
}
