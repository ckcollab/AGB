using System;
using D2Packets.D2Packets;

namespace D2Packets.BnetServer;

/// <summary>
/// Base class for Battle.net Server -&gt; Client Packets
/// </summary>
public class BSPacket : D2Packet
{
	public readonly BnetServerPacket PacketType;

	public BSPacket(byte[] data)
		: base(data, PacketOrigin.BattleNetServer)
	{
		packetID = data[1];
		PacketType = (BnetServerPacket)packetID;
	}

	public static BSPacket Build(byte[] data)
	{
		if ((object)D2PacketsInfo.BSPacketTypes[data[1]] == null)
		{
			return new BSPacket(data);
		}
		return Activator.CreateInstance(D2PacketsInfo.BSPacketTypes[data[1]], data) as BSPacket;
	}
}
