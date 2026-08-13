using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x3E - Update Item Stats
/// <para>Updates an item's stat (Quantity when used, Durability when repaired...)</para>
/// <para>This packet is packed and should be looked at in bits...</para>
/// </summary>
public class UpdateItemStats : GSPacket
{
	public static readonly int NULL_Int32 = -1;

	protected int unknown8b;

	protected uint uid;

	protected Stats stats = new Stats();

	protected int unknown60b;

	protected int unknown61b = -1;

	protected int unknown78b = -1;

	protected long offset;

	protected byte[] unknownEnd;

	public uint UID => uid;

	public Stats Stats => stats;

	public int Unknown8b => unknown8b;

	public int Unknown60b => unknown60b;

	public int Unknown61b => unknown61b;

	public int Unknown78b => unknown78b;

	public byte[] UnknownEnd => unknownEnd;

	public UpdateItemStats(byte[] data)
		: base(data)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		BitReader br = new BitReader(data, 1);
		unknown8b = br.ReadInt32(10);
		uid = br.ReadUInt32();
		while (br.ReadBoolean(1))
		{
			BaseStat baseStat = BaseStat.Get(br.ReadInt32(9));
			unknown60b = br.ReadInt32(1);
			StatType type = baseStat.Type;
			if (type == StatType.ChargedSkill)
			{
				unknown61b = br.ReadInt32(1);
				int currentCharges = br.ReadInt32(8);
				int maxCharges = br.ReadInt32(8);
				unknown78b = br.ReadInt32(1);
				int level = br.ReadInt32(6);
				int skill = br.ReadInt32(10);
				stats.Add(new ChargedSkillStat(baseStat, level, skill, currentCharges, maxCharges, uid));
			}
			else if (baseStat.Signed)
			{
				stats.Add(new SignedStat(baseStat, br.ReadInt32(baseStat.SendBits)));
			}
			else
			{
				stats.Add(new UnsignedStat(baseStat, br.ReadUInt32(baseStat.SendBits)));
			}
		}
		offset = br.get_Position();
		unknownEnd = br.ReadByteArray();
	}
}
