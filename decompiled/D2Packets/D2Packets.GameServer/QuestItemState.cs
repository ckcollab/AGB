using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x5D - Quest Item State
/// <para>Sent when for quest related actions / mode changes.</para>
/// </summary>
public class QuestItemState : GSPacket
{
	public string Unknown1 => ByteConverter.ToHexString(data, 1);

	public QuestItemState(byte[] data)
		: base(data)
	{
	}
}
