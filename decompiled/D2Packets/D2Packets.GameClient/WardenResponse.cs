using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x66 - Warden Response
/// <para>Sent in reply to GS 0xAE; Warden check's response.</para>
/// </summary>
public class WardenResponse : GCPacket
{
	protected int dataLength;

	public int DataLength => dataLength;

	public byte[] WardenData
	{
		get
		{
			byte[] bytes = new byte[dataLength];
			Array.Copy(data, 3, bytes, 0, dataLength);
			return bytes;
		}
	}

	public WardenResponse(byte[] data)
		: base(data)
	{
		dataLength = BitConverter.ToUInt16(data, 1);
	}
}
