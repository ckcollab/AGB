using System;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x2A - Transaction Complete
/// <para>Notifies that buying / selling an item or hiring a mercenary succeeded and assigns it a new UID.</para>
/// </summary>
public class TransactionComplete : GSPacket
{
	protected TransactionType type;

	protected uint uid;

	protected uint goldLeft;

	public TransactionType Type => type;

	public uint UID => uid;

	public uint GoldLeft => goldLeft;

	public string Unknown2 => ByteConverter.ToHexString(data, 2, 5);

	public TransactionComplete(byte[] data)
		: base(data)
	{
		type = (TransactionType)data[1];
		uid = BitConverter.ToUInt32(data, 7);
		goldLeft = BitConverter.ToUInt32(data, 11);
	}
}
