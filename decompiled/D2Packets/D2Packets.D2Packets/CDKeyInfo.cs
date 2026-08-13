using System;
using ETUtils;

namespace D2Packets.D2Packets;

/// <summary>
/// Structure containing information about a single CD Key.
/// Used by <see cref="T:D2Packets.BnetClient.BnetAuthRequest" />.
/// </summary>
public class CDKeyInfo
{
	/// <summary>
	/// Always 16 for Diablo II...
	/// </summary>
	public readonly int Length;

	public readonly int ProductValue;

	public readonly int PublicValue;

	/// <summary>
	/// Always 0...
	/// </summary>
	public readonly int Unknown;

	public readonly byte[] Hash;

	public CDKeyInfo(int productValue, int publicValue, byte[] hash)
	{
		Length = 16;
		ProductValue = productValue;
		PublicValue = publicValue;
		Unknown = 0;
		Hash = hash;
	}

	public CDKeyInfo(byte[] data, int offset)
	{
		Length = BitConverter.ToInt32(data, offset);
		ProductValue = BitConverter.ToInt32(data, offset + 4);
		PublicValue = BitConverter.ToInt32(data, offset + 8);
		Unknown = BitConverter.ToInt32(data, offset + 12);
		Hash = new byte[20];
		Array.Copy(data, offset + 16, Hash, 0, 20);
	}

	public override string ToString()
	{
		return StringUtils.ToInfoString((object)this);
	}
}
