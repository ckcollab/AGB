namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0xB0 - Game Over
/// <para>Notifies client of server's intention to drop the connection.</para>
/// <para>Sent in reply to GameClient.ExitGame, normally followed by UnloadDone and GameLogoutSuccess.</para>
/// </summary>
public class GameOver : GSPacket
{
	public GameOver(byte[] data)
		: base(data)
	{
	}

	public GameOver()
		: base(Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1] { 176 };
	}
}
