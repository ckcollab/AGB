namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x69 - Exit Game
/// <para>Notify D2GS that you are leaving the game.</para>
/// <para>NOTE: Upon receiving this packet, D2 will send a WM_CLOSE message to it's main window.
/// If the "ExitGame" flag is not set first it will not just the leave the game, but close Diablo II !</para>
/// </summary>
public class ExitGame : GCPacket
{
	public ExitGame(byte[] data)
		: base(data)
	{
	}

	public ExitGame()
		: base(EnterGame.Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1] { 105 };
	}
}
