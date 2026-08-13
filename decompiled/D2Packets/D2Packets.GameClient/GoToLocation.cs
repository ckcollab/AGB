using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet Wrapper - Go To Location
/// <para>Wrapper for <see cref="T:D2Packets.GameClient.WalkToLocation" /> and <see cref="T:D2Packets.GameClient.RunToLocation" />.</para>
/// </summary>
public class GoToLocation : GCPacket
{
	protected int x;

	protected int y;

	public int X => x;

	public int Y => y;

	public GoToLocation(byte[] data)
		: base(data)
	{
		x = BitConverter.ToUInt16(data, 1);
		y = BitConverter.ToUInt16(data, 3);
	}
}
