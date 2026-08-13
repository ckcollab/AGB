using System;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x4B - Extra Work Response
/// <para>Contains the result of the extra work performed as requested by 
/// <see cref="T:D2Packets.BnetServer.ExtraWorkInfo" /> or <see cref="T:D2Packets.BnetServer.RequiredExtraWorkInfo" />.</para>
/// </summary>
public class ExtraWorkResponse : BCPacket
{
	protected int resultLength;

	protected int client;

	public int Client => client;

	public int ResultLength => resultLength;

	public byte[] ResultData
	{
		get
		{
			byte[] bytes = new byte[resultLength];
			Array.Copy(data, 7, bytes, 0, resultLength);
			return bytes;
		}
	}

	public ExtraWorkResponse(byte[] data)
		: base(data)
	{
		client = BitConverter.ToUInt16(data, 4);
		resultLength = BitConverter.ToUInt16(data, 6);
	}
}
