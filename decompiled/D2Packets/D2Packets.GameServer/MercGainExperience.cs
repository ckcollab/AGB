using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet Wrapper - Merc Gain Experience
/// <para>Wrapper for <see cref="T:D2Packets.GameServer.MercByteToExperience" /> and <see cref="T:D2Packets.GameServer.MercWordToExperience" />.</para>
/// </summary>
public class MercGainExperience : GSPacket
{
	protected byte id;

	protected uint uid;

	protected uint experience;

	protected byte ID => id;

	protected uint UID => uid;

	public uint Experience => experience;

	public MercGainExperience(byte[] data)
		: base(data)
	{
		id = data[1];
		uid = BitConverter.ToUInt32(data, 2);
	}
}
