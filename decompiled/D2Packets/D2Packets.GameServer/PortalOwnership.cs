using System;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x82 - Portal Ownership
/// <para>Notifies you of a portal's ownership information when it comes into your range of view.</para>
/// </summary>
public class PortalOwnership : GSPacket
{
	protected uint ownerUID;

	protected string ownerName;

	protected uint portalLocalUID;

	protected uint portalRemoteUID;

	public uint OwnerUID => ownerUID;

	public string OwnerName => ownerName;

	public uint PortalLocalUID => portalLocalUID;

	public uint PortalRemoteUID => portalRemoteUID;

	public PortalOwnership(byte[] data)
		: base(data)
	{
		ownerUID = BitConverter.ToUInt32(data, 1);
		ownerName = ByteConverter.GetNullString(data, 5, 16);
		portalLocalUID = BitConverter.ToUInt32(data, 21);
		portalRemoteUID = BitConverter.ToUInt32(data, 25);
	}
}
