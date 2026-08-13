using System;

namespace D2Data;

[Flags]
public enum PlayerRelationshipType
{
	None = 0,
	Loot = 1,
	Mute = 2,
	Squelch = 4,
	Hostile = 8
}
