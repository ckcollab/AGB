using System;

namespace D2Data;

[Flags]
public enum DamageTypes
{
	None = 0,
	Fire = 1,
	Lightning = 2,
	Cold = 4,
	Physical = 8,
	Magic = 0x10,
	Poison = 0x20,
	Undead = 0x40
}
