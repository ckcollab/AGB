namespace D2Packets.RealmClient;

/// <summary>
/// Realm Client Packet 0x13 - Cancel Game Creation
/// <para>Cancels a currently pending game creation.</para>
/// <para>Note: pressing the cancel button after the game was created and client attempts to join won't trigger this packet.</para>
/// </summary>
public class CancelGameCreation : RCPacket
{
	public CancelGameCreation(byte[] data)
		: base(data)
	{
	}
}
