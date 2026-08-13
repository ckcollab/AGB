namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x10 - Leave Chat
/// <para>Sent before joining a game or exiting to character selection screen.</para>
/// </summary>
public class LeaveChat : BCPacket
{
	public LeaveChat(byte[] data)
		: base(data)
	{
	}
}
