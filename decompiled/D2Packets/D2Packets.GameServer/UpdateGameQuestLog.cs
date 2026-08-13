using System;
using D2Data;
using D2Packets.D2Packets;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x29 - Update Game Quest Log
/// <para>Provides info about current quest states for the current game.</para>
/// <para>Sent when entering game and interacting with a town folk.</para>
/// </summary>
public class UpdateGameQuestLog : GSPacket
{
	protected GameQuestInfo[] quests;

	public GameQuestInfo[] Quests => quests;

	public string Unknown82 => ByteConverter.ToHexString(data, 83);

	public UpdateGameQuestLog(byte[] data)
		: base(data)
	{
		quests = new GameQuestInfo[41];
		for (int i = 0; i < 41; i++)
		{
			quests[i] = new GameQuestInfo((QuestType)i, (GameQuestState)BitConverter.ToUInt16(data, 1 + i * 2));
		}
	}
}
