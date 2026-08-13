using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x31 - Display Quest Message
/// <para>Notifies D2GS that a quest message was displayed so it can update the quest log.</para>
/// </summary>
public class DisplayQuestMessage : GCPacket
{
	protected uint uid;

	protected uint message;

	public uint UID => uid;

	public uint Message => message;

	public DisplayQuestMessage(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		message = BitConverter.ToUInt32(data, 5);
	}

	public DisplayQuestMessage(uint uid, uint message)
		: base(Build(uid, message))
	{
		this.uid = uid;
		this.message = message;
	}

	public static byte[] Build(uint uid, uint message)
	{
		return new byte[9]
		{
			49,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)message,
			(byte)(message >> 8),
			(byte)(message >> 16),
			(byte)(message >> 24)
		};
	}
}
