using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet Wrapper - Attribute Notification
/// <para>Wrapper for <see cref="T:D2Packets.GameServer.AttributeByte" />, <see cref="T:D2Packets.GameServer.AttributeWord" /> and 
/// <see cref="T:D2Packets.GameServer.AttributeDWord" />.</para>
/// </summary>
public class AttributeNotification : GSPacket
{
	protected StatBase stat;

	public StatBase Stat => stat;

	public AttributeNotification(byte[] data)
		: base(data)
	{
	}
}
