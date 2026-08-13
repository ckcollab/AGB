namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x04 - Act Loaded
/// <para>Loading screen lights up...</para>
/// </summary>
public class LoadDone : GSPacket
{
	public LoadDone(byte[] data)
		: base(data)
	{
	}

	public LoadDone()
		: base(Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1] { 4 };
	}
}
