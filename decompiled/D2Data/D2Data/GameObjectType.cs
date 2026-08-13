using System;

namespace D2Data;

[Flags]
public enum GameObjectType
{
	Dummy = 0,
	RefillingShrine = 1,
	HealthShrine = 2,
	ManaShrine = 3,
	HealthExchangeShrine = 4,
	ManaExchangeShrine = 5,
	ArmorShrine = 6,
	CombatShrine = 7,
	ResistFireShrine = 8,
	ResistColdShrine = 9,
	ResistLightningShrine = 0xA,
	ResistPoisonShrine = 0xB,
	SkillShrine = 0xC,
	ManaRechargeShrine = 0xD,
	StaminaShrine = 0xE,
	ExperienceShrine = 0xF,
	ShrineOfEnirhs = 0x10,
	PortalShrine = 0x11,
	GemShrine = 0x12,
	StormShrine = 0x13,
	WarpingShrine = 0x14,
	ExplodingShrine = 0x15,
	PoisonShrine = 0x16,
	Waypoint = 0x1E,
	Portal = 0x1F,
	Shrine = 0x80
}
