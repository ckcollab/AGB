namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x00 - Game Loading
/// <para>Sent during game logon sequence to let client know the server is processing the request.</para>
/// <para>The join can still fail after this...</para>
/// </summary>
/// <remarks>
/// Part of logon sequence
/// <para>Previous packet: <see cref="T:D2Packets.GameServer.GameLogonReceipt" /></para>
/// <para>Next packet: <see cref="T:D2Packets.GameServer.GameLogonSuccess" /></para>
/// </remarks>
public class GameLoading : GSPacket
{
	public GameLoading(byte[] data)
		: base(data)
	{
	}

	public GameLoading()
		: base(Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1];
	}
}
