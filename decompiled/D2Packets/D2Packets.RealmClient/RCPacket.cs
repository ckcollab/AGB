using System;
using D2Packets.D2Packets;

namespace D2Packets.RealmClient;

/// <summary>
/// Base class for Realm Client -&gt; Server Packets
/// </summary>
public class RCPacket : D2Packet
{
	public readonly RealmClientPacket PacketType;

	public RCPacket(byte[] data)
		: base(data, PacketOrigin.RealmClient)
	{
		packetID = data[2];
		PacketType = (RealmClientPacket)packetID;
	}

	public static RCPacket Build(byte[] data)
	{
		if ((object)D2PacketsInfo.RCPacketTypes[data[2]] == null)
		{
			return new RCPacket(data);
		}
		return Activator.CreateInstance(D2PacketsInfo.RCPacketTypes[data[2]], data) as RCPacket;
	}
}
