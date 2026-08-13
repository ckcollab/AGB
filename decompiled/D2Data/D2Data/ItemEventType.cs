using System;

namespace D2Data;

[Flags]
public enum ItemEventType : uint
{
	None = 0u,
	DoMeleeAttack = 1u,
	DoMissileAttack = 2u,
	DoMeleeDamage = 4u,
	DoMissileDamage = 8u,
	Kill = 0x10u,
	DamagedInMelee = 0x20u,
	DamagedByMissile = 0x40u,
	Death = 0x80u,
	LevelUp = 0x100u
}
