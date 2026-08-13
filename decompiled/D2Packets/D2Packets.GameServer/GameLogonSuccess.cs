namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x02 - Game Logon Success
/// <para>Last packet of the logon sequence sent only if the game logon is successful.</para>
/// <para>If this packet is not received the connexion will be dropped.</para>
/// </summary>
/// <remarks>
/// Part of logon sequence
/// <para>Previous packet: <see cref="T:D2Packets.GameServer.GameLoading" /></para>
/// <para>Response: <see cref="T:D2Packets.GameClient.EnterGame" /></para>
/// </remarks>
public class GameLogonSuccess : GSPacket
{
	public GameLogonSuccess(byte[] data)
		: base(data)
	{
	}

	public GameLogonSuccess()
		: base(Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1] { 2 };
	}
}
