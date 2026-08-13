using System;
using D2Packets.D2Packets;

namespace D2Packets.GameServer;

/// <summary>
/// Base class for D2GS Game Server -&gt; Client Packets
/// </summary>
public class GSPacket : D2Packet
{
	public readonly GameServerPacket PacketType;

	public GSPacket(byte[] data)
		: base(data, PacketOrigin.GameServer)
	{
		packetID = data[0];
		PacketType = (GameServerPacket)packetID;
	}

	public static GSPacket Build(byte[] data)
	{
		if ((object)D2PacketsInfo.GSPacketTypes[data[0]] == null)
		{
			return new GSPacket(data);
		}
		return Activator.CreateInstance(D2PacketsInfo.GSPacketTypes[data[0]], data) as GSPacket;
	}
}
