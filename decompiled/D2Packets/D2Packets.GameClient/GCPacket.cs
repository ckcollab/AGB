using System;
using D2Packets.D2Packets;

namespace D2Packets.GameClient;

/// <summary>
/// Base class for D2GS Game Client -&gt; Server Packets
/// </summary>
public class GCPacket : D2Packet
{
	public readonly GameClientPacket PacketType;

	public GCPacket(byte[] data)
		: base(data, PacketOrigin.GameClient)
	{
		packetID = data[0];
		PacketType = (GameClientPacket)packetID;
	}

	public static GCPacket Build(byte[] data)
	{
		if ((object)D2PacketsInfo.GCPacketTypes[data[0]] == null)
		{
			return new GCPacket(data);
		}
		return Activator.CreateInstance(D2PacketsInfo.GCPacketTypes[data[0]], data) as GCPacket;
	}
}
