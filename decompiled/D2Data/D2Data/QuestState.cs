using System;

namespace D2Data;

[Flags]
public enum QuestState
{
	Event1 = 1,
	Event2 = 2,
	Event4 = 4,
	Event8 = 8,
	Active = 0x10,
	/// <summary>
	/// Previous quest's main goal is accomplished !?!
	/// </summary>
	Enabled = 0x20,
	Unknown0x40 = 0x40,
	/// <summary>
	/// An NPC wants to give the quest, or quest was given by an NPC.
	/// </summary>
	FromNPC = 0x80
}
