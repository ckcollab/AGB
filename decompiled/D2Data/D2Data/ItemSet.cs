using System;
using System.Collections.Generic;

namespace D2Data;

/// <summary>
/// Item Sets Base Info (Sets.txt)
/// </summary>
public class ItemSet
{
	public readonly int Index;

	public readonly ItemSetType Type;

	public readonly string Name;

	public readonly string Name2;

	public readonly int Version;

	public readonly int Level;

	public readonly ItemProperty[][] PartialMods;

	public readonly ItemProperty[] FullMods;

	private static int ItemCount = 0;

	private static ItemSet[] items = new ItemSet[32]
	{
		new ItemSet(ItemCount++, "Civerb's Vestments", "Civerb's Vestments", 0, 13, "res-fire", "", 15, 15, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "str", "0", 15, 15, "dmg-undead", "0", 200, 200, "res-ltng", "0", 25, 25, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Hsarus' Defense", "Hsarus' Defense", 0, 4, "thorns", "", 5, 5, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "dmg-max", "0", 5, 5, "nofreeze", "0", 1, 1, "res-ltng", "0", 25, 25, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Cleglaw's Brace", "Cleglaw's Brace", 0, 6, "ac", "", 50, 50, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "ac", "0", 50, 50, "manasteal", "0", 6, 6, "crush", "0", 35, 35, "swing2", "", 20, 20, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Iratha's Finery", "Iratha's Finery", 0, 21, "ac", "", 50, 50, "", "", -1, -1, "move2", "", 20, 20, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "res-all", "0", 20, 20, "res-fire-max", "0", 10, 10, "res-cold-max", "0", 10, 10, "res-ltng-max", "0", 10, 10, "res-pois-max", "0", 10, 10, "dex", "0", 15, 15, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Isenhart's Armory", "Isenhart's Armory", 0, 11, "str", "", 10, 10, "", "", -1, -1, "dex", "", 10, 10, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "lifesteal", "0", 5, 5, "res-all", "0", 10, 10, "att%", "0", 35, 35, "block", "", 30, 30, "move2", "", 20, 20, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Vidala's Rig", "Vidala's Rig", 0, 19, "att", "", 75, 75, "", "", -1, -1, "dex", "", 15, 15, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "dmg-cold", "50", 15, 20, "freeze", "0", 1, 1, "pierce", "0", 50, 50, "str", "", 10, 10, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Milabrega's Regalia", "Milabrega's Regalia", 0, 23, "att", "", 75, 75, "", "", -1, -1, "att", "", 125, 125, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "lifesteal", "0", 8, 8, "pal", "0", 2, 2, "manasteal", "0", 10, 10, "res-pois", "0", 15, 15, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Cathan's Traps", "Cathan's Traps", 0, 15, "dmg-fire", "", 15, 20, "", "", -1, -1, "res-ltng", "", 25, 25, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "att", "0", 60, 60, "red-mag", "0", 3, 3, "res-all", "0", 25, 25, "cast1", "0", 10, 10, "mana", "0", 20, 20, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Tancred's Battlegear", "Tancred's Battlegear", 0, 27, "dmg-ltng", "", 15, 15, "", "", -1, -1, "lifesteal", "", 5, 5, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "gold%", "0", 75, 75, "res-all", "0", 10, 10, "slow", "0", 35, 35, "manasteal", "0", 5, 5, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Sigon's Complete Steel", "Sigon's Complete Steel", 0, 9, "lifesteal", "", 10, 10, "", "", -1, -1, "ac", "", 100, 100, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "res-fire", "0", 12, 12, "thorns", "0", 12, 12, "red-dmg", "0", 7, 7, "fire-max", "0", 24, 24, "mana", "", 20, 20, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Infernal Tools", "Infernal Tools", 0, 7, "dmg-pois", "80", 25, 25, "", "", -1, -1, "mana", "", 10, 10, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "att%", "0", 20, 20, "nec", "0", 1, 1, "openwounds", "0", 20, 20, "manasteal", "", 6, 6, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Berserker's Garb", "Berserker's Garb", 0, 5, "hp", "", 50, 50, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "res-pois-len", "0", 75, 75, "pois-min", "0", 16, 16, "pois-max", "0", 32, 32, "pois-len", "0", 75, 75, "ac", "", 75, 75, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Death's Disguise", "Death's Disguise", 0, 8, "lifesteal", "", 8, 8, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "att%", "0", 40, 40, "dmg-min", "0", 10, 10, "res-all", "0", 25, 25, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Angelic Raiment", "Angelic Raiment", 0, 17, "dex", "", 10, 10, "", "", -1, -1, "mana", "", 50, 50, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "res-all", "0", 25, 25, "half-freeze", "0", 1, 1, "mag%", "0", 40, 40, "regen-mana", "", 8, 8, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Arctic Gear", "Arctic Gear", 0, 3, "str", "", 5, 5, "", "", -1, -1, "hp", "", 50, 50, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "cold-min", "0", 6, 6, "cold-max", "0", 14, 14, "cold-len", "0", 100, 100, "nofreeze", "0", 1, 1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Arcanna's Tricks", "Arcanna's Tricks", 0, 20, "mana", "", 25, 25, "", "", -1, -1, "hp", "", 50, 50, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "cast3", "0", 20, 20, "manasteal", "0", 5, 5, "mana", "0", 25, 25, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Natalya's Odium", "Natalya's Odium", 100, 22, "red-mag", "", 15, 15, "", "", -1, -1, "ac", "", 200, 200, "", "", -1, -1, "res-pois", "", 20, 20, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "res-all", "", 50, 50, "ass", "", 3, 3, "ac", "", 150, 150, "lifesteal", "", 14, 14, "manasteal", "", 14, 14, "red-dmg%", "", 30, 30, "fade", "", 1, 1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Aldur's Watchtower", "Aldur's Watchtower", 100, 29, "att%", "", 150, 150, "", "", -1, -1, "mag%", "", 50, 50, "", "", -1, -1, "lifesteal", "", 10, 10, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "res-all", "", 50, 50, "dru", "", 3, 3, "ac", "", 150, 150, "manasteal", "", 10, 10, "mana", "", 150, 150, "dmg%", "", 350, 350, "state", "fullsetgeneric", 1, 1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Immortal King", "Immortal King", 100, 37, "att", "", 50, 50, "", "", -1, -1, "att", "", 75, 75, "", "", -1, -1, "att", "", 125, 125, "", "", -1, -1, "att", "", 200, 200, "", "", -1, -1, "res-all", "", 50, 50, "bar", "", 3, 3, "hp", "", 150, 150, "red-mag", "", 10, 10, "state", "fullsetgeneric", 1, 1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Tal Rasha's Wrappings", "Tal Rasha's Wrappings", 100, 26, "regen", "", 10, 10, "", "", -1, -1, "mag%", "", 65, 65, "", "", -1, -1, "balance2", "", 25, 25, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "res-all", "", 50, 50, "sor", "", 3, 3, "ac", "", 150, 150, "hp", "", 150, 150, "ac-miss", "", 50, 50, "state", "fullsetgeneric", 1, 1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Griswold's Legacy", "Griswold's Legacy", 100, 44, "str", "", 20, 20, "", "", -1, -1, "dex", "", 30, 30, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "res-all", "", 50, 50, "pal", "", 3, 3, "att", "", 200, 200, "hp", "", 150, 150, "state", "fullsetgeneric", 1, 1, "balance2", "", 30, 30, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Trang-Oul's Avatar", "Trang-Oul's Avatar", 100, 32, "regen-mana", "", 15, 15, "oskill", "FireBall", 18, 18, "regen-mana", "", 15, 15, "oskill", "FireWall", 13, 13, "regen-mana", "", 15, 15, "oskill", "Meteor", 10, 10, "", "", -1, -1, "", "", -1, -1, "res-all", "", 50, 50, "nec", "", 3, 3, "mana", "", 100, 100, "ac", "", 200, 200, "state", "monsterset", 1, 1, "regen-mana", "", 15, 15, "oskill", "FireMastery", 3, 3, "lifesteal", "", 20, 20),
		new ItemSet(ItemCount++, "M'avina's Battle Hymn", "M'avina's Battle Hymn", 100, 21, "str", "", 20, 20, "", "", -1, -1, "dex", "", 30, 30, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "res-all", "", 50, 50, "ama", "", 3, 3, "ac", "", 100, 100, "att", "", 100, 100, "mag%", "", 100, 100, "state", "fullsetgeneric", 1, 1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "The Disciple", "The Disciple", 100, 39, "ac", "", 150, 150, "", "", -1, -1, "dmg-pois", "75", 75, 75, "", "", -1, -1, "str", "", 10, 10, "", "", -1, -1, "dex", "", 10, 10, "", "", -1, -1, "res-all", "", 50, 50, "allskills", "", 2, 2, "mana", "", 100, 100, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Heaven's Brethren", "Heaven's Brethren", 100, 55, "regen-stam", "", 50, 50, "", "", -1, -1, "regen", "", 20, 20, "", "", -1, -1, "dmg-fire", "", 30, 30, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "res-all", "", 50, 50, "allskills", "", 2, 2, "nofreeze", "", 1, 1, "light", "", 5, 5, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Orphan's Call", "Orphan's Call", 100, 41, "hp", "", 35, 35, "", "", -1, -1, "thorns", "", 5, 5, "", "", -1, -1, "ac", "", 100, 100, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "hp", "", 50, 50, "dex", "", 10, 10, "str", "", 20, 20, "ac", "", 100, 100, "res-all", "", 15, 15, "mag%", "0", 80, 80, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Hwanin's Majesty", "Hwanin's Majesty", 100, 28, "ac", "", 100, 100, "", "", -1, -1, "ac", "", 200, 200, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "allskills", "", 2, 2, "lifesteal", "", 20, 20, "move3", "", 30, 30, "res-all", "", 30, 30, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Sazabi's Grand Tribute", "Sazabi's Grand Tribute", 100, 34, "move3", "", 40, 40, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "res-all", "", 30, 30, "lifesteal", "", 15, 15, "hp%", "", 27, 27, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Bul-Kathos' Children", "Bul-Kathos' Children", 100, 50, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "dmg-fire", "", 20, 20, "allskills", "", 2, 2, "att", "", 200, 200, "ac", "", 25, 25, "dmg-undead", "", 200, 200, "dmg-demon", "", 200, 200, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Cow King's Leathers", "Cow King's Leathers", 100, 20, "res-pois", "", 25, 25, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "stam", "", 100, 100, "str", "", 20, 20, "gold%", "", 100, 100, "mag%", "", 100, 100, "gethit-skill", "42", 25, 5, "swing3", "", 30, 30, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "Naj's Ancient Set", "Naj's Ancient Set", 100, 43, "ac", "", 175, 175, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "allskills", "", 1, 1, "regen", "", 10, 10, "dex", "", 15, 15, "res-all", "", 50, 50, "str", "", 20, 20, "mana", "", 100, 100, "", "", -1, -1, "", "", -1, -1),
		new ItemSet(ItemCount++, "McAuley's Folly", "McAuley's Folly", 100, 20, "ac", "", 50, 50, "", "", -1, -1, "att", "", 75, 75, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "allskills", "", 1, 1, "mag%", "", 50, 50, "mana", "", 50, 50, "lifesteal", "", 4, 4, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1, "", "", -1, -1)
	};

	public int Items => BaseSetItem.GetItemCount(Type);

	public ItemSet(int index, string name, string name2, int version, int level, string PCode2a, string PParam2a, int PMin2a, int PMax2a, string PCode2b, string PParam2b, int PMin2b, int PMax2b, string PCode3a, string PParam3a, int PMin3a, int PMax3a, string PCode3b, string PParam3b, int PMin3b, int PMax3b, string PCode4a, string PParam4a, int PMin4a, int PMax4a, string PCode4b, string PParam4b, int PMin4b, int PMax4b, string PCode5a, string PParam5a, int PMin5a, int PMax5a, string PCode5b, string PParam5b, int PMin5b, int PMax5b, string FCode1, string FParam1, int FMin1, int FMax1, string FCode2, string FParam2, int FMin2, int FMax2, string FCode3, string FParam3, int FMin3, int FMax3, string FCode4, string FParam4, int FMin4, int FMax4, string FCode5, string FParam5, int FMin5, int FMax5, string FCode6, string FParam6, int FMin6, int FMax6, string FCode7, string FParam7, int FMin7, int FMax7, string FCode8, string FParam8, int FMin8, int FMax8)
	{
		Index = index;
		Type = (ItemSetType)index;
		Name = name;
		Name2 = name2;
		Version = version;
		Level = level;
		PartialMods = new ItemProperty[4][];
		if (PCode2b.Length > 0)
		{
			PartialMods[0] = new ItemProperty[2]
			{
				new ItemProperty(PCode2a, PParam2a, PMin2a, PMax2a),
				new ItemProperty(PCode2b, PParam2b, PMin2b, PMax2b)
			};
		}
		else if (PCode2a.Length > 0)
		{
			PartialMods[0] = new ItemProperty[1]
			{
				new ItemProperty(PCode2a, PParam2a, PMin2a, PMax2a)
			};
		}
		else
		{
			PartialMods[0] = new ItemProperty[0];
		}
		if (PCode3b.Length > 0)
		{
			PartialMods[1] = new ItemProperty[2]
			{
				new ItemProperty(PCode3a, PParam3a, PMin3a, PMax3a),
				new ItemProperty(PCode3b, PParam3b, PMin3b, PMax3b)
			};
		}
		else if (PCode3a.Length > 0)
		{
			PartialMods[1] = new ItemProperty[1]
			{
				new ItemProperty(PCode3a, PParam3a, PMin3a, PMax3a)
			};
		}
		else
		{
			PartialMods[1] = new ItemProperty[0];
		}
		if (PCode4b.Length > 0)
		{
			PartialMods[2] = new ItemProperty[2]
			{
				new ItemProperty(PCode4a, PParam4a, PMin4a, PMax4a),
				new ItemProperty(PCode4b, PParam4b, PMin4b, PMax4b)
			};
		}
		else if (PCode4a.Length > 0)
		{
			PartialMods[2] = new ItemProperty[1]
			{
				new ItemProperty(PCode4a, PParam4a, PMin4a, PMax4a)
			};
		}
		else
		{
			PartialMods[2] = new ItemProperty[0];
		}
		if (PCode5b.Length > 0)
		{
			PartialMods[3] = new ItemProperty[2]
			{
				new ItemProperty(PCode5a, PParam5a, PMin5a, PMax5a),
				new ItemProperty(PCode5b, PParam5b, PMin5b, PMax5b)
			};
		}
		else if (PCode5a.Length > 0)
		{
			PartialMods[3] = new ItemProperty[1]
			{
				new ItemProperty(PCode5a, PParam5a, PMin5a, PMax5a)
			};
		}
		else
		{
			PartialMods[3] = new ItemProperty[0];
		}
		List<ItemProperty> list = new List<ItemProperty>();
		if (FCode1.Length > 0)
		{
			list.Add(new ItemProperty(FCode1, FParam1, FMin1, FMax1));
		}
		if (FCode2.Length > 0)
		{
			list.Add(new ItemProperty(FCode2, FParam2, FMin2, FMax2));
		}
		if (FCode3.Length > 0)
		{
			list.Add(new ItemProperty(FCode3, FParam3, FMin3, FMax3));
		}
		if (FCode4.Length > 0)
		{
			list.Add(new ItemProperty(FCode4, FParam4, FMin4, FMax4));
		}
		if (FCode5.Length > 0)
		{
			list.Add(new ItemProperty(FCode5, FParam5, FMin5, FMax5));
		}
		if (FCode6.Length > 0)
		{
			list.Add(new ItemProperty(FCode6, FParam6, FMin6, FMax6));
		}
		if (FCode7.Length > 0)
		{
			list.Add(new ItemProperty(FCode7, FParam7, FMin7, FMax7));
		}
		if (FCode8.Length > 0)
		{
			list.Add(new ItemProperty(FCode8, FParam8, FMin8, FMax8));
		}
		FullMods = list.ToArray();
	}

	public override string ToString()
	{
		return Name;
	}

	public static ItemSet Get(int index)
	{
		if (index < 0 || index >= ItemCount)
		{
			throw new ArgumentOutOfRangeException();
		}
		return items[index];
	}

	public static ItemSet Get(ItemSetType type)
	{
		if (type < ItemSetType.CiverbsVestments || (int)type >= ItemCount)
		{
			throw new ArgumentOutOfRangeException();
		}
		return items[(int)type];
	}

	public static ItemSet GetByName(string name)
	{
		for (int i = 0; i < ItemCount; i++)
		{
			if (items[i].Name == name)
			{
				return items[i];
			}
		}
		throw new ArgumentException($"No BaseItem named {name} found !");
	}
}
