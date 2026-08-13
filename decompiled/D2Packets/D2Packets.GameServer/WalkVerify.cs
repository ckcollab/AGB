using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x96 - Walk Verify
/// <para>Used to update your stamina and sync up with server during run / walk.</para>
/// <para>This is also sent if stamina changes due to items or regeneration, with State == 0.</para>
/// </summary>
public class WalkVerify : GSPacket
{
	protected int stamina;

	protected int x;

	protected int y;

	protected int state;

	public int X => x;

	public int Y => y;

	public int Stamina => stamina;

	/// <summary>
	/// Some kind of state or count...
	/// <para>If 0, the player is done moving; otherwise another Walk Verify will usually be sent shortly afterwards.</para>
	/// </summary>
	public int State => state;

	public WalkVerify(byte[] data)
		: base(data)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		BitReader br = new BitReader(data, 1);
		stamina = br.ReadInt32(15);
		x = br.ReadInt32(16);
		y = br.ReadInt32(16);
		state = br.ReadInt32(17);
	}
}
