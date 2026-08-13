using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x49 - Waypoint Interact
/// </summary>
public class WaypointInteract : GCPacket
{
	protected uint uid;

	protected WaypointDestination destination;

	public uint UID => uid;

	public WaypointDestination Destination => destination;

	public WaypointInteract(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		destination = (WaypointDestination)data[5];
	}

	public WaypointInteract(uint uid, WaypointDestination destination)
		: base(Build(uid, destination))
	{
		this.uid = uid;
		this.destination = destination;
	}

	public static byte[] Build(uint uid, WaypointDestination destination)
	{
		return new byte[9]
		{
			73,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)destination,
			0,
			0,
			0
		};
	}
}
