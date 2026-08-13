namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x40 - Request Quest Log
/// <para>Request the character's quest log for the current act.</para>
/// </summary>
public class RequestQuestLog : GCPacket
{
	public RequestQuestLog(byte[] data)
		: base(data)
	{
	}

	public RequestQuestLog()
		: base(Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1] { 64 };
	}
}
