namespace D2Data;

/// <summary>
/// All Bnet titles are 0x01 - 0x03. To match this list, add 4 if hardcore, 0x20 if expansion and 0x100 if female...
/// Some expansion title are gender neutral; if title ends with "F", remove it to get proper title...
/// </summary>
public enum CharacterTitle
{
	Nooblar = 0,
	Sir = 1,
	Lord = 2,
	Baron = 3,
	Nooblette = 256,
	Dame = 257,
	Lady = 258,
	Baroness = 259,
	CourageousNooblar = 4,
	Count = 5,
	Duke = 6,
	King = 7,
	CourageousNooblette = 260,
	Countess = 261,
	Duchess = 262,
	Queen = 263,
	DoublePlusNooblar = 32,
	Slayer = 33,
	Champion = 34,
	Patriarch = 35,
	DoublePlusNooblette = 288,
	SlayerF = 289,
	ChampionF = 290,
	Matriarch = 291,
	NooblarWhoLikesChicken = 36,
	Destroyer = 37,
	Conquerer = 38,
	Guardian = 39,
	NoobletteWhoLikesChicken = 292,
	DestroyerF = 293,
	ConquererF = 293,
	GuardianF = 293,
	None = 65535
}
