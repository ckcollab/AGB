using System;

namespace D2Data;

public class BaseGem
{
	public readonly int Index;

	public readonly ItemClass Type;

	public readonly int Transform;

	public readonly int NumMods;

	public readonly ItemProperty[] WeaponMods;

	public readonly ItemProperty[] HelmMods;

	public readonly ItemProperty[] ShieldMods;

	public static readonly int ItemCount = 0;

	private static BaseGem[] items = new BaseGem[68]
	{
		new BaseGem(ItemCount++, ItemClass.ChippedAmethyst, 18, 3, "att", 0, 40, 40, null, -1, -1, -1, null, 0, 0, 0, "str", 0, 3, 3, null, 0, 0, 0, null, 0, 0, 0, "ac", 0, 8, 8, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawedAmethyst, 18, 3, "att", 0, 60, 60, null, -1, -1, -1, null, 0, 0, 0, "str", 0, 4, 4, null, 0, 0, 0, null, 0, 0, 0, "ac", 0, 12, 12, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.Amethyst, 18, 3, "att", 0, 80, 80, null, -1, -1, -1, null, 0, 0, 0, "str", 0, 6, 6, null, 0, 0, 0, null, 0, 0, 0, "ac", 0, 18, 18, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawlessAmethyst, 18, 3, "att", 0, 100, 100, null, -1, -1, -1, null, 0, 0, 0, "str", 0, 8, 8, null, 0, 0, 0, null, 0, 0, 0, "ac", 0, 24, 24, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.PerfectAmethyst, 17, 3, "att", 0, 150, 150, null, -1, -1, -1, null, 0, 0, 0, "str", 0, 10, 10, null, 0, 0, 0, null, 0, 0, 0, "ac", 0, 30, 30, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.ChippedSaphire, 5, 3, "cold-min", 0, 1, 1, "cold-max", 0, 3, 3, "cold-len", 0, 25, 25, "mana", 0, 10, 10, null, 0, 0, 0, null, 0, 0, 0, "res-cold", 0, 12, 12, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawedSaphire, 5, 3, "cold-min", 0, 3, 3, "cold-max", 0, 5, 5, "cold-len", 0, 35, 35, "mana", 0, 17, 17, null, 0, 0, 0, null, 0, 0, 0, "res-cold", 0, 16, 16, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.Saphire, 5, 3, "cold-min", 0, 4, 4, "cold-max", 0, 7, 7, "cold-len", 0, 50, 50, "mana", 0, 24, 24, null, 0, 0, 0, null, 0, 0, 0, "res-cold", 0, 22, 22, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawlessSaphire, 5, 3, "cold-min", 0, 6, 6, "cold-max", 0, 10, 10, "cold-len", 0, 60, 60, "mana", 0, 31, 31, null, 0, 0, 0, null, 0, 0, 0, "res-cold", 0, 28, 28, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.PerfectSaphire, 6, 3, "cold-min", 0, 10, 10, "cold-max", 0, 14, 14, "cold-len", 0, 75, 75, "mana", 0, 38, 38, null, 0, 0, 0, null, 0, 0, 0, "res-cold", 0, 40, 40, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.ChippedEmerald, 11, 3, "pois-min", 0, 34, 34, "pois-max", 0, 34, 34, "pois-len", 0, 75, 75, "dex", 0, 3, 3, null, 0, 0, 0, null, 0, 0, 0, "res-pois", 0, 12, 12, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawedEmerald, 11, 3, "pois-min", 0, 51, 51, "pois-max", 0, 51, 51, "pois-len", 0, 100, 100, "dex", 0, 4, 4, null, 0, 0, 0, null, 0, 0, 0, "res-pois", 0, 16, 16, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.Emerald, 11, 3, "pois-min", 0, 82, 82, "pois-max", 0, 82, 82, "pois-len", 0, 125, 125, "dex", 0, 6, 6, null, 0, 0, 0, null, 0, 0, 0, "res-pois", 0, 22, 22, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawlessEmerald, 11, 3, "pois-min", 0, 101, 101, "pois-max", 0, 101, 101, "pois-len", 0, 152, 152, "dex", 0, 8, 8, null, 0, 0, 0, null, 0, 0, 0, "res-pois", 0, 28, 28, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.PerfectEmerald, 12, 3, "pois-min", 0, 143, 143, "pois-max", 0, 143, 143, "pois-len", 0, 179, 179, "dex", 0, 10, 10, null, 0, 0, 0, null, 0, 0, 0, "res-pois", 0, 40, 40, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.ChippedRuby, 8, 3, "fire-min", 0, 3, 3, "fire-max", 0, 4, 4, null, 0, 0, 0, "hp", 0, 10, 10, null, 0, 0, 0, null, 0, 0, 0, "res-fire", 0, 12, 12, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawedRuby, 8, 3, "fire-min", 0, 5, 5, "fire-max", 0, 8, 8, null, 0, 0, 0, "hp", 0, 17, 17, null, 0, 0, 0, null, 0, 0, 0, "res-fire", 0, 16, 16, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.Ruby, 8, 3, "fire-min", 0, 8, 8, "fire-max", 0, 12, 12, null, 0, 0, 0, "hp", 0, 24, 24, null, 0, 0, 0, null, 0, 0, 0, "res-fire", 0, 22, 22, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawlessRuby, 8, 3, "fire-min", 0, 10, 10, "fire-max", 0, 16, 16, null, 0, 0, 0, "hp", 0, 31, 31, null, 0, 0, 0, null, 0, 0, 0, "res-fire", 0, 28, 28, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.PerfectRuby, 9, 3, "fire-min", 0, 15, 15, "fire-max", 0, 20, 20, null, 0, 0, 0, "hp", 0, 38, 38, null, 0, 0, 0, null, 0, 0, 0, "res-fire", 0, 40, 40, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.ChippedDiamond, 1, 3, "dmg-undead", 0, 28, 28, null, -1, -1, -1, null, 0, 0, 0, "att", 0, 20, 20, null, 0, 0, 0, null, 0, 0, 0, "res-all", 0, 6, 6, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawedDiamond, 1, 3, "dmg-undead", 0, 34, 34, null, -1, -1, -1, null, 0, 0, 0, "att", 0, 40, 40, null, 0, 0, 0, null, 0, 0, 0, "res-all", 0, 8, 8, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.Diamond, 1, 3, "dmg-undead", 0, 44, 44, null, -1, -1, -1, null, 0, 0, 0, "att", 0, 60, 60, null, 0, 0, 0, null, 0, 0, 0, "res-all", 0, 11, 11, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawlessDiamond, 1, 3, "dmg-undead", 0, 54, 54, null, -1, -1, -1, null, 0, 0, 0, "att", 0, 80, 80, null, 0, 0, 0, null, 0, 0, 0, "res-all", 0, 14, 14, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.PerfectDiamond, 0, 3, "dmg-undead", 0, 68, 68, null, -1, -1, -1, null, 0, 0, 0, "att", 0, 100, 100, null, 0, 0, 0, null, 0, 0, 0, "res-all", 0, 19, 19, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.ChippedTopaz, 14, 3, "ltng-min", 0, 1, 1, "ltng-max", 0, 8, 8, null, 0, 0, 0, "mag%", 0, 9, 9, null, 0, 0, 0, null, 0, 0, 0, "res-ltng", 0, 12, 12, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawedTopaz, 14, 3, "ltng-min", 0, 1, 1, "ltng-max", 0, 14, 14, null, 0, 0, 0, "mag%", 0, 13, 13, null, 0, 0, 0, null, 0, 0, 0, "res-ltng", 0, 16, 16, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.Topaz, 14, 3, "ltng-min", 0, 1, 1, "ltng-max", 0, 22, 22, null, 0, 0, 0, "mag%", 0, 16, 16, null, 0, 0, 0, null, 0, 0, 0, "res-ltng", 0, 22, 22, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawlessTopaz, 14, 3, "ltng-min", 0, 1, 1, "ltng-max", 0, 30, 30, null, 0, 0, 0, "mag%", 0, 20, 20, null, 0, 0, 0, null, 0, 0, 0, "res-ltng", 0, 28, 28, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.PerfectTopaz, 13, 3, "ltng-min", 0, 1, 1, "ltng-max", 0, 40, 40, null, 0, 0, 0, "mag%", 0, 24, 24, null, 0, 0, 0, null, 0, 0, 0, "res-ltng", 0, 40, 40, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.ChippedSkull, 2, 3, "manasteal", 0, 1, 1, "lifesteal", 0, 2, 2, null, -1, -1, -1, "regen", 0, 2, 2, "regen-mana", 0, 8, 8, null, 0, 0, 0, "thorns", 0, 4, 4, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawedSkull, 2, 3, "manasteal", 0, 2, 2, "lifesteal", 0, 2, 2, null, -1, -1, -1, "regen", 0, 3, 3, "regen-mana", 0, 8, 8, null, 0, 0, 0, "thorns", 0, 8, 8, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.Skull, 2, 3, "manasteal", 0, 2, 2, "lifesteal", 0, 3, 3, null, -1, -1, -1, "regen", 0, 3, 3, "regen-mana", 0, 12, 12, null, 0, 0, 0, "thorns", 0, 12, 12, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.FlawlessSkull, 2, 3, "manasteal", 0, 3, 3, "lifesteal", 0, 3, 3, null, -1, -1, -1, "regen", 0, 4, 4, "regen-mana", 0, 12, 12, null, 0, 0, 0, "thorns", 0, 16, 16, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.PerfectSkull, 3, 3, "manasteal", 0, 3, 3, "lifesteal", 0, 4, 4, null, -1, -1, -1, "regen", 0, 5, 5, "regen-mana", 0, 19, 19, null, 0, 0, 0, "thorns", 0, 20, 20, null, 0, 0, 0, null, 0, 0, 0),
		new BaseGem(ItemCount++, ItemClass.ElRune, 18, 1, "light", -1, 1, 1, "att", -1, 50, 50, null, -1, -1, -1, "light", -1, 1, 1, "ac", -1, 15, 15, null, -1, -1, -1, "light", -1, 1, 1, "ac", -1, 15, 15, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.EldRune, 18, 1, "att-undead", -1, 50, 50, "dmg-undead", -1, 75, 75, null, -1, -1, -1, "stamdrain", -1, 15, 15, null, -1, -1, -1, null, -1, -1, -1, "block", -1, 7, 7, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.TirRune, 18, 1, "mana-kill", -1, 2, 2, null, -1, -1, -1, null, -1, -1, -1, "mana-kill", -1, 2, 2, null, -1, -1, -1, null, -1, -1, -1, "mana-kill", -1, 2, 2, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.NefRune, 18, 1, "knock", -1, 1, 1, null, -1, -1, -1, null, -1, -1, -1, "ac-miss", -1, 30, 30, null, -1, -1, -1, null, -1, -1, -1, "ac-miss", -1, 30, 30, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.EthRune, 18, 1, "reduce-ac", -1, 25, 25, null, -1, -1, -1, null, -1, -1, -1, "regen-mana", -1, 15, 15, null, -1, -1, -1, null, -1, -1, -1, "regen-mana", -1, 15, 15, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.IthRune, 18, 1, "dmg-max", -1, 9, 9, null, -1, -1, -1, null, -1, -1, -1, "dmg-to-mana", -1, 15, 15, null, -1, -1, -1, null, -1, -1, -1, "dmg-to-mana", -1, 15, 15, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.TalRune, 18, 1, "dmg-pois", 125, 154, 154, null, -1, -1, -1, null, -1, -1, -1, "res-pois", -1, 30, 30, null, -1, -1, -1, null, -1, -1, -1, "res-pois", -1, 35, 35, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.RalRune, 18, 1, "dmg-fire", -1, 5, 30, null, -1, -1, -1, null, -1, -1, -1, "res-fire", -1, 30, 30, null, -1, -1, -1, null, -1, -1, -1, "res-fire", -1, 35, 35, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.OrtRune, 18, 1, "dmg-ltng", -1, 1, 50, null, -1, -1, -1, null, -1, -1, -1, "res-ltng", -1, 30, 30, null, -1, -1, -1, null, -1, -1, -1, "res-ltng", -1, 35, 35, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.ThulRune, 18, 1, "dmg-cold", 75, 3, 14, null, -1, -1, -1, null, -1, -1, -1, "res-cold", -1, 30, 30, null, -1, -1, -1, null, -1, -1, -1, "res-cold", -1, 35, 35, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.AmnRune, 18, 1, "lifesteal", -1, 7, 7, null, -1, -1, -1, null, -1, -1, -1, "thorns", -1, 14, 14, null, -1, -1, -1, null, -1, -1, -1, "thorns", -1, 14, 14, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.SolRune, 18, 1, "dmg-min", -1, 9, 9, null, -1, -1, -1, null, -1, -1, -1, "red-dmg", -1, 7, 7, null, -1, -1, -1, null, -1, -1, -1, "red-dmg", -1, 7, 7, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.ShaelRune, 18, 1, "swing2", -1, 20, 20, null, -1, -1, -1, null, -1, -1, -1, "balance2", -1, 20, 20, null, -1, -1, -1, null, -1, -1, -1, "block2", -1, 20, 20, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.DolRune, 18, 1, "howl", -1, 32, 32, null, -1, -1, -1, null, -1, -1, -1, "regen", -1, 7, 7, null, -1, -1, -1, null, -1, -1, -1, "regen", -1, 7, 7, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.HelRune, 18, 1, "ease", -1, -20, -20, null, -1, -1, -1, null, -1, -1, -1, "ease", -1, -15, -15, null, -1, -1, -1, null, -1, -1, -1, "ease", -1, -15, -15, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.IoRune, 18, 1, "vit", -1, 10, 10, null, -1, -1, -1, null, -1, -1, -1, "vit", -1, 10, 10, null, -1, -1, -1, null, -1, -1, -1, "vit", -1, 10, 10, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.LumRune, 18, 1, "enr", -1, 10, 10, null, -1, -1, -1, null, -1, -1, -1, "enr", -1, 10, 10, null, -1, -1, -1, null, -1, -1, -1, "enr", -1, 10, 10, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.KoRune, 18, 1, "dex", -1, 10, 10, null, -1, -1, -1, null, -1, -1, -1, "dex", -1, 10, 10, null, -1, -1, -1, null, -1, -1, -1, "dex", -1, 10, 10, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.FalRune, 18, 1, "str", -1, 10, 10, null, -1, -1, -1, null, -1, -1, -1, "str", -1, 10, 10, null, -1, -1, -1, null, -1, -1, -1, "str", -1, 10, 10, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.LemRune, 18, 1, "gold%", -1, 75, 75, null, -1, -1, -1, null, -1, -1, -1, "gold%", -1, 50, 50, null, -1, -1, -1, null, -1, -1, -1, "gold%", -1, 50, 50, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.PulRune, 18, 1, "att-demon", -1, 100, 100, "dmg-demon", -1, 75, 75, null, -1, -1, -1, "ac%", -1, 30, 30, null, -1, -1, -1, null, -1, -1, -1, "ac%", -1, 30, 30, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.UmRune, 18, 1, "openwounds", -1, 25, 25, null, -1, -1, -1, null, -1, -1, -1, "res-all", -1, 15, 15, null, -1, -1, -1, null, -1, -1, -1, "res-all", -1, 22, 22, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.MalRune, 18, 1, "noheal", -1, 1, 1, null, -1, -1, -1, null, -1, -1, -1, "red-mag", -1, 7, 7, null, -1, -1, -1, null, -1, -1, -1, "red-mag", -1, 7, 7, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.IstRune, 18, 1, "mag%", -1, 30, 30, null, -1, -1, -1, null, -1, -1, -1, "mag%", -1, 25, 25, null, -1, -1, -1, null, -1, -1, -1, "mag%", -1, 25, 25, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.GulRune, 18, 1, "att%", -1, 20, 20, null, -1, -1, -1, null, -1, -1, -1, "res-pois-max", -1, 5, 5, null, -1, -1, -1, null, -1, -1, -1, "res-pois-max", -1, 5, 5, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.VexRune, 18, 1, "manasteal", -1, 7, 7, null, -1, -1, -1, null, -1, -1, -1, "res-fire-max", -1, 5, 5, null, -1, -1, -1, null, -1, -1, -1, "res-fire-max", -1, 5, 5, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.OhmRune, 18, 1, "dmg%", -1, 50, 50, null, -1, -1, -1, null, -1, -1, -1, "res-cold-max", -1, 5, 5, null, -1, -1, -1, null, -1, -1, -1, "res-cold-max", -1, 5, 5, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.LoRune, 18, 1, "deadly", -1, 20, 20, null, -1, -1, -1, null, -1, -1, -1, "res-ltng-max", -1, 5, 5, null, -1, -1, -1, null, -1, -1, -1, "res-ltng-max", -1, 5, 5, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.SurRune, 18, 1, "stupidity", -1, 1, 1, null, -1, -1, -1, null, -1, -1, -1, "mana%", -1, 5, 5, null, -1, -1, -1, null, -1, -1, -1, "mana", -1, 50, 50, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.BerRune, 18, 1, "crush", -1, 20, 20, null, -1, -1, -1, null, -1, -1, -1, "red-dmg%", -1, 8, 8, null, -1, -1, -1, null, -1, -1, -1, "red-dmg%", -1, 8, 8, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.JahRune, 18, 1, "ignore-ac", -1, 1, 1, null, -1, -1, -1, null, -1, -1, -1, "hp%", -1, 5, 5, null, -1, -1, -1, null, -1, -1, -1, "hp", -1, 50, 50, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.ChamRune, 18, 1, "freeze", -1, 3, 3, null, -1, -1, -1, null, -1, -1, -1, "nofreeze", -1, 1, 1, null, -1, -1, -1, null, -1, -1, -1, "nofreeze", -1, 1, 1, null, -1, -1, -1, null, -1, -1, 0),
		new BaseGem(ItemCount++, ItemClass.ZodRune, 18, 1, "indestruct", -1, 1, 1, null, -1, -1, -1, null, -1, -1, -1, "indestruct", -1, 1, 1, null, -1, -1, -1, null, -1, -1, -1, "indestruct", -1, 1, 1, null, -1, -1, -1, null, -1, -1, 0)
	};

