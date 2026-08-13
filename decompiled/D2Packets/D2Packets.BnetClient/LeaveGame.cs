namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x1F - Leave Game
/// <para>Notifies Battle.net you have left a game.</para>
/// </summary>
public class LeaveGame : BCPacket
{
	public LeaveGame(byte[] data)
		: base(data)
	{
	}
}
