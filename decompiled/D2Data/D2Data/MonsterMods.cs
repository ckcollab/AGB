using System;

namespace D2Data;

[Flags]
public enum MonsterMods
{
	None = 0,
	PhysicalImmune = 1,
	MagicImmune = 2,
	FireImmune = 4,
	LightningImmune = 8,
	ColdImmune = 0x10,
	PoisonImmune = 0x20,
	ExtraStrong = 0x40,
	ExtraFast = 0x80,
	Cursed = 0xFE,
	MagicResistant = 0x200,
	FireEnchanted = 0x400,
	LightningEnchanted = 0x800,
	ColdEnchanted = 0x1000,
	ManaBurn = 0x2000,
	Teleportation = 0x4000,
	SpectralHit = 0x8000,
	StoneSkin = 0x10000,
	MultiShot = 0x20000,
	ConvictionAura = 0x40000,
	MightAura = 0x80000,
	HolyFireAura = 0x100000,
	BlessedAimAura = 0x200000,
	HolyFreezeAura = 0x400000,
	HolyShockAura = 0x800000,
	FanaticismAura = 0x1000000
}
