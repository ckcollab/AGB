using System;

namespace D2Data;

[Flags]
public enum PotionType
{
	None = 0,
	Stamina = 1,
	Antidote = 2,
	Thawing = 4,
	MinorMana = 8,
	LightMana = 0x10,
	Mana = 0x20,
	GreaterMana = 0x40,
	SuperMana = 0x80,
	AnyMana = 0xF8,
	MinorHealing = 0x100,
	LightHealing = 0x200,
	Healing = 0x400,
	GreaterHealing = 0x800,
	SuperHealing = 0x1000,
	AnyHealing = 0x1F00,
	Rejuvenation = 0x2000,
	FullRejuvenation = 0x4000,
	AnyRejuvenation = 0x6000
}
