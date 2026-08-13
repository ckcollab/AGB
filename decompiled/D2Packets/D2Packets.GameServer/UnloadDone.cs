namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x05 - Unload Done
/// <para>Sent before 0x03 on act change or after 0xB0 on game quit (but not if GS connection is interrupted.)</para>
/// </summary>
public class UnloadDone : GSPacket
{
	public UnloadDone(byte[] data)
		: base(data)
	{
	}

	public UnloadDone()
		: base(Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1] { 5 };
	}
}
