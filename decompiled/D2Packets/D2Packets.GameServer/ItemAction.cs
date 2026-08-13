using System;
using System.Collections.Generic;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet Wrapper - Item Action
/// <para>Wrapper for <see cref="T:D2Packets.GameServer.WorldItemAction" /> and <see cref="T:D2Packets.GameServer.OwnedItemAction" />.</para>
/// </summary>
public class ItemAction : GSPacket
{
	protected ItemActionType action;

	protected ItemDestination destination;

	protected ItemCategory category;

	protected uint uid;

	protected ItemFlags flags;

	protected ItemVersion version;

	protected EquipmentLocation equipmentLocation;

	protected int x;

	protected int y;

	protected ItemLocation container = ItemLocation.Unspecified;

	protected BaseItem baseItem;

	protected BaseSetItem setItem = null;

	protected BaseUniqueItem uniqueItem = null;

	protected BaseRuneword runeword = null;

	protected ItemQuality quality = ItemQuality.NotApplicable;

	protected SuperiorItemType superiorType = SuperiorItemType.NotApplicable;

	protected ItemAffix prefix;

	protected ItemAffix suffix;

	protected MagicPrefixType[] magicPrefixes;

	protected MagicSuffixType[] magicSuffixes;

	protected string name = null;

	protected CharacterClass charClass = CharacterClass.NotApplicable;

	protected int level = -1;

	protected int usedSockets = -1;

	protected int use = -1;

	protected int graphic = -1;

	protected int color = -1;

	protected StatBase[] stats;

	protected StatBase[] mods;

	protected StatBase[][] setBonuses;

	protected int runewordID = -1;

	protected int runewordParam = -1;

	public ItemActionType Action => action;

	public ItemDestination Destination => destination;

	public EquipmentLocation EquipmentLocation => equipmentLocation;

	public ItemLocation Container => container;

	public int X => x;

	public int Y => y;

	public uint UID => uid;

	public BaseItem BaseItem => baseItem;

	public ItemQuality Quality => quality;

	public ItemAffix Prefix => prefix;

	public ItemAffix Suffix => suffix;

	public SuperiorItemType SuperiorType => superiorType;

	public BaseUniqueItem UniqueItem => uniqueItem;

	public BaseSetItem SetItem => setItem;

	public BaseRuneword Runeword => runeword;

	/// <summary>
	/// Personalised item's / ear's owner.
	/// </summary>
	public string Name => name;

	public CharacterClass Class => charClass;

	public int Level => level;

	public ItemFlags Flags => flags;

	public StatBase[] Stats => stats;

	public int UsedSockets
	{
		get
		{
			if (usedSockets == 0 && (flags & ItemFlags.Socketed) != ItemFlags.Socketed)
			{
				return -1;
			}
			return usedSockets;
		}
	}

	/// <summary>
	/// TODO: figure out what this is exactly...  tp tome = 0, id tome = 1
	/// <para>1 == action click ??</para>
	/// </summary>
	public int Use => use;

	public int Graphic => graphic;

	public int Color => color;

	public MagicPrefixType[] MagicPrefixes => magicPrefixes;

	public MagicSuffixType[] MagicSuffixes => magicSuffixes;

	public StatBase[] Mods => mods;

	public StatBase[][] SetBonuses => setBonuses;

	public ItemVersion Version => version;

	public ItemCategory Category => category;

	public int RunewordID => runewordID;

	public int RunewordParam => runewordParam;

