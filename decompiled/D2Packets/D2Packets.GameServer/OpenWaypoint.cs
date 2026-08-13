using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x63 - Open Waypoint
/// <para>Notifies to open a waypoint's menu and which destinations are available.</para>
/// </summary>
public class OpenWaypoint : GSPacket
{
	protected uint uid;

	protected WaypointsAvailiable waypoints;

	public uint UID => uid;

	public WaypointsAvailiable Waypoints => waypoints;

	public OpenWaypoint(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		waypoints = (WaypointsAvailiable)BitConverter.ToUInt64(data, 7);
	}
}
