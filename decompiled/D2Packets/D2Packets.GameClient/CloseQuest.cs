using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x58 - Close Quest
/// <para>Notifies D2GS that a quest's completion animation in pannel was viewed.</para>
/// <para>Sent when the quest pannel is opened.</para>
/// </summary>
/// <remarks>
/// Length: 3
/// </remarks>
public class CloseQuest : GCPacket
{
	protected QuestType quest;

	/// <summary>
	/// The id of the quest.
	/// <list type="bullet">
	/// <item>Offset: 1</item>
	/// <item>Length: WORD</item>
	/// </list>
	/// </summary>
	public QuestType Quest => quest;

	public CloseQuest(byte[] data)
		: base(data)
	{
		quest = (QuestType)BitConverter.ToUInt16(data, 1);
	}

	public CloseQuest(QuestType quest)
		: base(Build(quest))
	{
		this.quest = quest;
	}

	public static byte[] Build(QuestType quest)
	{
		return new byte[3]
		{
			88,
			(byte)quest,
			(byte)((ushort)quest >> 8)
		};
	}
}
