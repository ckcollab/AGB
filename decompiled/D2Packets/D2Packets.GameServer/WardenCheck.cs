using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0xAE - Warden Check
/// <para>Teh Unholy Guardian of Secrets...</para>
/// </summary>
public class WardenCheck : GSPacket
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

	public WardenCheck(byte[] data)
		: base(data)
	{
		dataLength = BitConverter.ToUInt16(data, 1);
	}
}
