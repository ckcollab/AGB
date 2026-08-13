using D2Data;

namespace D2Packets.D2Packets;

/// <summary>
/// Structure containing information about a single quest.
/// <para>Used by <see cref="T:D2Packets.GameServer.UpdateGameQuestLog" />.</para>
/// </summary>
public class GameQuestInfo
{
	public QuestType Type;

	public GameQuestState State;

	public GameQuestInfo(QuestType type, GameQuestState state)
	{
		Type = type;
		State = state;
	}

	public override string ToString()
	{
		return $"{Type}: {State}";
	}
}
