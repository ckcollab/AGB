using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet Wrapper - Merc Attribute Notification
/// <para>Wrapper for <see cref="T:D2Packets.GameServer.MercAttributeByte" />, <see cref="T:D2Packets.GameServer.MercAttributeWord" /> and 
/// <see cref="T:D2Packets.GameServer.MercAttributeDWord" />.</para>
/// <para>TEST: also used for pets ?</para>
/// </summary>
public class MercAttributeNotification : GSPacket
{
	protected StatBase stat;

	protected uint uid;

	public uint UID => uid;

	public StatBase Stat => stat;

	public MercAttributeNotification(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 2);
	}
}
