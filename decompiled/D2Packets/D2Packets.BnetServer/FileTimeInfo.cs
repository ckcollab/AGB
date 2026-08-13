using System;
using ETUtils;

namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x33 - File Time Info
/// <para>Contains the last modified timestamp of a file.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.BnetClient.FileTimeRequest" />
/// </remarks>
public class FileTimeInfo : BSPacket
{
	protected uint requestID;

	protected uint unknown;

	protected DateTime filetime;

	protected string filename;

	public uint RequestID => requestID;

	public DateTime FileTime => filetime;

	public string Filename => filename;

	public uint Unknown => unknown;

	public FileTimeInfo(byte[] data)
		: base(data)
	{
		requestID = BitConverter.ToUInt32(data, 4);
		unknown = BitConverter.ToUInt32(data, 8);
		filetime = DateTime.FromFileTimeUtc(BitConverter.ToInt64(data, 12));
		filename = ByteConverter.GetNullString(data, 20);
	}
}
