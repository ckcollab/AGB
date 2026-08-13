using System;

namespace D2Data;

[Flags]
public enum QuestStanding
{
	Complete = 1,
	RewardPending = 2,
	Started = 4,
	LeftTownWhileStarted = 8,
	EnteredFirstArea = 0x10,
	Event0x20 = 0x20,
	Event0x40 = 0x40,
	Event0x80 = 0x80
}
