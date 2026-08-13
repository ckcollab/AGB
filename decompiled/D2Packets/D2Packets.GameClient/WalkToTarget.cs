using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x02 - Walk To Target
/// </summary>
public class WalkToTarget : GoToTarget
{
	public static readonly bool WRAPPED = true;

	public WalkToTarget(byte[] data)
		: base(data)
	{
	}

	public WalkToTarget(UnitType target, uint uid)
		: base(Build(target, uid))
	{
	}

	public static byte[] Build(UnitType target, uint uid)
	{
		return new byte[9]
		{
			2,
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
