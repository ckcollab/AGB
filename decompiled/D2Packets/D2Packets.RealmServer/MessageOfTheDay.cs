using ETUtils;

namespace D2Packets.RealmServer;

/// <summary>
/// Realm Server Packet 0x12 - Message of the Day (more like message of the year ^^)
/// <para>Notifies client of the message to display when in lobby.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.RealmClient.MessageOfTheDayRequest" />
/// </remarks>
public class MessageOfTheDay : RSPacket
{
	protected string message;

	public string Message => message;

	public MessageOfTheDay(byte[] data)
		: base(data)
	{
		int offset = 3;
		while (data[offset++] != 0)
		{
		}
		message = ByteConverter.GetNullString(data, offset);
	}
}
