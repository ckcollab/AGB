using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x15 - Send Message
/// <para>Send a public game message or whisper to a player.</para>
/// </summary>
public class SendMessage : GCPacket
{
	protected GameMessageType type;

	protected string message;

	protected string recipient = null;

	public GameMessageType Type => type;

	public string Message => message;

	public string Recipient => recipient;

	public SendMessage(byte[] data)
		: base(data)
	{
		type = (GameMessageType)BitConverter.ToUInt16(data, 1);
		message = ByteConverter.GetNullString(data, 3);
		if (type == GameMessageType.GameWhisper)
		{
			recipient = ByteConverter.GetNullString(data, 4 + message.Length);
		}
	}

	public SendMessage(GameMessageType type, string message)
		: base(Build(type, message))
	{
		this.type = type;
		this.message = message;
	}

	public SendMessage(GameMessageType type, string message, string recipient)
		: base(Build(type, message, recipient))
	{
		this.type = type;
		this.message = message;
		this.recipient = recipient;
	}

	public static byte[] Build(GameMessageType type, string message)
	{
		return Build(type, message, null);
	}

	public static byte[] Build(GameMessageType type, string message, string recipient)
	{
		if (message == null || message.Length == 0)
		{
			throw new ArgumentException();
		}
		int r = recipient?.Length ?? 0;
		byte[] bytes = new byte[6 + message.Length + r];
		bytes[0] = 21;
		bytes[1] = (byte)type;
		bytes[2] = (byte)((byte)type >> 8);
		for (int i = 0; i < message.Length; i++)
		{
			bytes[3 + i] = (byte)message[i];
		}
		if (r > 0)
		{
			r = 4 + message.Length;
			for (int i = 0; i < recipient.Length; i++)
			{
				bytes[r + i] = (byte)recipient[i];
			}
		}
		return bytes;
	}
}
