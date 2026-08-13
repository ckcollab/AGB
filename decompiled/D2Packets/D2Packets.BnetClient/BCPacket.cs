using System;
using D2Packets.D2Packets;

namespace D2Packets.BnetClient;

/// <summary>
/// Base class for Battle.net Client -&gt; Server Packets
/// </summary>
public class BCPacket : D2Packet
{
	public readonly BnetClientPacket PacketType;

	public BCPacket(byte[] data)
		: base(data, PacketOrigin.BattleNetClient)
	{
		packetID = data[1];
		PacketType = (BnetClientPacket)packetID;
	}

	public static BCPacket Build(byte[] data)
	{
		if ((object)D2PacketsInfo.BCPacketTypes[data[1]] == null)
		{
			return new BCPacket(data);
		}
		return Activator.CreateInstance(D2PacketsInfo.BCPacketTypes[data[1]], data) as BCPacket;
	}
}
