using System;

namespace D2Data;

/// <summary>
/// ItemTypes.txt
/// </summary>
public class BaseItemType
{
	public readonly int Index;

	public readonly ItemType Type;

	public readonly string Name;

	public readonly string Code;

	public readonly string Equiv1;

	public readonly string Equiv2;

	public readonly bool Repair;

	public readonly bool Body;

	public readonly string BodyLoc1;

	public readonly string BodyLoc2;

	public readonly string Shoots;

	public readonly string Quiver;

	public readonly bool Throwable;

	public readonly bool Reload;

	public readonly bool ReEquip;

	public readonly bool AutoStack;

	public readonly bool Magic;

	public readonly bool Rare;

	public readonly bool Normal;

	public readonly bool Charm;

	public readonly bool Gem;

	public readonly bool Beltable;

	public readonly int MaxSock1;

	public readonly int MaxSock25;

	public readonly int MaxSock40;

	public readonly int TreasureClass;

	public readonly int Rarity;

	public readonly CharacterClass StaffMods;

	public readonly int CostFormula;

	public readonly CharacterClass Class;

	public readonly int VarInvGfx;

	public readonly string InvGfx1;

	public readonly string InvGfx2;

	public readonly string InvGfx3;

	public readonly string InvGfx4;

	public readonly string InvGfx5;

	public readonly string InvGfx6;

	public readonly StorePage StorePage;

	private static int ItemCount = 0;

