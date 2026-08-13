using System;
using D2Packets.D2Packets;

namespace D2Packets.RealmServer;

/// <summary>
/// Base class for Realm Server -&gt; Client Packets
/// </summary>
public class RSPacket : D2Packet
{
	public readonly RealmServerPacket PacketType;

	public RSPacket(byte[] data)
		: base(data, PacketOrigin.RealmServer)
	{
		packetID = data[2];
		PacketType = (RealmServerPacket)packetID;
	}

	public static RSPacket Build(byte[] data)
	{
		if ((object)D2PacketsInfo.RSPacketTypes[data[2]] == null)
		{
			return new RSPacket(data);
		}
		return Activator.CreateInstance(D2PacketsInfo.RSPacketTypes[data[2]], data) as RSPacket;
	}
}
