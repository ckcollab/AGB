namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x9C - World Item Action
/// </summary>
public class WorldItemAction : ItemAction
{
	public static readonly bool WRAPPED = true;

	public static readonly int NULL_Int32 = -1;

	public WorldItemAction(byte[] data)
		: base(data)
	{
	}
}
