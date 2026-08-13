using System;

namespace D2Data;

[Flags]
public enum CharacterFlags
{
	/// <summary>
	/// None would normally mean a male softcore character on open non-ladder classic who never died...
	/// </summary>
	None = 0,
	/// <summary>
	/// Character has never joined a game
	/// </summary>
	Noob = 1,
	UNKNOWNx2 = 2,
	Hardcore = 4,
	Died = 8,
	UNKNOWNx10 = 0x10,
	Expansion = 0x20,
	Ladder = 0x40,
	Realm = 0x80,
	Female = 0x100
}
