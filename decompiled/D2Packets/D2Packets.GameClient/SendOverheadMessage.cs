using System;
using ETUtils;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x14 - Send Overhead Message
/// <para>Sends a message to be displayed over the character.</para>
/// </summary>
public class SendOverheadMessage : GCPacket
{
	protected string message;

	public string Message => message;

	/// <summary>
	/// Probably the MessageType to match other message packets... 
	/// unused since not needed for single message type packet.
	/// </summary>
	public string Unknown1 => ByteConverter.ToHexString(data, 1, 2);

	/// <summary>
	/// Probably just the same unused reserved nullstrings as below...
	/// </summary>
	public string UnknownEnd => ByteConverter.ToHexString(data, 4 + Message.Length, 2);

	public SendOverheadMessage(byte[] data)
		: base(data)
	{
		message = ByteConverter.GetNullString(data, 3);
	}

	public SendOverheadMessage(string message)
		: base(Build(message))
	{
		this.message = message;
	}

	public static byte[] Build(string message)
	{
		if (message == null || message.Length == 0)
		{
			throw new ArgumentException("message");
		}
		byte[] bytes = new byte[6 + message.Length];
		bytes[0] = 20;
		for (int i = 0; i < message.Length; i++)
		{
			bytes[3 + i] = (byte)message[i];
		}
		return bytes;
	}
}
