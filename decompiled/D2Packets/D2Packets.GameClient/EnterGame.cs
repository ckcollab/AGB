namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x6B - Enter Game
/// <para>Sent after successful logon. Server will then send all the game's information.</para>
/// </summary>
public class EnterGame : GCPacket
{
	public EnterGame(byte[] data)
		: base(data)
	{
	}

	public EnterGame()
		: base(Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1] { 107 };
	}
}
