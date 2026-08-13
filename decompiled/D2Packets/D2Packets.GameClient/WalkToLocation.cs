namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x01 - Walk To Location
/// </summary>
public class WalkToLocation : GoToLocation
{
	public static readonly bool WRAPPED = true;

	public WalkToLocation(byte[] data)
		: base(data)
	{
	}

	public WalkToLocation(int x, int y)
		: base(Build(x, y))
	{
	}

	public static byte[] Build(int x, int y)
	{
		return new byte[5]
		{
			1,
			(byte)x,
			(byte)(x >> 8),
			(byte)y,
			(byte)(y >> 8)
		};
	}
}
