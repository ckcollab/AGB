using System;
using ETUtils;

namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x15 - Ad Info
/// <para>Contains the information required to download and display an ad.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.BnetClient.AdInfoRequest" />
/// </remarks>
public class AdInfo : BSPacket
{
	protected uint id;

	protected string extension;

	protected DateTime timestamp;

	protected string filename;

	protected string url;

	public uint ID => id;

	public string Extension => extension;

	public DateTime Timestamp => timestamp;

	public string Filename => filename;

	public string URL => url;

	public AdInfo(byte[] data)
		: base(data)
	{
		id = BitConverter.ToUInt32(data, 4);
		extension = ByteConverter.GetString(data, 8, 4);
		timestamp = DateTime.FromFileTimeUtc(BitConverter.ToInt64(data, 12));
		filename = ByteConverter.GetNullString(data, 20);
		url = ByteConverter.GetNullString(data, 21 + filename.Length);
	}
}