	protected static BaseItemType[] items = new BaseItemType[103]
	{
		new BaseItemType(ItemCount++, "None", "", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "None", "", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Shield", "shie", "shld", "", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 3, 4, 0, 3, -1, 1, -1, 0, "", "", "", "", "", "", 1),
		new BaseItemType(ItemCount++, "Armor", "tors", "armo", "", 1, 1, "tors", "tors", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 4, 6, 0, 3, -1, 1, -1, 0, "", "", "", "", "", "", 1),
		new BaseItemType(ItemCount++, "Gold", "gold", "misc", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Bow Quiver", "bowq", "misl", "", 0, 1, "rarm", "larm", "", "bow", 0, 1, 0, 1, -1, -1, 1, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Crossbow Quiver", "xboq", "misl", "", 0, 1, "rarm", "larm", "", "xbow", 0, 1, 0, 1, -1, -1, 1, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Player Body Part", "play", "misc", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 1, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Herb", "herb", "misc", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Potion", "poti", "misc", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 0, 1, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Ring", "ring", "misc", "", 0, 1, "rrin", "lrin", "", "", 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 5, "invrin1", "invrin2", "invrin3", "invrin4", "invrin5", "", 3),
		new BaseItemType(ItemCount++, "Elixir", "elix", "misc", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 0, 0, 0, 1, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Amulet", "amul", "misc", "", 0, 1, "neck", "neck", "", "", 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 3, "invamu1", "invamu2", "invamu3", "", "", "", 3),
		new BaseItemType(ItemCount++, "Charm", "char", "misc", "", 0, 0, "", "", "", "", 0, 0, 0, 0, 1, -1, 0, 1, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 3, "invch1", "invch4", "invch7", "", "", "", 3),
		new BaseItemType(ItemCount++, "Not Used", "", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Boots", "boot", "armo", "", 1, 1, "feet", "feet", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 1, -1, 0, "", "", "", "", "", "", 1),
		new BaseItemType(ItemCount++, "Gloves", "glov", "armo", "", 1, 1, "glov", "glov", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 1, -1, 0, "", "", "", "", "", "", 1),
		new BaseItemType(ItemCount++, "Not Used", "", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Book", "book", "misc", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Belt", "belt", "armo", "", 1, 1, "belt", "belt", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 1, -1, 0, "", "", "", "", "", "", 1),
		new BaseItemType(ItemCount++, "Gem", "gem", "sock", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Torch", "torc", "misc", "", 0, 1, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Scroll", "scro", "misc", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 0, 1, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Not Used", "", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Scepter", "scep", "rod", "", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 5, 6, 0, 1, 3, 0, -1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Wand", "wand", "rod", "", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 2, 2, 2, 0, 1, 2, 0, -1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Staff", "staf", "rod", "", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 5, 6, 6, 0, 1, 1, 0, -1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Bow", "bow", "miss", "", 0, 1, "rarm", "larm", "bowq", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 4, 6, 1, 3, -1, 0, -1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Axe", "axe", "mele", "", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 4, 5, 6, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Club", "club", "blun", "", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 4, 6, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Sword", "swor", "mele", "", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 4, 6, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Hammer", "hamm", "blun", "", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 4, 6, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Knife", "knif", "mele", "", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 2, 3, 3, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Spear", "spea", "mele", "", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 4, 6, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Polearm", "pole", "mele", "", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 4, 6, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Crossbow", "xbow", "miss", "", 1, 1, "rarm", "larm", "xboq", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 4, 6, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Mace", "mace", "blun", "", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 4, 6, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Helm", "helm", "armo", "", 1, 1, "head", "head", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 2, 2, 3, 0, 3, -1, 1, -1, 0, "", "", "", "", "", "", 1),
		new BaseItemType(ItemCount++, "Missile Potion", "tpot", "thro", "", 0, 1, "rarm", "larm", "", "", 1, 1, 1, 1, -1, -1, 1, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Quest", "ques", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Body Part", "body", "misc", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 0, 0, 0, 0, 0, 0, 3, -1, 2, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Key", "key", "misc", "", 0, 0, "", "", "", "", 0, 0, 0, 1, -1, -1, 1, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Throwing Knife", "tkni", "comb", "knif", 1, 1, "rarm", "larm", "", "", 1, 1, 1, 1, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Throwing Axe", "taxe", "comb", "axe", 1, 1, "rarm", "larm", "", "", 1, 1, 1, 1, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Javelin", "jave", "comb", "spea", 1, 1, "rarm", "larm", "", "", 1, 1, 1, 1, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Weapon", "weap", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Melee Weapon", "mele", "weap", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Missile Weapon", "miss", "weap", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Thrown Weapon", "thro", "weap", "", 0, 0, "", "", "", "", 1, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Combo Weapon", "comb", "mele", "thro", 0, 0, "", "", "", "", 1, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Any Armor", "armo", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Any Shield", "shld", "armo", "seco", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Miscellaneous", "misc", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Socket Filler", "sock", "misc", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Second Hand", "seco", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Staves And Rods", "rod", "blun", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Missile", "misl", "misc", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Blunt", "blun", "mele", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Jewel", "jewl", "sock", "", 0, 0, "", "", "", "", 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 6, "invjw1", "invjw2", "invjw3", "invjw4", "invjw5", "invjw6", 3),
		new BaseItemType(ItemCount++, "Class Specific", "clas", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Amazon Item", "amaz", "clas", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, 0, 0, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Barbarian Item", "barb", "clas", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, 0, 4, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Necromancer Item", "necr", "clas", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, 0, 2, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Paladin Item", "pala", "clas", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, 0, 3, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Sorceress Item", "sorc", "clas", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, 0, 1, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Assassin Item", "assn", "clas", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, -1, 0, 6, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Druid Item", "drui", "clas", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, 0, 5, 0, "", "", "", "", "", "", -1),
		new BaseItemType(ItemCount++, "Hand to Hand", "h2h", "mele", "assn", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 2, 3, 3, 0, 2, -1, 0, 6, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Orb", "orb", "weap", "sorc", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 2, 3, 3, 0, 1, 1, 0, 1, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Voodoo Heads", "head", "shld", "necr", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 2, 3, 3, 0, 1, 2, 0, 2, 0, "", "", "", "", "", "", 1),
		new BaseItemType(ItemCount++, "Auric Shields", "ashd", "shld", "pala", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 4, 4, 0, 1, -1, 0, 3, 0, "", "", "", "", "", "", 1),
		new BaseItemType(ItemCount++, "Primal Helm", "phlm", "helm", "barb", 1, 1, "head", "head", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 2, 3, 3, 0, 1, 4, 0, 4, 0, "", "", "", "", "", "", 1),
		new BaseItemType(ItemCount++, "Pelt", "pelt", "helm", "drui", 1, 1, "head", "head", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 2, 3, 3, 0, 1, 5, 0, 5, 0, "", "", "", "", "", "", 1),
		new BaseItemType(ItemCount++, "Cloak", "cloa", "tors", "assn", 1, 1, "tors", "tors", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 6, 0, 6, 0, "", "", "", "", "", "", 1),
		new BaseItemType(ItemCount++, "Rune", "rune", "sock", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Circlet", "circ", "helm", "", 1, 1, "head", "head", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 1, 2, 3, 0, 3, -1, 1, -1, 0, "", "", "", "", "", "", 1),
		new BaseItemType(ItemCount++, "Healing Potion", "hpot", "poti", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 0, 1, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Mana Potion", "mpot", "poti", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 0, 1, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Rejuvenation Potion", "rpot", "hpot", "mpot", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 0, 1, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Stamina Potion", "spot", "poti", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 0, 1, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Antidote Potion", "apot", "poti", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 0, 1, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Thawing Potion", "wpot", "poti", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 0, 1, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Small Charm", "scha", "char", "", 0, 0, "", "", "", "", 0, 0, 0, 0, 1, -1, 0, 1, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 3, "invch1", "invch4", "invch7", "", "", "", 3),
		new BaseItemType(ItemCount++, "Medium Charm", "mcha", "char", "", 0, 0, "", "", "", "", 0, 0, 0, 0, 1, -1, 0, 1, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 3, "invch2", "invch5", "invch8", "", "", "", 3),
		new BaseItemType(ItemCount++, "Large Charm", "lcha", "char", "", 0, 0, "", "", "", "", 0, 0, 0, 0, 1, -1, 0, 1, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 3, "invch3", "invch6", "invch9", "", "", "", 3),
		new BaseItemType(ItemCount++, "Amazon Bow", "abow", "bow", "amaz", 0, 1, "rarm", "larm", "bowq", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 4, 5, 1, 1, -1, 0, 0, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Amazon Spear", "aspe", "spea", "amaz", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 3, 4, 6, 0, 1, -1, 0, 0, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Amazon Javelin", "ajav", "jave", "amaz", 1, 1, "rarm", "larm", "", "", 1, 1, 1, 1, -1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, 0, 0, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Hand to Hand 2", "h2h2", "h2h", "", 1, 1, "rarm", "larm", "", "", 0, 0, 0, 0, -1, 1, 0, 0, 0, 0, 2, 3, 3, 0, 2, 6, 0, 6, 0, "", "", "", "", "", "", 2),
		new BaseItemType(ItemCount++, "Magic Bow Quiver", "mboq", "bowq", "", 0, 1, "rarm", "larm", "", "bow", 0, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Magic Xbow Quiver", "mxbq", "xboq", "", 0, 1, "rarm", "larm", "", "xbow", 0, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Chipped Gem", "gem0", "gem", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Flawed Gem", "gem1", "gem", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Standard Gem", "gem2", "gem", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Flawless Gem", "gem3", "gem", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Perfect Gem", "gem4", "gem", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Amethyst", "gema", "gem", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Diamond", "gemd", "gem", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Emerald", "geme", "gem", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Ruby", "gemr", "gem", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Sapphire", "gems", "gem", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Topaz", "gemt", "gem", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3),
		new BaseItemType(ItemCount++, "Skull", "gemz", "gem", "", 0, 0, "", "", "", "", 0, 0, 0, 0, -1, -1, 1, 0, 1, 0, 0, 0, 0, 0, 3, -1, 0, -1, 0, "", "", "", "", "", "", 3)
	};

	public BaseItemType(int Index, string Name, string Code, string Equiv1, string Equiv2, int Repair, int Body, string BodyLoc1, string BodyLoc2, string Shoots, string Quiver, int Throwable, int Reload, int ReEquip, int AutoStack, int Magic, int Rare, int Normal, int Charm, int Gem, int Beltable, int MaxSock1, int MaxSock25, int MaxSock40, int TreasureClass, int Rarity, int StaffMods, int CostFormula, int Class, int VarInvGfx, string InvGfx1, string InvGfx2, string InvGfx3, string InvGfx4, string InvGfx5, string InvGfx6, int StorePage)
	{
		this.Index = Index;
		Type = (ItemType)Index;
		this.Name = Name;
		this.Code = Code;
		this.Equiv1 = Equiv1;
		this.Equiv2 = Equiv2;
		this.Repair = Repair == 1;
		this.Body = Body == 1;
		this.BodyLoc1 = BodyLoc1;
		this.BodyLoc2 = BodyLoc2;
		this.Shoots = Shoots;
		this.Quiver = Quiver;
		this.Throwable = Throwable == 1;
		this.Reload = Reload == 1;
		this.ReEquip = ReEquip == 1;
		this.AutoStack = AutoStack == 1;
		this.Magic = Magic == 1;
		this.Rare = Rare == 1;
		this.Normal = Normal == 1;
		this.Charm = Charm == 1;
		this.Gem = Gem == 1;
		this.Beltable = Beltable == 1;
		this.MaxSock1 = MaxSock1;
		this.MaxSock25 = MaxSock25;
		this.MaxSock40 = MaxSock40;
		this.TreasureClass = TreasureClass;
		this.Rarity = Rarity;
		this.StaffMods = (CharacterClass)StaffMods;
		this.CostFormula = CostFormula;
		this.Class = (CharacterClass)Class;
		this.VarInvGfx = VarInvGfx;
		this.InvGfx1 = InvGfx1;
		this.InvGfx2 = InvGfx2;
		this.InvGfx3 = InvGfx3;
		this.InvGfx4 = InvGfx4;
		this.InvGfx5 = InvGfx5;
		this.InvGfx6 = InvGfx6;
		this.StorePage = (StorePage)StorePage;
	}

	public static BaseItemType Get(int index)
	{
		if (index < 0 || index >= ItemCount)
		{
			throw new ArgumentOutOfRangeException();
		}
		return items[index];
	}

	public static BaseItemType Get(ItemType type)
	{
		if (type < ItemType.NotUsed1 || (int)type >= ItemCount)
		{
			throw new ArgumentOutOfRangeException();
		}
		return items[(int)type];
	}

	public static BaseItemType GetByCode(string code)
	{
		code = code.ToLower();
		for (int i = 0; i < ItemCount; i++)
		{
			if (items[i].Code == code)
			{
				return items[i];
			}
		}
		throw new ArgumentException($"No BaseItemType with code {code} found !");
	}
}