	public BaseGem(int Index, ItemClass Type, int Transform, int NumMods, string WeaponMod1Code, int WeaponMod1Param, int WeaponMod1Min, int WeaponMod1Max, string WeaponMod2Code, int WeaponMod2Param, int WeaponMod2Min, int WeaponMod2Max, string WeaponMod3Code, int WeaponMod3Param, int WeaponMod3Min, int WeaponMod3Max, string HelmMod1Code, int HelmMod1Param, int HelmMod1Min, int HelmMod1Max, string HelmMod2Code, int HelmMod2Param, int HelmMod2Min, int HelmMod2Max, string HelmMod3Code, int HelmMod3Param, int HelmMod3Min, int HelmMod3Max, string ShieldMod1Code, int ShieldMod1Param, int ShieldMod1Min, int ShieldMod1Max, string ShieldMod2Code, int ShieldMod2Param, int ShieldMod2Min, int ShieldMod2Max, string ShieldMod3Code, int ShieldMod3Param, int ShieldMod3Min, int ShieldMod3Max)
	{
		this.Type = Type;
		this.Transform = Transform;
		this.NumMods = NumMods;
		if (WeaponMod3Code != null)
		{
			WeaponMods = new ItemProperty[3]
			{
				new ItemProperty(WeaponMod1Code, WeaponMod1Param, WeaponMod1Min, WeaponMod1Max),
				new ItemProperty(WeaponMod2Code, WeaponMod2Param, WeaponMod2Min, WeaponMod2Max),
				new ItemProperty(WeaponMod3Code, WeaponMod3Param, WeaponMod3Min, WeaponMod3Max)
			};
		}
		else if (WeaponMod2Code != null)
		{
			WeaponMods = new ItemProperty[2]
			{
				new ItemProperty(WeaponMod1Code, WeaponMod1Param, WeaponMod1Min, WeaponMod1Max),
				new ItemProperty(WeaponMod2Code, WeaponMod2Param, WeaponMod2Min, WeaponMod2Max)
			};
		}
		else
		{
			WeaponMods = new ItemProperty[1]
			{
				new ItemProperty(WeaponMod1Code, WeaponMod1Param, WeaponMod1Min, WeaponMod1Max)
			};
		}
		if (HelmMod3Code != null)
		{
			HelmMods = new ItemProperty[3]
			{
				new ItemProperty(HelmMod1Code, HelmMod1Param, HelmMod1Min, HelmMod1Max),
				new ItemProperty(HelmMod2Code, HelmMod2Param, HelmMod2Min, HelmMod2Max),
				new ItemProperty(HelmMod3Code, HelmMod3Param, HelmMod3Min, HelmMod3Max)
			};
		}
		else if (HelmMod2Code != null)
		{
			HelmMods = new ItemProperty[2]
			{
				new ItemProperty(HelmMod1Code, HelmMod1Param, HelmMod1Min, HelmMod1Max),
				new ItemProperty(HelmMod2Code, HelmMod2Param, HelmMod2Min, HelmMod2Max)
			};
		}
		else
		{
			HelmMods = new ItemProperty[1]
			{
				new ItemProperty(HelmMod1Code, HelmMod1Param, HelmMod1Min, HelmMod1Max)
			};
		}
		if (ShieldMod3Code != null)
		{
			ShieldMods = new ItemProperty[3]
			{
				new ItemProperty(ShieldMod1Code, ShieldMod1Param, ShieldMod1Min, ShieldMod1Max),
				new ItemProperty(ShieldMod2Code, ShieldMod2Param, ShieldMod2Min, ShieldMod2Max),
				new ItemProperty(ShieldMod3Code, ShieldMod3Param, ShieldMod3Min, ShieldMod3Max)
			};
		}
		else if (ShieldMod2Code != null)
		{
			ShieldMods = new ItemProperty[2]
			{
				new ItemProperty(ShieldMod1Code, ShieldMod1Param, ShieldMod1Min, ShieldMod1Max),
				new ItemProperty(ShieldMod2Code, ShieldMod2Param, ShieldMod2Min, ShieldMod2Max)
			};
		}
		else
		{
			ShieldMods = new ItemProperty[1]
			{
				new ItemProperty(ShieldMod1Code, ShieldMod1Param, ShieldMod1Min, ShieldMod1Max)
			};
		}
	}

	public override string ToString()
	{
		return Type.ToString();
	}

	public static BaseGem Get(int index)
	{
		if (index < 0 || index >= ItemCount)
		{
			throw new ArgumentOutOfRangeException();
		}
		return items[index];
	}

	public static BaseGem Get(ItemClass type)
	{
		for (int i = 0; i < ItemCount; i++)
		{
			if (items[i].Type == type)
			{
				return items[i];
			}
		}
		throw new ArgumentException($"No BaseGem of Type {type} found !");
	}
}
