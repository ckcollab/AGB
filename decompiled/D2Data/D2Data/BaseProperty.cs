using System;
using System.Collections.Generic;

namespace D2Data;

/// <summary>
/// Base info class for item properties (as in Properties.txt)
/// Used by Sets, Uniques and Runewords.
/// </summary>
public class BaseProperty
{
	public readonly int Index;

	public readonly PropertyType Type;

	public readonly string Code;

	public readonly bool Done;

	public readonly ItemPropertyEntry[] Stats;

	public readonly string Desc;

	public readonly string Param;

	public readonly string Min;

	public readonly string Max;

	private static int ItemCount = 0;

	private static BaseProperty[] items = new BaseProperty[268]
	{
		new BaseProperty(ItemCount++, "ac", Done: true, -1, -1, 1, 31, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "ac-miss", Done: true, -1, -1, 1, 32, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "ac-hth", Done: true, -1, -1, 1, 33, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "red-dmg", Done: true, -1, -1, 1, 34, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "red-dmg%", Done: true, -1, -1, 1, 36, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "ac%", Done: true, -1, -1, 2, 16, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "red-mag", Done: true, -1, -1, 1, 35, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "str", Done: true, -1, -1, 1, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dex", Done: true, -1, -1, 1, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "vit", Done: true, -1, -1, 1, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "enr", Done: true, -1, -1, 1, 1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "mana", Done: true, -1, -1, 1, 9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "mana%", Done: true, -1, -1, 1, 77, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "hp", Done: true, -1, -1, 1, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "hp%", Done: true, -1, -1, 1, 76, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "att", Done: true, -1, -1, 1, 19, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "block", Done: true, -1, -1, 1, 20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "cold-min", Done: true, -1, -1, 1, 54, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "cold-max", Done: true, -1, -1, 1, 55, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "cold-len", Done: true, -1, -1, 1, 56, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "fire-min", Done: true, -1, -1, 1, 48, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "fire-max", Done: true, -1, -1, 1, 49, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "ltng-min", Done: true, -1, -1, 1, 50, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "ltng-max", Done: true, -1, -1, 1, 51, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "pois-min", Done: true, -1, -1, 1, 57, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "pois-max", Done: true, -1, -1, 1, 58, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "pois-len", Done: true, -1, -1, 1, 59, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dmg-min", Done: true, -1, -1, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dmg-max", Done: true, -1, -1, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dmg%", Done: true, -1, -1, 7, 25, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dmg-to-mana", Done: true, -1, -1, 1, 114, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "res-fire", Done: true, -1, -1, 1, 39, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "res-fire-max", Done: true, -1, -1, 1, 40, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "res-ltng", Done: true, -1, -1, 1, 41, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "res-ltng-max", Done: true, -1, -1, 1, 42, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "res-cold", Done: true, -1, -1, 1, 43, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "res-cold-max", Done: true, -1, -1, 1, 44, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "res-mag", Done: true, -1, -1, 1, 37, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "res-mag-max", Done: true, -1, -1, 1, 38, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "res-pois", Done: true, -1, -1, 1, 45, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "res-pois-max", Done: true, -1, -1, 1, 46, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "res-all", Done: true, -1, -1, 1, 39, -1, -1, 3, 41, -1, -1, 3, 43, -1, -1, 3, 45, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "res-all-max", Done: true, -1, -1, 1, 40, -1, -1, 3, 42, -1, -1, 3, 44, -1, -1, 3, 46, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "abs-fire%", Done: true, -1, -1, 1, 142, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "abs-fire", Done: true, -1, -1, 1, 143, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "abs-ltng%", Done: true, -1, -1, 1, 144, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "abs-ltng", Done: true, -1, -1, 1, 145, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "abs-mag%", Done: true, -1, -1, 1, 146, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "abs-mag", Done: true, -1, -1, 1, 147, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "abs-cold%", Done: true, -1, -1, 1, 148, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "abs-cold", Done: true, -1, -1, 1, 149, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dur", Done: true, -1, -1, 1, 73, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dur%", Done: true, -1, -1, 13, 75, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "regen", Done: true, -1, -1, 1, 74, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "thorns", Done: true, -1, -1, 1, 78, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "swing1", Done: true, -1, -1, 8, 93, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "swing2", Done: true, -1, -1, 8, 93, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "swing3", Done: true, -1, -1, 8, 93, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "gold%", Done: true, -1, -1, 1, 79, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "mag%", Done: true, -1, -1, 1, 80, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "knock", Done: true, -1, -1, 1, 81, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "regen-stam", Done: true, -1, -1, 1, 28, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "regen-mana", Done: true, -1, -1, 1, 27, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "stam", Done: true, -1, -1, 1, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "time", Done: true, -1, -1, 1, 82, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "manasteal", Done: true, -1, -1, 1, 62, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "lifesteal", Done: true, -1, -1, 1, 60, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "ama", Done: true, -1, 0, 21, 83, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "pal", Done: true, -1, 3, 21, 83, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "nec", Done: true, -1, 2, 21, 83, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "sor", Done: true, -1, 1, 21, 83, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "bar", Done: true, -1, 4, 21, 83, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "herb", Done: true, -1, -1, 1, 88, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "light", Done: true, -1, -1, 1, 89, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "color", Done: true, 1, -1, 1, 90, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "ease", Done: true, -1, -1, 1, 91, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "move1", Done: true, -1, -1, 8, 96, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "move2", Done: true, -1, -1, 8, 96, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "move3", Done: true, -1, -1, 8, 96, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "balance1", Done: true, -1, -1, 8, 99, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "balance2", Done: true, -1, -1, 8, 99, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "balance3", Done: true, -1, -1, 8, 99, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "block1", Done: true, -1, -1, 8, 102, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "block2", Done: true, -1, -1, 8, 102, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "block3", Done: true, -1, -1, 8, 102, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "cast1", Done: true, -1, -1, 8, 105, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "cast2", Done: true, -1, -1, 8, 105, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "cast3", Done: true, -1, -1, 8, 105, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "res-pois-len", Done: true, -1, -1, 1, 110, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dmg", Done: true, -1, -1, 1, 111, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "howl", Done: true, -1, -1, 1, 112, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "stupidity", Done: true, -1, -1, 1, 113, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "ignore-ac", Done: true, -1, -1, 1, 115, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "reduce-ac", Done: true, -1, -1, 1, 116, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "noheal", Done: true, -1, -1, 1, 117, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "half-freeze", Done: true, -1, -1, 1, 118, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "att%", Done: true, -1, -1, 1, 119, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dmg-ac", Done: true, -1, -1, 1, 120, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dmg-demon", Done: true, -1, -1, 1, 121, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dmg-undead", Done: true, -1, -1, 1, 122, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "att-demon", Done: true, -1, -1, 1, 123, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "att-undead", Done: true, -1, -1, 1, 124, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "throw", Done: true, -1, -1, 1, 125, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "fireskill", Done: true, -1, 1, 21, 126, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "allskills", Done: true, -1, -1, 1, 127, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "light-thorns", Done: true, -1, -1, 1, 128, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "freeze", Done: true, -1, -1, 1, 134, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "chance in 128", "length in frames", ""),
		new BaseProperty(ItemCount++, "openwounds", Done: true, -1, -1, 1, 135, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "crush", Done: true, -1, -1, 1, 136, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "kick", Done: true, -1, -1, 1, 137, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "mana-kill", Done: true, -1, -1, 1, 138, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "demon-heal", Done: true, -1, -1, 1, 139, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "bloody", Done: true, -1, -1, 1, 140, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "deadly", Done: true, -1, -1, 1, 141, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "slow", Done: true, -1, -1, 1, 150, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "nofreeze", Done: true, -1, -1, 1, 153, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "stamdrain", Done: true, -1, -1, 1, 154, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "reanimate", Done: true, -1, -1, 24, 155, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "pierce", Done: true, -1, -1, 1, 156, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "magicarrow", Done: true, -1, -1, 1, 157, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "explosivearrow", Done: true, -1, -1, 1, 158, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dru", Done: true, -1, 5, 21, 83, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "All Druid Skills", "", "", ""),
		new BaseProperty(ItemCount++, "ass", Done: true, -1, 6, 21, 83, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "All Assassin Skills", "", "", ""),
		new BaseProperty(ItemCount++, "skill", Done: true, -1, -1, 22, 107, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "skilltab", Done: true, -1, -1, 10, 188, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "aura", Done: true, -1, -1, 22, 151, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "att-skill", Done: true, -1, -1, 11, 195, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Proc Skill on Swing", "Skill", "% Chance", "Level"),
		new BaseProperty(ItemCount++, "hit-skill", Done: true, -1, -1, 11, 198, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Proc Skill on Hit", "Skill", "% Chance", "Level"),
		new BaseProperty(ItemCount++, "gethit-skill", Done: true, -1, -1, 11, 201, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Proc Skill on Get Hit", "Skill", "% Chance", "Level"),
		new BaseProperty(ItemCount++, "gembonus", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Increase chance of finding Gems", "", "", ""),
		new BaseProperty(ItemCount++, "regen-dur", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "fire-fx", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "ltng-fx", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "sock", Done: true, -1, -1, 14, 194, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dmg-fire", Done: true, -1, -1, 15, 48, -1, -1, 16, 49, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Fire Damage", "", "Min", "Max"),
		new BaseProperty(ItemCount++, "dmg-ltng", Done: true, -1, -1, 15, 50, -1, -1, 16, 51, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Lightning Damage", "", "Min", "Max"),
		new BaseProperty(ItemCount++, "dmg-mag", Done: true, -1, -1, 15, 52, -1, -1, 16, 53, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Magic Damge", "", "Min", "Max"),
		new BaseProperty(ItemCount++, "dmg-cold", Done: true, -1, -1, 15, 54, -1, -1, 16, 55, -1, -1, 17, 56, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Cold Damage", "Length (Frames)", "Min", "Max"),
		new BaseProperty(ItemCount++, "dmg-pois", Done: true, -1, -1, 15, 57, -1, -1, 16, 58, -1, -1, 17, 59, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Poison Damage", "Length (Frames)", "Min", "Max"),
		new BaseProperty(ItemCount++, "dmg-throw", Done: true, -1, -1, 15, 159, -1, -1, 16, 160, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Throwing Damage", "", "Min", "Max"),
		new BaseProperty(ItemCount++, "dmg-norm", Done: true, -1, -1, 15, 21, -1, -1, 16, 22, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Normal Damage Modifier", "", "Min", "Max"),
		new BaseProperty(ItemCount++, "ac/lvl", Done: true, -1, -1, 17, 214, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "AC per Player Level", "ac/lvl (8ths)", "", ""),
		new BaseProperty(ItemCount++, "ac%/lvl", Done: true, -1, -1, 17, 215, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "AC% per Player Level", "ac%/lvl (8ths)", "", ""),
		new BaseProperty(ItemCount++, "hp/lvl", Done: true, -1, -1, 17, 216, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "HP per Player Level", "hp/lvl (8ths)", "", ""),
		new BaseProperty(ItemCount++, "mana/lvl", Done: true, -1, -1, 17, 217, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Mana per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "dmg/lvl", Done: true, -1, -1, 17, 218, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Max Damage per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "dmg%/lvl", Done: true, -1, -1, 17, 219, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Max Damage % per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "str/lvl", Done: true, -1, -1, 17, 220, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Strength per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "dex/lvl", Done: true, -1, -1, 17, 221, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Dexterity per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "enr/lvl", Done: true, -1, -1, 17, 222, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Energy per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "vit/lvl", Done: true, -1, -1, 17, 223, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Vitality per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "att/lvl", Done: true, -1, -1, 17, 224, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Attack per Player Level", "att/lvl (1)", "", ""),
		new BaseProperty(ItemCount++, "att%/lvl", Done: true, -1, -1, 17, 225, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Attack% per Player Level", "att%/lvl (8ths)", "", ""),
		new BaseProperty(ItemCount++, "dmg-cold/lvl", Done: true, -1, -1, 17, 226, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Max Cold Damage per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "dmg-fire/lvl", Done: true, -1, -1, 17, 227, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Max Fire Damage per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "dmg-ltng/lvl", Done: true, -1, -1, 17, 228, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Max Lightning Dmg per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "dmg-pois/lvl", Done: true, -1, -1, 17, 229, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Max Poison Dmg per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "res-cold/lvl", Done: true, -1, -1, 17, 230, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Resist Cold% per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "res-fire/lvl", Done: true, -1, -1, 17, 231, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Resist Fire% per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "res-ltng/lvl", Done: true, -1, -1, 17, 232, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Resist Lightning% per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "res-pois/lvl", Done: true, -1, -1, 17, 233, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Resist Poison% per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "abs-cold/lvl", Done: true, -1, -1, 17, 234, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Cold Dmg per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "abs-fire/lvl", Done: true, -1, -1, 17, 235, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Fire Dmg per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "abs-ltng/lvl", Done: true, -1, -1, 17, 236, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Lightning Dmg per Player Lvl", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "abs-pois/lvl", Done: true, -1, -1, 17, 237, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Poison Dmg per Player Lvl", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "thorns/lvl", Done: true, -1, -1, 17, 238, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Damage to Attacker per Player Lvl", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "gold%/lvl", Done: true, -1, -1, 17, 239, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "+% Gold Dropped per Player Lvl", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "mag%/lvl", Done: true, -1, -1, 17, 240, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "+% Magical per Player Lvl", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "regen-stam/lvl", Done: true, -1, -1, 17, 241, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "+% Stamina Regeneration per Player Lvl", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "stam/lvl", Done: true, -1, -1, 17, 242, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Stamina per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "dmg-dem/lvl", Done: true, -1, -1, 17, 243, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Damage to Demons % per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "dmg-und/lvl", Done: true, -1, -1, 17, 244, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Damage to Undead % per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "att-dem/lvl", Done: true, -1, -1, 17, 245, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Attack Demons + per Player Level", "att/lvl (1)", "", ""),
		new BaseProperty(ItemCount++, "att-und/lvl", Done: true, -1, -1, 17, 246, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Attack Undead + per Player Level", "att/lvl (1)", "", ""),
		new BaseProperty(ItemCount++, "crush/lvl", Done: true, -1, -1, 17, 247, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "+% Chance of Crushing Blow per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "wounds/lvl", Done: true, -1, -1, 17, 248, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "+% Chance of Open Wounds per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "kick/lvl", Done: true, -1, -1, 17, 249, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Kick Damage per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "deadly/lvl", Done: true, -1, -1, 17, 250, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "+% Chance of Deadly Strike per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "gems%/lvl", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "+% Chance of finding Gems per Player Level", "(8ths)", "", ""),
		new BaseProperty(ItemCount++, "rep-dur", Done: true, -1, -1, 17, 252, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "regenerates durability", "speed (see note)", "", ""),
		new BaseProperty(ItemCount++, "rep-quant", Done: true, -1, -1, 17, 253, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "regenerates quantity", "speed (see note)", "", ""),
		new BaseProperty(ItemCount++, "stack", Done: true, -1, -1, 1, 254, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Increased stack size", "", "min count", "max count"),
		new BaseProperty(ItemCount++, "item%", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "+% Chance of finding item", "item type", "min chance", "max chance"),
		new BaseProperty(ItemCount++, "dmg-slash", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Slashing Damage", "", "min", "max"),
		new BaseProperty(ItemCount++, "dmg-slash%", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Slashing Damage %", "", "min %", "max %"),
		new BaseProperty(ItemCount++, "dmg-crush", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Crush Damage", "", "min", "max"),
		new BaseProperty(ItemCount++, "dmg-crush%", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Crush Damage %", "", "min %", "max %"),
		new BaseProperty(ItemCount++, "dmg-thrust", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Thrust Damage", "", "min", "max"),
		new BaseProperty(ItemCount++, "dmg-thrust%", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Thrust Damage %", "", "min %", "max %"),
		new BaseProperty(ItemCount++, "abs-slash", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Slashing Damage", "", "min amt", "max amt"),
		new BaseProperty(ItemCount++, "abs-crush", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Crushing Damage", "", "min amt", "max amt"),
		new BaseProperty(ItemCount++, "abs-thrust", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Thrusting Damage", "", "min amt", "max amt"),
		new BaseProperty(ItemCount++, "abs-slash%", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Slashing Damage %", "", "min %", "max %"),
		new BaseProperty(ItemCount++, "abs-crush%", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Crushing Damage %", "", "min %", "max %"),
		new BaseProperty(ItemCount++, "abs-thrust%", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Thrusting Damage %", "", "min %", "max %"),
		new BaseProperty(ItemCount++, "ac/time", Done: true, -1, -1, 18, 268, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "AC / time increment (0=day, 1=dusk, 2=night, 3=dawn)", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "ac%/time", Done: true, -1, -1, 18, 269, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "AC% / time increment (8 periods)", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "hp/time", Done: true, -1, -1, 18, 270, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "HP / time increment", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "mana/time", Done: true, -1, -1, 18, 271, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Mana / time increment", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "dmg/time", Done: true, -1, -1, 18, 272, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Max Damage / time increment", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "dmg%/time", Done: true, -1, -1, 18, 273, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Max Damage % / time increment", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "str/time", Done: true, -1, -1, 18, 274, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Strength / time increment", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "dex/time", Done: true, -1, -1, 18, 275, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Dexterity / time increment", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "enr/time", Done: true, -1, -1, 18, 276, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Energy / time increment", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "vit/time", Done: true, -1, -1, 18, 277, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Vitality / time increment", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "att/time", Done: true, -1, -1, 18, 278, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "To hit / time increment", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "att%/time", Done: true, -1, -1, 18, 279, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "To Hit % / time increment", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "dmg-cold/time", Done: true, -1, -1, 18, 280, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Cold Damage Max / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "dmg-fire/time", Done: true, -1, -1, 18, 281, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Fire Damage Max / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "dmg-ltng/time", Done: true, -1, -1, 18, 282, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Lightning Damage Max / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "dmg-pois/time", Done: true, -1, -1, 18, 283, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Poison Damage Max / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "res-cold/time", Done: true, -1, -1, 18, 284, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Resist Cold / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "res-fire/time", Done: true, -1, -1, 18, 285, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Resist Fire / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "res-ltng/time", Done: true, -1, -1, 18, 286, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Resist Lightning / time inc", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "res-pois/time", Done: true, -1, -1, 18, 287, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Resist Poison / time inc", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "abs-cold/time", Done: true, -1, -1, 18, 288, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Cold / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "abs-fire/time", Done: true, -1, -1, 18, 289, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Fire / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "abs-ltng/time", Done: true, -1, -1, 18, 290, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Lightning / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "abs-pois/time", Done: true, -1, -1, 18, 291, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Absorb Poison / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "gold%/time", Done: true, -1, -1, 18, 292, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Find Gold Amt % / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "mag%/time", Done: true, -1, -1, 18, 293, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Find Magic % / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "regen-stam/time", Done: true, -1, -1, 18, 294, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "% / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "stam/time", Done: true, -1, -1, 18, 295, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Stamina / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "dmg-dem/time", Done: true, -1, -1, 18, 296, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Damage to Demons % / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "dmg-und/time", Done: true, -1, -1, 18, 297, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Damage to Undead % / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "att-dem/time", Done: true, -1, -1, 18, 298, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "To Hit Demons % / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "att-und/time", Done: true, -1, -1, 18, 299, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "To Hit Undead % / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "crush/time", Done: true, -1, -1, 18, 300, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "% chance of Crushing Blow / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "wounds/time", Done: true, -1, -1, 18, 301, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "+% chance of Open Wounds / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "kick/time", Done: true, -1, -1, 18, 302, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Kick Damage / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "deadly/time", Done: true, -1, -1, 18, 303, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "+% chance of Deadly Strike / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "gems%/time", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "+% chance of finding Gems / time inc.", "center period", "min", "max"),
		new BaseProperty(ItemCount++, "pierce-fire", Done: true, -1, -1, 1, 333, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Negates % of Enemy Cold Resistance", "", "min", "max"),
		new BaseProperty(ItemCount++, "pierce-ltng", Done: true, -1, -1, 1, 334, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Negates % of Enemy Fire Resistance", "", "min", "max"),
		new BaseProperty(ItemCount++, "pierce-cold", Done: true, -1, -1, 1, 335, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Negates % of Enemy Lightning Resistance", "", "min", "max"),
		new BaseProperty(ItemCount++, "pierce-pois", Done: true, -1, -1, 1, 336, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Negates % of Enemy Poison Resistance", "", "min", "max"),
		new BaseProperty(ItemCount++, "dmg-mon", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Damage vs. specific Monster Type", "monster", "min", "max"),
		new BaseProperty(ItemCount++, "dmg%-mon", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Damage % vs. specific Monster Type", "monster", "min", "max"),
		new BaseProperty(ItemCount++, "att-mon", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "To Hit vs. specific Monster Type", "monster", "min", "max"),
		new BaseProperty(ItemCount++, "att%-mon", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "To Hit % vs. specific Monster Type", "monster", "min", "max"),
		new BaseProperty(ItemCount++, "ac-mon", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "AC vs. specific Monster Type", "monster", "min", "max"),
		new BaseProperty(ItemCount++, "ac%-mon", Done: false, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "AC% vs. specific Monster Type", "monster", "min", "max"),
		new BaseProperty(ItemCount++, "indestruct", Done: true, -1, -1, 20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Indestructible", "", "", ""),
		new BaseProperty(ItemCount++, "charged", Done: true, -1, -1, 19, 204, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Charged Skill", "Skill", "charges", "level"),
		new BaseProperty(ItemCount++, "extra-fire", Done: true, -1, -1, 1, 329, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "extra-ltng", Done: true, -1, -1, 1, 330, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "extra-cold", Done: true, -1, -1, 1, 331, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "extra-pois", Done: true, -1, -1, 1, 332, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dmg-elem", Done: true, -1, -1, 15, 48, -1, -1, 16, 49, -1, -1, 15, 50, -1, -1, 16, 51, -1, -1, 15, 54, -1, -1, 16, 55, -1, -1, 17, 56, "", "", "", ""),
		new BaseProperty(ItemCount++, "dmg-elem-min", Done: true, -1, -1, 1, 48, -1, -1, 3, 50, -1, -1, 3, 54, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "dmg-elem-max", Done: true, -1, -1, 1, 49, -1, -1, 3, 51, -1, -1, 3, 55, -1, -1, 17, 56, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "all-stats", Done: true, -1, -1, 1, 0, -1, -1, 3, 1, -1, -1, 3, 2, -1, -1, 3, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "addxp", Done: true, -1, -1, 1, 85, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "additional xp gain", "", "", ""),
		new BaseProperty(ItemCount++, "heal-kill", Done: true, -1, -1, 1, 86, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "life gained after each kill", "", "", ""),
		new BaseProperty(ItemCount++, "cheap", Done: true, -1, -1, 1, 87, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "reduces vendor prices", "", "min", "max"),
		new BaseProperty(ItemCount++, "rip", Done: true, -1, -1, 1, 108, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "slain monsters' corpses can't be used", "", "1", "1"),
		new BaseProperty(ItemCount++, "att-mon%", Done: true, -1, -1, 24, 179, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "attack% vs. monster type", "mon type", "min", "max"),
		new BaseProperty(ItemCount++, "dmg-mon%", Done: true, -1, -1, 24, 180, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "damage% vs. monster type", "mon type", "min", "max"),
		new BaseProperty(ItemCount++, "kill-skill", Done: true, -1, -1, 11, 196, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Proc Skill on killing something", "Skill", "% Chance", "Level"),
		new BaseProperty(ItemCount++, "death-skill", Done: true, -1, -1, 11, 197, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Proc Skill on getting killed", "Skill", "% Chance", "Level"),
		new BaseProperty(ItemCount++, "levelup-skill", Done: true, -1, -1, 11, 199, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Proc Skill on level up", "Skill", "% Chance", "Level"),
		new BaseProperty(ItemCount++, "skill-rand", Done: true, -1, -1, 12, 107, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "Bonus to random skill", "Level", "min Skill #", "max Skill #"),
		new BaseProperty(ItemCount++, "fade", Done: true, -1, -1, 17, 181, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "only use on monprop", "fade type", "", ""),
		new BaseProperty(ItemCount++, "levelreq", Done: true, -1, -1, 1, 92, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "ethereal", Done: true, -1, -1, 23, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", ""),
		new BaseProperty(ItemCount++, "oskill", Done: true, -1, -1, 22, 97, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "Skill", "min level", "max level"),
		new BaseProperty(ItemCount++, "state", Done: true, -1, -1, 24, 98, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "state", "1", "1"),
		new BaseProperty(ItemCount++, "randclassskill", Done: true, -1, 3, 36, 83, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, "", "", "", "")
	};

	public BaseProperty(int Index, string Code, bool Done, int Set1, int Val1, int Func1, int Stat1, int Set2, int Val2, int Func2, int Stat2, int Set3, int Val3, int Func3, int Stat3, int Set4, int Val4, int Func4, int Stat4, int Set5, int Val5, int Func5, int Stat5, int Set6, int Val6, int Func6, int Stat6, int Set7, int Val7, int Func7, int Stat7, string Desc, string Param, string Min, string Max)
	{
		this.Index = Index;
		Type = (PropertyType)Index;
		this.Code = Code;
		this.Done = Done;
		List<ItemPropertyEntry> list = new List<ItemPropertyEntry>();
		if (Stat1 != -1)
		{
			list.Add(new ItemPropertyEntry(Set1, Val1, Func1, Stat1));
		}
		if (Stat2 != -1)
		{
			list.Add(new ItemPropertyEntry(Set2, Val2, Func2, Stat2));
		}
		if (Stat3 != -1)
		{
			list.Add(new ItemPropertyEntry(Set3, Val3, Func3, Stat3));
		}
		if (Stat4 != -1)
		{
			list.Add(new ItemPropertyEntry(Set4, Val4, Func4, Stat4));
		}
		if (Stat5 != -1)
		{
			list.Add(new ItemPropertyEntry(Set5, Val5, Func5, Stat5));
		}
		if (Stat6 != -1)
		{
			list.Add(new ItemPropertyEntry(Set6, Val6, Func6, Stat6));
		}
		if (Stat7 != -1)
		{
			list.Add(new ItemPropertyEntry(Set7, Val7, Func7, Stat7));
		}
		Stats = list.ToArray();
		this.Desc = Desc;
		this.Param = Param;
		this.Min = Min;
		this.Max = Max;
	}

	public override string ToString()
	{
		return Code;
	}

	public static BaseProperty Get(int index)
	{
		if (index < 0 || index >= ItemCount)
		{
			throw new ArgumentOutOfRangeException();
		}
		return items[index];
	}

	public static BaseProperty GetByCode(string code)
	{
		for (int i = 0; i < ItemCount; i++)
		{
			if (items[i].Code == code)
			{
				return items[i];
			}
		}
		throw new ArgumentException($"No Property with code {code} found !");
	}
}
