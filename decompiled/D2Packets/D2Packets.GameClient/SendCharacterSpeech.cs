using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x3F - Send Character Speech
/// </summary>
public class SendCharacterSpeech : GCPacket
{
	protected GameSound speech;

	public GameSound Speech => speech;

	public SendCharacterSpeech(byte[] data)
		: base(data)
	{
		speech = (GameSound)BitConverter.ToUInt16(data, 1);
	}

	public SendCharacterSpeech(GameSound speech)
		: base(Build(speech))
	{
		this.speech = speech;
	}

	public static byte[] Build(GameSound speech)
	{
		return new byte[3]
		{
			63,
			(byte)speech,
			(byte)((ushort)speech >> 8)
		};
	}
}
