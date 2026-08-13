using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x95 - Life Mana Change
/// <para>Notifies that the receiving player's life and / or mana has changed.</para>
/// <para>If stamina changes (e.g. removing a stamina granting charm), 
/// a <see cref="T:D2Packets.GameServer.WalkVerify" /> packet will be sent instead.</para>
/// </summary>
public class LifeManaChange : GSPacket
{
	protected int life;

	protected int mana;

	protected int stamina;

	protected int x;

	protected int y;

	protected byte[] unknown85b;

	public int Life => life;

	public int Mana => mana;

	public int Stamina => stamina;

	public int X => x;

	public int Y => y;

	/// <summary>
	/// 31 bits left in packet... sometimes used.
	/// </summary>
	public byte[] Unknown85b => unknown85b;

	public LifeManaChange(byte[] data)
		: base(data)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		BitReader br = new BitReader(data, 1);
		life = br.ReadInt32(15);
		mana = br.ReadInt32(15);
		stamina = br.ReadInt32(15);
		x = br.ReadInt32(16);
		y = br.ReadInt32(16);
		unknown85b = br.ReadByteArray();
	}
}
