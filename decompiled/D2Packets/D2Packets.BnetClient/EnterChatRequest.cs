using ETUtils;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x0A - Enter Chat Request
/// <para>Sent when entering lobby.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.BnetServer.EnterChatResponse" />
/// </remarks>
public class EnterChatRequest : BCPacket
{
	protected string name;

	protected string realm;

	public string Name => name;

	public string Realm => realm;

	public EnterChatRequest(byte[] data)
		: base(data)
	{
		name = ByteConverter.GetNullString(data, 4);
		realm = ByteConverter.GetString(data, 5 + name.Length, -1, 44);
	}
}
