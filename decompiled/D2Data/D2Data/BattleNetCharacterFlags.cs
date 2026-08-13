using System;

namespace D2Data;

[Flags]
public enum BattleNetCharacterFlags : uint
{
	BlizzardRepresentative = 1u,
	ChannelOperator = 2u,
	Speaker = 4u,
	BattleNetAdministrator = 8u,
	NoUDPSupport = 0x10u,
	Squelched = 0x20u,
	SpecialGuest = 0x40u,
	PGLOfficial = 0x400u,
	WCGOfficial = 0x1000u,
	KBKSingles = 0x2000u,
	KBKPlayer = 0x8000u,
	KBKBeginner = 0x10000u,
	WhiteKBK = 0x20000u,
	GameRoom = 0x40000u,
	GFOfficial = 0x100000u,
	GFPlayer = 0x200000u,
	PGLPlayer = 0x2000000u
}
