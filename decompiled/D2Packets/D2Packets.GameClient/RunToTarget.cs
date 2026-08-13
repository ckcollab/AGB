using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x04 - Run To Target
/// </summary>
public class RunToTarget : GoToTarget
{
	public static readonly bool WRAPPED = true;

	public RunToTarget(byte[] data)
		: base(data)
	{
	}

	public RunToTarget(UnitType target, uint uid)
		: base(Build(target, uid))
	{
	}

	public static byte[] Build(UnitType target, uint uid)
	{
		return new byte[9]
		{
			4,
			(byte)target,
			0,
			0,
			0,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
