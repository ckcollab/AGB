using System;

namespace D2Data;

[Flags]
public enum PlayerInformationActionType
{
	Neutral = 0,
	Party = 2,
	Friendly = 4,
	UnFriendly = 8,
	Remove = 1,
	None = 0x80
}
