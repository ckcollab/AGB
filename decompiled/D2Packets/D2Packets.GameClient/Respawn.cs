namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x41 - Respawn
/// <para>Restart in town (when dead.)</para>
/// </summary>
public class Respawn : GCPacket
{
	public Respawn(byte[] data)
		: base(data)
	{
	}

	public Respawn()
		: base(Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1] { 65 };
	}
}
