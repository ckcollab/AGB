using System;

namespace D2Data;

[Flags]
public enum FindPotionFlags
{
	FromBelt = 1,
	FromInventory = 2,
	FromAny = 3,
	FromBeltFirst = 4,
	SmallestFirst = 8
}
