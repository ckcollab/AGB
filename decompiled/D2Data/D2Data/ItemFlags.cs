using System;

namespace D2Data;

[Flags]
public enum ItemFlags : uint
{
	None = 0u,
	Equipped = 1u,
	InSocket = 8u,
	/// <summary>
	/// Not undentified, really... also set for items that cannot be identified.
	/// </summary>
	Identified = 0x10u,
	/// <summary>
	/// Has to do with aura / state change !?
	/// </summary>
	x20 = 0x20u,
	SwitchedIn = 0x40u,
	SwitchedOut = 0x80u,
	Broken = 0x100u,
	/// <summary>
	/// Sometimes on Mana, Healing and Rejuvenation potions and runes... use is unknown.
	/// </summary>
	Duplicate = 0x400u,
	Socketed = 0x800u,
	/// <summary>
	/// Set on items equipped by Valkyrie...
	/// </summary>
	OnPet = 0x1000u,
	/// <summary>
	/// Set on AddToShop (but not for items I sold), on items equipped by Valkyrie.
	/// Also set on AddToGround and sometimes(?) on quest items... Special Interact ? Is New ?
	/// </summary>
	x2000 = 0x2000u,
	NotInSocket = 0x4000u,
	/// <summary>
	/// Is a player's ear. Ear packets have a different structure...
	/// </summary>
	Ear = 0x10000u,
	/// <summary>
	/// Item a character started with (meaning the item worthless to resell.)
	/// </summary>
	StartItem = 0x20000u,
	/// <summary>
	/// Item that doesn't have an ILevel or stats.
	/// </summary>
	Compact = 0x200000u,
	Ethereal = 0x400000u,
	/// <summary>
	/// Meaning is unknown...
	/// </summary>
	Any = 0x800000u,
	Personalized = 0x1000000u,
	/// <summary>
	/// Item a town folk is offering for gambling (same purpose as Compact: no ILevel + extra info.)
	/// </summary>
	Gamble = 0x2000000u,
	Runeword = 0x4000000u,
	/// <summary>
	/// Induce State Change !?
	/// </summary>
	x8000000 = 0x8000000u
}
