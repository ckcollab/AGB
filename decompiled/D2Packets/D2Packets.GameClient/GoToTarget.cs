using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet Wrapper - Go To Target
/// <para>Wrapper for <see cref="T:D2Packets.GameClient.WalkToTarget" /> and <see cref="T:D2Packets.GameClient.RunToTarget" />.</para>
/// </summary>
public class GoToTarget : GCPacket
{
	protected UnitType unitType;

	protected uint uid;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public GoToTarget(byte[] data)
		: base(data)
	{
		unitType = (UnitType)BitConverter.ToUInt32(data, 1);
		uid = BitConverter.ToUInt32(data, 5);
	}
}
