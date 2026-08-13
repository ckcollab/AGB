using ETUtils;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x0E - Chat Command
/// <para>Sends a chat message or command to Battle.net.</para>
/// </summary>
public class ChatCommand : BCPacket
{
	protected string message;

	public string Message => message;

	public ChatCommand(byte[] data)
		: base(data)
	{
		message = ByteConverter.GetNullString(data, 4);
	}
}
