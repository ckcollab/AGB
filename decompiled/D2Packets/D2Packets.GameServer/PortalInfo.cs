using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x60 - Portal Info
/// <para>A portal comes into your range of view.</para>
/// <para>This packet should be linked to a GS packet 0x82 to assign an owner to the portal.</para>
/// </summary>
public class PortalInfo : GSPacket
{
	protected TownPortalState state;

	protected AreaLevel destination;

	protected uint uid;

	public AreaLevel Destination => destination;

	public TownPortalState State => state;

	public uint UID => uid;

	public PortalInfo(byte[] data)
		: base(data)
	{
		state = (TownPortalState)data[1];
		destination = (AreaLevel)data[2];
		uid = BitConverter.ToUInt32(data, 3);
	}
}
