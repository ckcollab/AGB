using System;

namespace D2Data;

[Flags]
public enum WaypointsAvailiable : ulong
{
	None = 0uL,
	RogueEncampment = 1uL,
	ColdPlains = 2uL,
	StonyField = 4uL,
	DarkWood = 8uL,
	BlackMarsh = 0x10uL,
	OuterCloister = 0x20uL,
	JailLevel1 = 0x40uL,
	InnerCloister = 0x80uL,
	CatacombsLevel2 = 0x100uL,
	LutGholein = 0x200uL,
	LutGholeinSewersLevel2 = 0x400uL,
	DryHills = 0x800uL,
	HallsOfTheDeadLevel2 = 0x1000uL,
	FarOasis = 0x2000uL,
	LostCity = 0x4000uL,
	PalaceCellarLevel1 = 0x8000uL,
	ArcaneSanctuary = 0x10000uL,
	CanyonOfTheMagi = 0x20000uL,
	KurastDocks = 0x40000uL,
	SpiderForest = 0x80000uL,
	GreatMarsh = 0x100000uL,
	FlayerJungle = 0x200000uL,
	LowerKurast = 0x400000uL,
	KurastBazaar = 0x800000uL,
	UpperKurast = 0x1000000uL,
	Travincial = 0x2000000uL,
	DuranceOfHateLevel2 = 0x4000000uL,
	ThePandemoniumFortress = 0x8000000uL,
	CityOfTheDamned = 0x10000000uL,
	RiverOfFlame = 0x20000000uL,
	Harrogath = 0x40000000uL,
	FrigidHighlands = 0x80000000uL,
	ArreatPlateau = 0x100000000uL,
	CrystallinePassage = 0x200000000uL,
	GlacialTrail = 0x400000000uL,
	HallsOfPain = 0x800000000uL,
	FrozenTundra = 0x1000000000uL,
	TheAncientsWay = 0x2000000000uL,
	WorldstoneKeepLevel2 = 0x4000000000uL,
	HaveList = 0x8000000000uL
}