	public ItemAction(byte[] data)
		: base(data)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		BitReader br = new BitReader(data, 1);
		action = (ItemActionType)br.ReadByte();
		br.SkipBytes(1);
		category = (ItemCategory)br.ReadByte();
		uid = br.ReadUInt32();
		if (data[0] == 157)
		{
			br.SkipBytes(5);
		}
		flags = (ItemFlags)br.ReadUInt32();
		version = (ItemVersion)br.ReadInt32(10);
		destination = (ItemDestination)br.ReadByte(3);
		if (destination == ItemDestination.Ground)
		{
			x = br.ReadUInt16();
			y = br.ReadUInt16();
		}
		else
		{
			equipmentLocation = (EquipmentLocation)br.ReadByte(4);
			x = br.ReadByte(4);
			y = br.ReadByte(3);
			container = (ItemLocation)br.ReadByte(4);
		}
		if (action == ItemActionType.AddToShop || action == ItemActionType.RemoveFromShop)
		{
			int buff = (int)(container | ItemLocation.Store);
			if ((buff & 1) == 1)
			{
				buff--;
				y += 8;
			}
			container = (ItemLocation)buff;
		}
		else if (container == ItemLocation.Unspecified)
		{
			if (equipmentLocation == EquipmentLocation.NotApplicable)
			{
				if ((Flags & ItemFlags.InSocket) == ItemFlags.InSocket)
				{
					container = ItemLocation.Item;
				}
				else if (action == ItemActionType.PutInBelt || action == ItemActionType.RemoveFromBelt || (action == ItemActionType.UpdateStats && destination == ItemDestination.Belt))
				{
					container = ItemLocation.Belt;
					y = x / 4;
					x %= 4;
				}
			}
			else
			{
				container = ItemLocation.Equipment;
			}
		}
		if ((flags & ItemFlags.Ear) == ItemFlags.Ear)
		{
			charClass = (CharacterClass)br.ReadByte(3);
			level = br.ReadByte(7);
			name = br.ReadString(7, '\0', 16);
			baseItem = BaseItem.Get(ItemClass.Ear);
			this.stats = new StatBase[0];
			mods = new StatBase[0];
			return;
		}
		baseItem = BaseItem.GetByID(category, br.ReadUInt32());
		List<StatBase> stats = new List<StatBase>();
		if (baseItem.Class == ItemClass.Gold)
		{
			stats.Add(new SignedStat(BaseStat.Get(StatType.Quantity), br.ReadInt32(br.ReadBoolean(1) ? 32 : 12)));
			this.stats = stats.ToArray();
			mods = new StatBase[0];
			return;
		}
		usedSockets = br.ReadByte(3);
		if ((flags & (ItemFlags.Compact | ItemFlags.Gamble)) != 0)
		{
			this.stats = stats.ToArray();
			mods = new StatBase[0];
			return;
		}
		level = br.ReadByte(7);
		quality = (ItemQuality)br.ReadByte(4);
		if (br.ReadBoolean(1))
		{
			graphic = br.ReadInt32(3);
		}
		if (br.ReadBoolean(1))
		{
			color = br.ReadInt32(11);
		}
		if ((flags & ItemFlags.Identified) == ItemFlags.Identified)
		{
			switch (quality)
			{
			case ItemQuality.Inferior:
				prefix = new ItemAffix(ItemAffixType.InferiorPrefix, br.ReadByte(3));
				break;
			case ItemQuality.Superior:
				prefix = new ItemAffix(ItemAffixType.SuperiorPrefix, 0);
				superiorType = (SuperiorItemType)br.ReadByte(3);
				break;
			case ItemQuality.Magic:
				prefix = new ItemAffix(ItemAffixType.MagicPrefix, br.ReadUInt16(11));
				suffix = new ItemAffix(ItemAffixType.MagicSuffix, br.ReadUInt16(11));
				break;
			case ItemQuality.Rare:
			case ItemQuality.Crafted:
				prefix = new ItemAffix(ItemAffixType.RarePrefix, br.ReadByte(8));
				suffix = new ItemAffix(ItemAffixType.RareSuffix, br.ReadByte(8));
				break;
			case ItemQuality.Set:
				setItem = BaseSetItem.Get(br.ReadUInt16(12));
				break;
			case ItemQuality.Unique:
				if (baseItem.Code != "std")
				{
					uniqueItem = BaseUniqueItem.Get(br.ReadUInt16(12));
				}
				break;
			}
		}
		if (quality == ItemQuality.Rare || quality == ItemQuality.Crafted)
		{
			List<MagicPrefixType> magicPrefixes = new List<MagicPrefixType>();
			List<MagicSuffixType> magicSuffixes = new List<MagicSuffixType>();
			for (int i = 0; i < 3; i++)
			{
				if (br.ReadBoolean(1))
				{
					magicPrefixes.Add((MagicPrefixType)br.ReadUInt16(11));
				}
				if (br.ReadBoolean(1))
				{
					magicSuffixes.Add((MagicSuffixType)br.ReadUInt16(11));
				}
			}
			this.magicPrefixes = magicPrefixes.ToArray();
			this.magicSuffixes = magicSuffixes.ToArray();
		}
		if ((Flags & ItemFlags.Runeword) == ItemFlags.Runeword)
		{
			runewordID = br.ReadUInt16(12);
			runewordParam = br.ReadUInt16(4);
			int val = -1;
			if (runewordParam == 5)
			{
				val = runewordID - runewordParam * 5;
				if (val < 100)
				{
					val--;
				}
			}
			else if (runewordParam == 2)
			{
				val = ((runewordID & 0x3FF) >> 5) + 2;
			}
			br.set_ByteOffset(br.get_ByteOffset() - 2);
			runewordParam = br.ReadUInt16();
			runewordID = val;
			if (val == -1)
			{
				throw new Exception("Unknown Runeword: " + runewordParam);
			}
			runeword = BaseRuneword.Get(val);
		}
		if ((Flags & ItemFlags.Personalized) == ItemFlags.Personalized)
		{
			name = br.ReadString(7, '\0', 16);
		}
		if (baseItem is BaseArmor)
		{
			BaseStat baseStat = BaseStat.Get(StatType.ArmorClass);
			stats.Add(new SignedStat(baseStat, br.ReadInt32(baseStat.SaveBits) - baseStat.SaveAdd));
		}
		if (baseItem is BaseArmor || baseItem is BaseWeapon)
		{
			BaseStat baseStat = BaseStat.Get(StatType.MaxDurability);
			int val = br.ReadInt32(baseStat.SaveBits);
			stats.Add(new SignedStat(baseStat, val));
			if (val > 0)
			{
				baseStat = BaseStat.Get(StatType.Durability);
				stats.Add(new SignedStat(baseStat, br.ReadInt32(baseStat.SaveBits)));
			}
		}
		if ((Flags & ItemFlags.Socketed) == ItemFlags.Socketed)
		{
			BaseStat baseStat = BaseStat.Get(StatType.Sockets);
			stats.Add(new SignedStat(baseStat, br.ReadInt32(baseStat.SaveBits)));
		}
		if (baseItem.Stackable)
		{
			if (baseItem.Useable)
			{
				use = br.ReadByte(5);
			}
			stats.Add(new SignedStat(BaseStat.Get(StatType.Quantity), br.ReadInt32(9)));
		}
		this.stats = stats.ToArray();
		if ((Flags & ItemFlags.Identified) != ItemFlags.Identified)
		{
			mods = new StatBase[0];
			return;
		}
		int setMods = ((Quality == ItemQuality.Set) ? br.ReadByte(5) : (-1));
		stats.Clear();
		StatBase stat;
		while ((stat = ReadStat(br)) != null)
		{
			stats.Add(stat);
		}
		if ((flags & ItemFlags.Runeword) == ItemFlags.Runeword)
		{
			while ((stat = ReadStat(br)) != null)
			{
				stats.Add(stat);
			}
		}
		mods = stats.ToArray();
		if (setMods == -1)
		{
			return;
		}
		setBonuses = new StatBase[5][];
		for (int i = 0; i < 5; i++)
		{
			stats.Clear();
			if ((setMods & (1 << i)) != 0)
			{
				while ((stat = ReadStat(br)) != null)
				{
					stats.Add(stat);
				}
			}
			setBonuses[i] = stats.ToArray();
		}
	}

	private StatBase ReadStat(BitReader br)
	{
		int statID = br.ReadInt32(9);
		if (statID == 511)
		{
			return null;
		}
		BaseStat baseStat = BaseStat.Get(statID);
		if (baseStat.SaveParamBits != -1)
		{
			switch (baseStat.Type)
			{
			case StatType.Reanimate:
				return new ReanimateStat(baseStat, br.ReadUInt32(baseStat.SaveParamBits), br.ReadUInt32(baseStat.SaveBits));
			case StatType.ElementalSkillBonus:
				return new ElementalSkillsBonusStat(baseStat, br.ReadInt32(baseStat.SaveParamBits), br.ReadInt32(baseStat.SaveBits));
			case StatType.ClassSkillsBonus:
				return new ClassSkillsBonusStat(baseStat, br.ReadInt32(baseStat.SaveParamBits), br.ReadInt32(baseStat.SaveBits));
			case StatType.Aura:
				return new AuraStat(baseStat, br.ReadInt32(baseStat.SaveParamBits), br.ReadInt32(baseStat.SaveBits));
			case StatType.NonClassSkill:
			case StatType.SingleSkill:
				return new SkillBonusStat(baseStat, br.ReadInt32(baseStat.SaveParamBits), br.ReadInt32(baseStat.SaveBits));
			case StatType.ChargedSkill:
				return new ChargedSkillStat(baseStat, br.ReadInt32(6), br.ReadInt32(10), br.ReadInt32(8), br.ReadInt32(8), uid);
			case StatType.SkillOnAttack:
			case StatType.SkillOnKill:
			case StatType.SkillOnDeath:
			case StatType.SkillOnStriking:
			case StatType.SkillOnLevelUp:
			case StatType.SkillOnGetHit:
				return new SkillOnEventStat(baseStat, br.ReadInt32(6), br.ReadInt32(10), br.ReadInt32(baseStat.SaveBits));
			case StatType.SkillTabBonus:
				return new SkillTabBonusStat(baseStat, br.ReadInt32(3), br.ReadInt32(3), br.ReadInt32(10), br.ReadInt32(baseStat.SaveBits));
			default:
			{
				int index = baseStat.Index;
				throw new Exception("Invalid stat: " + index);
			}
			}
		}
		if (baseStat.OpBase == StatType.Level)
		{
			return new PerLevelStat(baseStat, br.ReadInt32(baseStat.SaveBits));
		}
		switch (baseStat.Type)
		{
		case StatType.MaxDamagePercent:
		case StatType.MinDamagePercent:
			return new DamageRangeStat(baseStat, br.ReadInt32(baseStat.SaveBits), br.ReadInt32(baseStat.SaveBits));
		case StatType.FireMinDamage:
		case StatType.LightMinDamage:
		case StatType.MagicMinDamage:
			return new DamageRangeStat(baseStat, br.ReadInt32(baseStat.SaveBits), br.ReadInt32(BaseStat.Get(baseStat.Index + 1).SaveBits));
		case StatType.ColdMinDamage:
			return new ColdDamageStat(baseStat, br.ReadInt32(baseStat.SaveBits), br.ReadInt32(BaseStat.Get(StatType.ColdMaxDamage).SaveBits), br.ReadInt32(BaseStat.Get(StatType.ColdLength).SaveBits));
		case StatType.PoisonMinDamage:
			return new PoisonDamageStat(baseStat, br.ReadInt32(baseStat.SaveBits), br.ReadInt32(BaseStat.Get(StatType.PoisonMaxDamage).SaveBits), br.ReadInt32(BaseStat.Get(StatType.PoisonLength).SaveBits));
		case StatType.ReplenishDurability:
		case StatType.ReplenishQuantity:
			return new ReplenishStat(baseStat, br.ReadInt32(baseStat.SaveBits));
		default:
		{
			if (baseStat.Signed)
			{
				int val2 = br.ReadInt32(baseStat.SaveBits);
				if (baseStat.SaveAdd > 0)
				{
					val2 -= baseStat.SaveAdd;
				}
				return new SignedStat(baseStat, val2);
			}
			uint val = br.ReadUInt32(baseStat.SaveBits);
			if (baseStat.SaveAdd > 0)
			{
				val -= (uint)baseStat.SaveAdd;
			}
			return new UnsignedStat(baseStat, val);
		}
		}
	}
}
