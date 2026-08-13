using System;

namespace AGB.Mapping;

[Flags]
public enum Collision
{
	Bit_04 = 0x10,
	Bit_06 = 0x40,
	Bit_07 = 0x80,
	BIT_MASK = 0xFF,
	Default = 0xB,
	Jump = 4,
	Light = 0x20,
	LineOfSight = 2,
	None = 0,
	PlayerWalk = 8,
	Walk = 1
}
