namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x06 - Game Logout Success
/// <para>Sent on game quit if the connection with Battle.net is not dropped.</para>
/// </summary>
public class GameLogoutSuccess : GSPacket
{
	public GameLogoutSuccess(byte[] data)
		: base(data)
	{
	}

	public GameLogoutSuccess()
		: base(Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1] { 6 };
	}
}
