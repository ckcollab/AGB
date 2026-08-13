using System;
using ETUtils;

namespace D2Packets.D2Packets;

/// <summary>
/// Structure containing information about a single Realm (e.g. USEast).
/// Used by <see cref="T:D2Packets.BnetServer.QueryRealmsResponse" />.
/// </summary>
public struct RealmInfo
{
	public readonly uint Unknown;

	public readonly string Name;

	public readonly string Description;

	public RealmInfo(byte[] data, int offset)
	{
		Unknown = BitConverter.ToUInt32(data, offset);
		Name = ByteConverter.GetNullString(data, offset + 4);
		Description = ByteConverter.GetNullString(data, offset + 5 + Name.Length);
	}

	public override string ToString()
	{
		return Name;
	}
}
