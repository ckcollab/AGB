using D2Data;

namespace D2Packets.D2Packets;

/// <summary>
/// Structure containing information about a single quest.
/// <para>Used by <see cref="T:D2Packets.GameServer.UpdateQuestLog" />.</para>
/// </summary>
public class QuestLog
{
	public QuestType Type;

	public int State;

	public QuestLog(QuestType type, int state)
	{
		Type = type;
		State = state;
	}

	public override string ToString()
	{
		return string.Format("{0}: {1}", Type, State.ToString("x2"));
	}
}
