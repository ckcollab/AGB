namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x03 - Run To Location
/// </summary>
public class RunToLocation : GoToLocation
{
	public static readonly bool WRAPPED = true;

	public RunToLocation(byte[] data)
		: base(data)
	{
	}

	public RunToLocation(int x, int y)
		: base(Build(x, y))
	{
	}

	public static byte[] Build(int x, int y)
	{
		return new byte[5]
		{
			3,
			(byte)x,
			(byte)(x >> 8),
			(byte)y,
			(byte)(y >> 8)
		};
	}
}
