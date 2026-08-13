namespace D2Packets.RealmClient;

/// <summary>
/// Realm Client Packet 0x12 - Message Of The Day Request
/// <para>Sent after logon to request the message to display in lobby.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.RealmServer.MessageOfTheDay" />
/// </remarks>
public class MessageOfTheDayRequest : RCPacket
{
	public MessageOfTheDayRequest(byte[] data)
		: base(data)
	{
	}
}
