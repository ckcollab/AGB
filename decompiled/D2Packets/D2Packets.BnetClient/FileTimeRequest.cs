using System;
using ETUtils;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x33 - File Time Request
/// <para>Request the timestamp of a file to determine if was modified.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.BnetServer.FileTimeInfo" />
/// </remarks>
public class FileTimeRequest : BCPacket
{
	protected uint requestID;

	protected uint unknown;

	protected string filename;

	public uint RequestID => requestID;

	public string Filename => filename;

	public uint Unknown => unknown;

	public FileTimeRequest(byte[] data)
		: base(data)
	{
		requestID = BitConverter.ToUInt32(data, 4);
		unknown = BitConverter.ToUInt32(data, 8);
		filename = ByteConverter.GetNullString(data, 12);
	}
}
