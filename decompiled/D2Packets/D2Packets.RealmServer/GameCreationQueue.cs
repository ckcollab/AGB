using System;

namespace D2Packets.RealmServer;

/// <summary>
/// Realm Server Packet 0x14 - Game Creation Queue
/// <para>Initialize waiting queue or update queue position.</para>
/// </summary>
public class GameCreationQueue : RSPacket
{
	protected uint position;

	public uint Position => position;

	public GameCreationQueue(byte[] data)
		: base(data)
	{
		position = BitConverter.ToUInt32(data, 3);
	}
}
