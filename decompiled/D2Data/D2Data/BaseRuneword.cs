using System;

namespace D2Data;

/// <summary>
/// Base info class for Runewords
/// </summary>
public class BaseRuneword
{
	public readonly int Index;

	public readonly RunewordType Id;

	public readonly string Name;

	public readonly bool Complete;

	public readonly bool Server;

	public readonly BaseItemType IType1;

	public readonly BaseItemType IType2;

	public readonly BaseItemType IType3;

	public readonly BaseItemType IType4;

	public readonly BaseItemType IType5;

	public readonly BaseItemType IType6;

	public readonly BaseItemType EType1;

	public readonly BaseItemType EType2;

	public readonly BaseItemType EType3;

	public readonly string Runes;

	public readonly string Rune1;

	public readonly string Rune2;

	public readonly string Rune3;

	public readonly string Rune4;

	public readonly string Rune5;

	public readonly string Rune6;

	public readonly BaseProperty Property1;

	public readonly int Param1;

	public readonly int Min1;

	public readonly int Max1;

	public readonly BaseProperty Property2;

	public readonly int Param2;

	public readonly int Min2;

	public readonly int Max2;

	public readonly BaseProperty Property3;

	public readonly int Param3;

	public readonly int Min3;

	public readonly int Max3;

	public readonly BaseProperty Property4;

	public readonly int Param4;

	public readonly int Min4;

	public readonly int Max4;

	public readonly BaseProperty Property5;

	public readonly int Param5;

	public readonly int Min5;

	public readonly int Max5;

	public readonly BaseProperty Property6;

	public readonly int Param6;

	public readonly int Min6;

	public readonly int Max6;

	public readonly BaseProperty Property7;

	public readonly int Param7;

	public readonly int Min7;

	public readonly int Max7;

	private static int ItemCount = 0;

	private static BaseRuneword[] items = new BaseRuneword[169]
	{
		new BaseRuneword(++ItemCount, "Ancient's Pledge", Complete: true, Server: false, "shld", "", "", "", "", "", "", "", "", "", "r08", "r09", "r07", "", "", "", "res-cold", -1, 30, 30, "res-all", -1, 13, 13, "ac%", -1, 50, 50, "dmg-to-mana", -1, 10, 10, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Armageddon", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Authority", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Beast", Complete: true, Server: false, "axe", "scep", "hamm", "", "", "", "", "", "", "", "r30", "r03", "r22", "r23", "r17", "", "swing2", -1, 40, 40, "aura", 122, 9, 9, "dmg%", -1, 240, 270, "str", -1, 25, 40, "charged", 247, 5, 13, "oskill", 228, 3, 3, "oskill", 224, 3, 3),
		new BaseRuneword(++ItemCount, "Beauty", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Black", Complete: true, Server: false, "club", "hamm", "mace", "", "", "", "", "", "", "", "r10", "r16", "r04", "", "", "", "crush", -1, 40, 40, "dmg%", -1, 120, 120, "swing2", -1, 15, 15, "red-mag", -1, 2, 2, "att", -1, 200, 200, "charged", 74, 12, 4, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Blood", Complete: false, Server: false, "helm", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Bone", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "SolUmUm", "r12", "r22", "r22", "", "", "", "hit-skill", 84, 15, 10, "gethit-skill", 68, 15, 10, "nec", -1, 2, 2, "mana", -1, 100, 150, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Bramble", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "RalOhmSurEth", "r08", "r27", "r29", "r05", "", "", "balance2", -1, 50, 50, "ac", -1, 300, 300, "aura", 103, 15, 21, "heal-kill", -1, 13, 13, "extra-pois", -1, 25, 50, "res-pois", -1, 100, 100, "charged", 246, 33, 13),
		new BaseRuneword(++ItemCount, "Brand", Complete: true, Server: false, "miss", "", "", "", "", "", "", "", "", "JahLoMalGul", "r31", "r28", "r23", "r25", "", "", "dmg%", -1, 260, 340, "dmg-demon", -1, 280, 330, "hit-skill", 84, 100, 18, "gethit-skill", 66, 35, 14, "knock", -1, 1, 1, "noheal", -1, 1, 1, "explosivearrow", -1, 15, 15),
		new BaseRuneword(++ItemCount, "Breath of the Dying", Complete: true, Server: false, "weap", "", "", "", "", "", "", "", "", "VexHelElEldZodEth", "r26", "r15", "r01", "r02", "r33", "r05", "swing2", -1, 60, 60, "dmg-undead", -1, 125, 125, "lifesteal", -1, 12, 15, "noheal", -1, 1, 1, "kill-skill", 92, 50, 20, "dmg%", -1, 350, 400, "all-stats", -1, 30, 30),
		new BaseRuneword(++ItemCount, "Broken Promise", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Call to Arms", Complete: true, Server: false, "weap", "", "", "", "", "", "", "", "", "AmnRalMalIstOhm", "r11", "r08", "r23", "r24", "r27", "", "swing2", -1, 40, 40, "dmg%", -1, 200, 240, "allskills", -1, 1, 1, "oskill", 155, 2, 6, "oskill", 149, 1, 6, "oskill", 146, 1, 4, "regen", -1, 12, 12),
		new BaseRuneword(++ItemCount, "Chains of Honor", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "DolUmBerIst", "r14", "r22", "r30", "r24", "", "", "res-all", -1, 50, 50, "ac%", -1, 70, 70, "dmg-demon", -1, 200, 200, "dmg-undead", -1, 100, 100, "lifesteal", -1, 8, 8, "allskills", -1, 2, 2, "str", -1, 20, 20),
		new BaseRuneword(++ItemCount, "Chance", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Chaos", Complete: true, Server: false, "h2h", "", "", "", "", "", "", "", "", "FalOhmUm", "r19", "r27", "r22", "", "", "", "demon-heal", -1, 15, 15, "dmg%", -1, 240, 290, "dmg-mag", -1, 216, 471, "oskill", 151, 1, 1, "swing2", -1, 35, 35, "hit-skill", 64, 9, 11, "hit-skill", 38, 11, 9),
		new BaseRuneword(++ItemCount, "Crescent Moon", Complete: true, Server: false, "axe", "swor", "pole", "", "", "", "", "", "", "ShaelUmTir", "r13", "r22", "r03", "", "", "", "pierce-ltng", -1, 35, 35, "ignore-ac", -1, 1, 1, "dmg%", -1, 180, 220, "abs-mag", -1, 9, 11, "charged", 227, 30, 18, "hit-skill", 42, 7, 13, "hit-skill", 53, 10, 17),
		new BaseRuneword(++ItemCount, "Darkness", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Daylight", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Death", Complete: true, Server: false, "swor", "axe", "", "", "", "", "", "", "", "HelElVexOrtGul", "r15", "r01", "r26", "r09", "r25", "", "dmg%", -1, 300, 385, "deadly/lvl", 4, -1, -1, "charged", 85, 15, 22, "att-skill", 55, 25, 18, "death-skill", 53, 100, 44, "crush", -1, 50, 50, "indestruct", -1, 1, 1),
		new BaseRuneword(++ItemCount, "Deception", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Delirium", Complete: true, Server: false, "helm", "", "", "", "", "", "", "", "", "LemIstIo", "r20", "r24", "r16", "", "", "", "hit-skill", 81, 11, 18, "charged", 86, 60, 17, "gethit-skill", 77, 14, 13, "ac", -1, 261, 261, "gethit-skill", 273, 6, 14, "gethit-skill", 350, 1, 50, "allskills", -1, 2, 2),
		new BaseRuneword(++ItemCount, "Desire", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Despair", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Destruction", Complete: true, Server: false, "pole", "swor", "", "", "", "", "", "", "", "VexLoBerJahKo", "r26", "r28", "r30", "r31", "r18", "", "dmg%", -1, 350, 350, "dmg-mag", -1, 100, 180, "hit-skill", 229, 5, 23, "death-skill", 56, 100, 45, "att-skill", 48, 15, 22, "hit-skill", 244, 23, 12, "noheal", -1, 1, 1),
		new BaseRuneword(++ItemCount, "Doom", Complete: true, Server: false, "axe", "pole", "hamm", "", "", "", "", "", "", "HelOhmUmLoCham", "r15", "r27", "r22", "r28", "r32", "", "dmg%", -1, 280, 320, "aura", 114, 12, 12, "swing2", -1, 45, 45, "noheal", -1, 1, 1, "pierce-cold", -1, 40, 60, "allskills", -1, 2, 2, "hit-skill", 244, 5, 18),
		new BaseRuneword(++ItemCount, "Dragon", Complete: true, Server: false, "tors", "shld", "", "", "", "", "", "", "", "SurLoSol", "r29", "r28", "r12", "", "", "", "ac", -1, 360, 360, "ac-miss", -1, 230, 230, "str/lvl", 3, -1, -1, "hit-skill", 62, 12, 15, "gethit-skill", 278, 20, 18, "aura", 102, 14, 14, "all-stats", -1, 3, 5),
		new BaseRuneword(++ItemCount, "Dread", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Dream", Complete: true, Server: false, "helm", "shld", "", "", "", "", "", "", "", "IoJahPul", "r16", "r31", "r21", "", "", "", "ac", -1, 150, 220, "gethit-skill", 81, 10, 15, "mana/lvl", 5, -1, -1, "res-all", -1, 5, 20, "balance3", -1, 20, 30, "aura", 118, 15, 15, "mag%", -1, 12, 25),
		new BaseRuneword(++ItemCount, "Duress", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "ShaelUmThul", "r13", "r22", "r10", "", "", "", "dmg-cold", 50, 37, 133, "dmg%", -1, 10, 20, "ac%", -1, 150, 200, "balance2", -1, 20, 20, "openwounds", -1, 33, 33, "crush", -1, 15, 15, "stamdrain", -1, -20, -20),
		new BaseRuneword(++ItemCount, "Edge", Complete: true, Server: false, "miss", "", "", "", "", "", "", "", "", "TirTalAmn", "r03", "r07", "r11", "", "", "", "dmg-demon", -1, 320, 380, "dmg-undead", -1, 280, 280, "swing2", -1, 35, 35, "noheal", -1, 1, 1, "aura", 103, 15, 15, "all-stats", -1, 5, 10, "cheap", -1, 15, 15),
		new BaseRuneword(++ItemCount, "Elation", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Enigma", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "JahIthBer", "r31", "r06", "r30", "", "", "", "ac", -1, 750, 775, "heal-kill", -1, 14, 14, "move2", -1, 45, 45, "str/lvl", 6, -1, -1, "allskills", -1, 2, 2, "mag%/lvl", 8, -1, -1, "oskill", 54, 1, 1),
		new BaseRuneword(++ItemCount, "Enlightenment", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "PulRalSur", "r21", "r08", "r12", "", "", "", "hit-skill", 47, 5, 15, "gethit-skill", 46, 5, 15, "sor", -1, 2, 2, "oskill", 37, 1, 1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Envy", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Eternity", Complete: true, Server: false, "mele", "", "", "", "", "", "", "", "", "AmnBerIstSolSur", "r11", "r30", "r24", "r12", "r29", "", "dmg%", -1, 260, 310, "indestruct", -1, 1, 1, "slow", -1, 33, 33, "charged", 95, 88, 8, "regen", -1, 16, 16, "regen-mana", -1, 16, 16, "nofreeze", -1, 1, 1),
		new BaseRuneword(++ItemCount, "Exile", Complete: true, Server: false, "pala", "", "", "", "", "", "", "", "", "VexOhmIstDol", "r26", "r27", "r24", "r14", "", "", "block2", -1, 30, 30, "freeze", -1, 1, 1, "ac%", -1, 220, 260, "aura", 104, 13, 16, "skilltab", 10, 2, 2, "hit-skill", 82, 15, 5, "rep-dur", 25, -1, -1),
		new BaseRuneword(++ItemCount, "Faith", Complete: true, Server: false, "miss", "", "", "", "", "", "", "", "", "OhmJahLemEld", "r27", "r31", "r20", "r02", "", "", "dmg%", -1, 280, 280, "att%", -1, 300, 300, "dmg-fire", -1, 120, 120, "res-all", -1, 15, 15, "aura", 122, 12, 15, "reanimate", 1, 10, 10, "allskills", -1, 1, 2),
		new BaseRuneword(++ItemCount, "Famine", Complete: true, Server: false, "axe", "hamm", "", "", "", "", "", "", "", "FalOhmOrtJah", "r19", "r27", "r09", "r31", "", "", "dmg%", -1, 270, 320, "lifesteal", -1, 12, 12, "swing2", -1, 30, 30, "noheal", -1, 1, 1, "dmg-mag", -1, 180, 200, "dmg-elem", 100, 50, 200, "ethereal", -1, 1, 1),
		new BaseRuneword(++ItemCount, "Flame", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Fortitude", Complete: true, Server: false, "weap", "tors", "", "", "", "", "", "", "", "ElSolDolLo", "r01", "r12", "r14", "r28", "", "", "ac%", -1, 200, 200, "dmg%", -1, 300, 300, "cast3", -1, 25, 25, "gethit-skill", 60, 20, 15, "dmg-to-mana", -1, 12, 12, "hp/lvl", -1, 8, 12, "res-all", -1, 25, 30),
		new BaseRuneword(++ItemCount, "Fortune", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Friendship", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Fury", Complete: true, Server: false, "mele", "", "", "", "", "", "", "", "", "", "r31", "r25", "r05", "", "", "", "dmg%", -1, 209, 209, "swing2", -1, 40, 40, "noheal", -1, 1, 1, "openwounds", -1, 66, 66, "lifesteal", -1, 6, 6, "deadly", -1, 33, 33, "skill", 147, 5, 5),
		new BaseRuneword(++ItemCount, "Gloom", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "FalUmPul", "r19", "r22", "r21", "", "", "", "ac%", -1, 170, 230, "res-all", -1, 30, 30, "gethit-skill", 71, 15, 3, "balance2", -1, 10, 10, "dmg-to-mana", -1, 5, 5, "light", -1, -3, -3, "half-freeze", -1, 1, 1),
		new BaseRuneword(++ItemCount, "Glory", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Grief", Complete: true, Server: false, "swor", "axe", "", "", "", "", "", "", "", "EthTirLoMalRal", "r05", "r03", "r28", "r23", "r08", "", "dmg-dem/lvl", 15, -1, -1, "dmg", -1, 340, 400, "swing3", -1, 30, 40, "hit-skill", 278, 35, 15, "pierce-pois", -1, 20, 25, "ignore-ac", -1, 1, 1, "heal-kill", -1, 10, 15),
		new BaseRuneword(++ItemCount, "Hand of Justice", Complete: true, Server: false, "weap", "", "", "", "", "", "", "", "", "SurChamAmnLo", "r29", "r32", "r11", "r28", "", "", "swing2", -1, 33, 33, "dmg%", -1, 280, 330, "aura", 102, 16, 16, "levelup-skill", 46, 100, 36, "death-skill", 56, 100, 48, "ignore-ac", -1, 1, 1, "pierce-fire", -1, 20, 20),
		new BaseRuneword(++ItemCount, "Harmony", Complete: true, Server: false, "miss", "", "", "", "", "", "", "", "", "TirIthSolKo", "r03", "r06", "r12", "r18", "", "", "dmg%", -1, 200, 275, "dmg-elem", -1, 55, 160, "charged", 95, 25, 20, "aura", 115, 10, 10, "oskill", 32, 2, 6, "regen-mana", -1, 20, 20, "light", -1, 2, 2),
		new BaseRuneword(++ItemCount, "Hatred", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Heart of the Oak", Complete: true, Server: false, "staf", "mace", "", "", "", "", "", "", "", "KoVexPulThul", "r18", "r26", "r21", "r10", "", "", "cast2", -1, 40, 40, "charged", 226, 25, 4, "mana%", -1, 15, 15, "allskills", -1, 3, 3, "regen", -1, 20, 20, "res-all", -1, 30, 40, "charged", 221, 60, 14),
		new BaseRuneword(++ItemCount, "Heaven's Will", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Holy Tears", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Holy Thunder", Complete: true, Server: false, "scep", "", "", "", "", "", "", "", "", "", "r05", "r08", "r09", "r07", "", "", "dmg%", -1, 60, 60, "dmg-ltng", -1, 20, 60, "dmg-max", -1, 10, 10, "res-ltng", -1, 60, 60, "res-ltng-max", -1, 5, 5, "skill", 118, 3, 3, "charged", 53, 60, 7),
		new BaseRuneword(++ItemCount, "Honor", Complete: true, Server: false, "mele", "", "", "", "", "", "", "", "", "", "r11", "r01", "r06", "r03", "r12", "", "dmg%", -1, 160, 160, "regen", -1, 10, 10, "allskills", -1, 1, 1, "att", -1, 200, 200, "deadly", -1, 25, 25, "str", -1, 10, 10, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Revenge", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Humility", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Hunger", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Ice", Complete: true, Server: false, "miss", "", "", "", "", "", "", "", "", "AmnShaelJahLo", "r11", "r13", "r31", "r28", "", "", "dmg%", -1, 140, 210, "aura", 114, 18, 18, "extra-cold", -1, 25, 30, "hit-skill", 44, 25, 22, "levelup-skill", 59, 100, 40, "pierce-cold", -1, 20, 20, "gold%/lvl", 25, -1, -1),
		new BaseRuneword(++ItemCount, "Infinity", Complete: true, Server: false, "pole", "", "", "", "", "", "", "", "", "BerMalBerIst", "r30", "r23", "r30", "r24", "", "", "dmg%", -1, 255, 325, "move3", -1, 35, 35, "vit/lvl", 4, -1, -1, "aura", 123, 12, 12, "kill-skill", 53, 50, 20, "pierce-ltng", -1, 45, 55, "charged", 235, 30, 21),
		new BaseRuneword(++ItemCount, "Innocence", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Insight", Complete: true, Server: false, "pole", "staf", "", "", "", "", "", "", "", "RalTirTalSol", "r08", "r03", "r07", "r12", "", "", "dmg%", -1, 200, 260, "att%", -1, 180, 250, "mag%", -1, 23, 23, "oskill", 9, 1, 6, "cast2", -1, 35, 35, "aura", 120, 12, 17, "all-stats", -1, 5, 5),
		new BaseRuneword(++ItemCount, "Jealousy", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Judgement", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "King's Grace", Complete: true, Server: false, "swor", "scep", "", "", "", "", "", "", "", "", "r11", "r08", "r10", "", "", "", "dmg%", -1, 100, 100, "att", -1, 150, 150, "dmg-demon", -1, 100, 100, "dmg-undead", -1, 50, 50, "att-demon", -1, 100, 100, "att-undead", -1, 100, 100, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Kingslayer", Complete: true, Server: false, "swor", "axe", "", "", "", "", "", "", "", "MalUmGulFal", "r23", "r22", "r25", "r19", "", "", "swing2", -1, 30, 30, "dmg%", -1, 230, 270, "reduce-ac", -1, 25, 25, "crush", -1, 33, 33, "openwounds", -1, 25, 25, "oskill", 111, 1, 1, "gold%", -1, 40, 40),
		new BaseRuneword(++ItemCount, "Knight's Vigil", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Knowledge", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Last Wish", Complete: true, Server: false, "swor", "hamm", "axe", "", "", "", "", "", "", "JahMalJahSurJahBer", "r31", "r23", "r31", "r29", "r31", "r30", "dmg%", -1, 330, 375, "att-skill", 38, 20, 20, "hit-skill", 82, 10, 18, "gethit-skill", 267, 6, 11, "crush", -1, 40, 50, "mag%/lvl", 4, -1, -1, "aura", 98, 17, 17),
		new BaseRuneword(++ItemCount, "Law", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Lawbringer", Complete: true, Server: false, "swor", "hamm", "scep", "", "", "", "", "", "", "AmnLemKo", "r11", "r20", "r18", "", "", "", "dmg-cold", -1, 130, 180, "dmg-fire", -1, 150, 210, "aura", 119, 16, 18, "hit-skill", 87, 20, 15, "ac-miss", -1, 200, 250, "rip", -1, 1, 1, "reduce-ac", -1, 50, 50),
		new BaseRuneword(++ItemCount, "Leaf", Complete: true, Server: false, "staf", "", "", "", "", "", "", "", "", "", "r03", "r08", "", "", "", "", "fireskill", -1, 3, 3, "ac/lvl", -1, 16, 16, "res-cold", -1, 33, 33, "skill", 41, 3, 3, "skill", 36, 3, 3, "skill", 37, 3, 3, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Lightning", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Lionheart", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "", "r15", "r17", "r19", "", "", "", "str", -1, 15, 15, "vit", -1, 20, 20, "dex", -1, 15, 15, "dmg%", -1, 20, 20, "hp", -1, 50, 50, "res-all", -1, 30, 30, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Lore", Complete: true, Server: false, "helm", "", "", "", "", "", "", "", "", "", "r09", "r12", "", "", "", "", "enr", -1, 10, 10, "allskills", -1, 1, 1, "light", -1, 2, 2, "mana-kill", -1, 2, 2, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Love", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Loyalty", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Lust", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Madness", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(ItemCount += 2, "Malice", Complete: true, Server: false, "mele", "", "", "", "", "", "", "", "", "", "r06", "r01", "r05", "", "", "", "openwounds", -1, 100, 100, "dmg-ac", -1, -100, -100, "noheal", -1, 1, 1, "dmg%", -1, 33, 33, "light", -1, -1, -1, "regen", -1, -5, -5, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Melody", Complete: true, Server: false, "miss", "", "", "", "", "", "", "", "", "", "r13", "r18", "r04", "", "", "", "dmg%", -1, 50, 50, "skilltab", 0, 3, 3, "skill", 9, 3, 3, "skill", 13, 3, 3, "skill", 17, 3, 3, "dmg-undead", -1, 300, 300, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Memory", Complete: true, Server: false, "staf", "", "", "", "", "", "", "", "", "", "r17", "r16", "r12", "r05", "", "", "mana%", -1, 20, 20, "red-mag", -1, 7, 7, "ac%", -1, 50, 50, "cast2", -1, 33, 33, "sor", -1, 3, 3, "skill", 58, 3, 3, "skill", 42, 2, 2),
		new BaseRuneword(++ItemCount, "Mist", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Morning", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Mystery", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Myth", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "HelAmnNef", "r15", "r11", "r04", "", "", "", "hit-skill", 137, 10, 1, "gethit-skill", 130, 3, 1, "bar", -1, 2, 2, "regen", -1, 10, 10, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Nadir", Complete: true, Server: false, "helm", "", "", "", "", "", "", "", "", "", "r04", "r03", "", "", "", "", "ac%", -1, 50, 50, "ac", -1, 10, 10, "light", -1, -3, -3, "charged", 264, 9, 13, "gold%", -1, -33, -33, "str", -1, 5, 5, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Nature's Kingdom", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Night", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Oath", Complete: true, Server: false, "swor", "axe", "mace", "", "", "", "", "", "", "ShaelPulMalLum", "r13", "r21", "r23", "r17", "", "", "dmg%", -1, 210, 340, "swing1", -1, 30, 30, "hit-skill", 93, 30, 20, "charged", 90, 14, 17, "charged", 236, 20, 16, "abs-mag", -1, 10, 15, "indestruct", -1, 1, 1),
		new BaseRuneword(++ItemCount, "Obedience", Complete: true, Server: false, "pole", "", "", "", "", "", "", "", "", "HelKoThulEthFal", "r15", "r18", "r10", "r05", "r19", "", "dmg%", -1, 370, 370, "crush", -1, 40, 40, "kill-skill", 52, 30, 21, "pierce-fire", -1, 25, 25, "ac", -1, 200, 300, "balance3", -1, 40, 40, "res-all", -1, 20, 30),
		new BaseRuneword(++ItemCount, "Oblivion", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Obsession", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Passion", Complete: true, Server: false, "weap", "", "", "", "", "", "", "", "", "DolOrtEldLem", "r14", "r09", "r02", "r20", "", "", "dmg%", -1, 160, 210, "oskill", 106, 1, 1, "att%", -1, 50, 80, "oskill", 152, 1, 1, "swing2", -1, 25, 25, "charged", 236, 12, 3, "stupidity", -1, 10, 10),
		new BaseRuneword(++ItemCount, "Patience", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Pattern", Complete: true, Server: false, "h2h", "", "", "", "", "", "", "", "", "", "r07", "r09", "r03", "", "", "", "att%", -1, 10, 10, "dmg%", -1, 40, 80, "dmg-fire", -1, 12, 32, "res-all", -1, 15, 15, "str", -1, 6, 6, "dex", -1, 6, 6, "block2", -1, 30, 30),
		new BaseRuneword(++ItemCount, "Peace", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "ShaelThulAmn", "r13", "r10", "r11", "", "", "", "hit-skill", 32, 2, 15, "gethit-skill", 17, 4, 5, "ama", -1, 2, 2, "oskill", 9, 2, 2, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Voice of Reason", Complete: true, Server: false, "swor", "mace", "", "", "", "", "", "", "", "LemKoElEld", "r20", "r18", "r01", "r02", "", "", "dmg-demon", -1, 220, 350, "dmg-undead", -1, 280, 300, "dmg-cold", -1, 100, 220, "pierce-cold", -1, 24, 24, "hit-skill", 45, 18, 20, "hit-skill", 64, 15, 13, "nofreeze", -1, 1, 1),
		new BaseRuneword(++ItemCount, "Penitence", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Peril", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Pestilence", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Phoenix", Complete: true, Server: false, "weap", "shld", "", "", "", "", "", "", "", "VexVexLoJah", "r26", "r26", "r28", "r31", "", "", "dmg%", -1, 350, 400, "ac-miss", -1, 350, 400, "hit-skill", 225, 40, 22, "levelup-skill", 46, 100, 40, "pierce-fire", -1, 28, 28, "aura", 124, 10, 15, "abs-fire", -1, 15, 21),
		new BaseRuneword(++ItemCount, "Piety", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Pillar of Faith", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Plague", Complete: true, Server: false, "weap", "", "", "", "", "", "", "", "", "ChamFalUm", "r32", "r19", "r22", "", "", "", "dmg-demon", -1, 260, 380, "gethit-skill", 91, 20, 12, "hit-skill", 92, 25, 15, "pierce-pois", -1, 23, 23, "deadly/lvl", 3, -1, -1, "aura", 109, 13, 17, "allskills", -1, 1, 2),
		new BaseRuneword(++ItemCount, "Praise", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Prayer", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Pride", Complete: true, Server: false, "pole", "", "", "", "", "", "", "", "", "ChamSurIoLo", "r32", "r29", "r16", "r28", "", "", "dmg-dem/lvl", 8, -1, -1, "dmg-ltng", -1, 50, 280, "att%", -1, 260, 300, "aura", 113, 16, 20, "gethit-skill", 51, 25, 17, "regen", -1, 8, 8, "gold%/lvl", 15, -1, -1),
		new BaseRuneword(++ItemCount, "Principle", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "RalGulEld", "r08", "r25", "r02", "", "", "", "hit-skill", 101, 100, 5, "pal", -1, 2, 2, "hp", -1, 100, 150, "dmg-undead", -1, 50, 50, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Prowess in Battle", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Prudence", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "", "r23", "r03", "", "", "", "", "ac%", -1, 140, 170, "red-mag", -1, 10, 10, "red-dmg", -1, 3, 3, "res-all", -1, 25, 35, "balance2", -1, 25, 25, "rep-dur", 25, -1, -1, "light", -1, 1, 1),
		new BaseRuneword(++ItemCount, "Punishment", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Purity", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Question", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Radiance", Complete: true, Server: false, "helm", "", "", "", "", "", "", "", "", "", "r04", "r12", "r06", "", "", "", "light", -1, 5, 5, "enr", -1, 10, 10, "vit", -1, 10, 10, "red-mag", -1, 3, 3, "mana", -1, 33, 33, "ac%", -1, 75, 75, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Rain", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "OrtMalIth", "r09", "r23", "r06", "", "", "", "hit-skill", 240, 5, 15, "gethit-skill", 235, 5, 15, "dru", -1, 2, 2, "mana", -1, 100, 150, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Reason", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Red", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Rhyme", Complete: true, Server: false, "shld", "", "", "", "", "", "", "", "", "", "r13", "r05", "", "", "", "", "block2", -1, 20, 20, "block", -1, 20, 20, "res-all", -1, 25, 25, "nofreeze", -1, 1, 1, "gold%", -1, 50, 50, "mag%", -1, 25, 25, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Rift", Complete: true, Server: false, "pole", "scep", "", "", "", "", "", "", "", "HelKoLemGul", "r15", "r18", "r20", "r25", "", "", "dmg-mag", -1, 160, 250, "dmg-fire", -1, 60, 180, "dmg-to-mana", -1, 38, 38, "hit-skill", 245, 20, 16, "att-skill", 64, 16, 21, "charged", 76, 40, 15, "all-stats", -1, 5, 10),
		new BaseRuneword(++ItemCount, "Sanctuary", Complete: true, Server: false, "shld", "", "", "", "", "", "", "", "", "KoKoMal", "r18", "r18", "r23", "", "", "", "block", -1, 20, 20, "block2", -1, 20, 20, "ac%", -1, 130, 160, "ac-miss", -1, 250, 250, "res-all", -1, 50, 70, "balance2", -1, 20, 20, "charged", 17, 60, 12),
		new BaseRuneword(++ItemCount, "Serendipity", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Shadow", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Shadow of Doubt", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Silence", Complete: true, Server: false, "weap", "", "", "", "", "", "", "", "", "", "r14", "r02", "r15", "r24", "r03", "r26", "manasteal", -1, 4, 4, "stupidity", -1, 33, 33, "dmg%", -1, 200, 200, "swing2", -1, 20, 20, "res-all", -1, 75, 75, "allskills", -1, 2, 2, "balance2", -1, 20, 20),
		new BaseRuneword(++ItemCount, "Siren's Song", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Smoke", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "", "r04", "r17", "", "", "", "", "ac-miss", -1, 250, 250, "ac%", -1, 75, 75, "res-all", -1, 50, 50, "balance2", -1, 20, 20, "light", -1, -1, -1, "charged", 72, 18, 6, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Sorrow", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Spirit", Complete: true, Server: false, "swor", "shld", "", "", "", "", "", "", "", "TalThulOrtAmn", "r07", "r10", "r09", "r11", "", "", "balance3", -1, 55, 55, "mana", -1, 89, 112, "ac-miss", -1, 250, 250, "vit", -1, 22, 22, "cast3", -1, 25, 35, "abs-mag", -1, 3, 8, "allskills", -1, 2, 2),
		new BaseRuneword(++ItemCount, "Splendor", Complete: true, Server: false, "shld", "", "", "", "", "", "", "", "", "", "r05", "r17", "", "", "", "", "light", -1, 3, 3, "gold%", -1, 50, 50, "mag%", -1, 20, 20, "ac%", -1, 60, 100, "block2", -1, 20, 20, "cast2", -1, 10, 10, "allskills", -1, 1, 1),
		new BaseRuneword(++ItemCount, "Starlight", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Stealth", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "", "r07", "r05", "", "", "", "", "red-mag", -1, 3, 3, "dex", -1, 6, 6, "stam", -1, 15, 15, "move2", -1, 25, 25, "cast2", -1, 25, 25, "balance2", -1, 25, 25, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Steel", Complete: true, Server: false, "swor", "axe", "mace", "", "", "", "", "", "", "", "r03", "r01", "", "", "", "", "swing2", -1, 25, 25, "dmg-min", -1, 3, 3, "dmg-max", -1, 3, 3, "openwounds", -1, 50, 50, "dmg%", -1, 20, 20, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Still Water", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Sting", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Stone", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "ShaelUmPulLum", "r13", "r22", "r21", "r17", "", "", "ac%", -1, 220, 260, "charged", 75, 16, 16, "ac-miss", -1, 300, 300, "charged", 229, 80, 16, "str", -1, 16, 16, "vit", -1, 16, 16, "balance2", -1, 40, 40),
		new BaseRuneword(++ItemCount, "Storm", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Strength", Complete: true, Server: false, "mele", "", "", "", "", "", "", "", "", "", "r11", "r03", "", "", "", "", "str", -1, 20, 20, "dmg%", -1, 35, 35, "vit", -1, 10, 10, "crush", -1, 25, 25, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Tempest", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Temptation", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Terror", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Thirst", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Thought", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Thunder", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Time", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Tradition", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Treachery", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "ShaelThulLem", "r13", "r10", "r20", "", "", "", "hit-skill", 278, 25, 15, "gethit-skill", 267, 5, 15, "ass", -1, 2, 2, "swing2", -1, 45, 45, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Trust", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Truth", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Unbending Will", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Valor", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Vengeance", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Venom", Complete: true, Server: false, "weap", "", "", "", "", "", "", "", "", "", "r07", "r14", "r23", "", "", "", "dmg-pois", 175, 312, 312, "ignore-ac", -1, 1, 1, "charged", 83, 27, 15, "charged", 92, 11, 13, "manasteal", -1, 7, 7, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Victory", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Voice", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Void", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "War", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Water", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Wealth", Complete: true, Server: false, "tors", "", "", "", "", "", "", "", "", "", "r20", "r18", "r03", "", "", "", "gold%", -1, 250, 250, "mag%", -1, 100, 100, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Whisper", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "White", Complete: true, Server: false, "wand", "", "", "", "", "", "", "", "", "", "r14", "r16", "", "", "", "", "skilltab", 7, 3, 3, "red-mag", -1, 4, 4, "cast2", -1, 20, 20, "mana", -1, 13, 13, "skill", 68, 3, 3, "skill", 84, 2, 2, "skill", 69, 4, 4),
		new BaseRuneword(++ItemCount, "Wind", Complete: true, Server: false, "mele", "", "", "", "", "", "", "", "", "", "r29", "r01", "", "", "", "", "dmg%", -1, 120, 160, "swing2", -1, 40, 40, "move2", -1, 20, 20, "reduce-ac", -1, 50, 50, "hit-skill", 245, 10, 9, "charged", 240, 127, 13, "balance2", -1, 15, 15),
		new BaseRuneword(++ItemCount, "Wings of Hope", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Wisdom", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Woe", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Wonder", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Wrath", Complete: true, Server: false, "miss", "", "", "", "", "", "", "", "", "PulLumBerMal", "r21", "r17", "r30", "r23", "", "", "dmg-demon", -1, 300, 300, "dmg-undead", -1, 250, 300, "dmg-ltng", -1, 41, 240, "dmg-mag", -1, 85, 120, "hit-skill", 82, 5, 10, "hit-skill", 87, 30, 1, "nofreeze", -1, 1, 1),
		new BaseRuneword(++ItemCount, "Youth", Complete: false, Server: false, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1, "", -1, -1, -1),
		new BaseRuneword(++ItemCount, "Zephyr", Complete: true, Server: false, "miss", "", "", "", "", "", "", "", "", "", "r09", "r05", "", "", "", "", "move2", -1, 25, 25, "swing2", -1, 25, 25, "dmg%", -1, 33, 33, "att", -1, 66, 66, "gethit-skill", 240, 7, 1, "ac", -1, 25, 25, "", -1, -1, -1)
	};

	public BaseRuneword(int Index, string Name, bool Complete, bool Server, string IType1, string IType2, string IType3, string IType4, string IType5, string IType6, string EType1, string EType2, string EType3, string Runes, string Rune1, string Rune2, string Rune3, string Rune4, string Rune5, string Rune6, string Property1, int Param1, int Min1, int Max1, string Property2, int Param2, int Min2, int Max2, string Property3, int Param3, int Min3, int Max3, string Property4, int Param4, int Min4, int Max4, string Property5, int Param5, int Min5, int Max5, string Property6, int Param6, int Min6, int Max6, string Property7, int Param7, int Min7, int Max7)
	{
		this.Index = Index;
		Id = (RunewordType)Index;
		this.Name = Name;
		this.Complete = Complete;
		this.Server = Server;
		if (IType1.Length == 0)
		{
			this.IType1 = null;
		}
		else
		{
			this.IType1 = BaseItemType.GetByCode(IType1);
		}
		if (IType2.Length == 0)
		{
			this.IType2 = null;
		}
		else
		{
			this.IType2 = BaseItemType.GetByCode(IType2);
		}
		if (IType3.Length == 0)
		{
			this.IType3 = null;
		}
		else
		{
			this.IType3 = BaseItemType.GetByCode(IType3);
		}
		if (IType4.Length == 0)
		{
			this.IType4 = null;
		}
		else
		{
			this.IType4 = BaseItemType.GetByCode(IType4);
		}
		if (IType5.Length == 0)
		{
			this.IType5 = null;
		}
		else
		{
			this.IType5 = BaseItemType.GetByCode(IType5);
		}
		if (IType6.Length == 0)
		{
			this.IType6 = null;
		}
		else
		{
			this.IType6 = BaseItemType.GetByCode(IType6);
		}
		if (EType1.Length == 0)
		{
			this.EType1 = null;
		}
		else
		{
			this.EType1 = BaseItemType.GetByCode(EType1);
		}
		if (EType2.Length == 0)
		{
			this.EType2 = null;
		}
		else
		{
			this.EType2 = BaseItemType.GetByCode(EType2);
		}
		if (EType3.Length == 0)
		{
			this.EType3 = null;
		}
		else
		{
			this.EType3 = BaseItemType.GetByCode(EType3);
		}
		this.Runes = Runes;
		this.Rune1 = Rune1;
		this.Rune2 = Rune2;
		this.Rune3 = Rune3;
		this.Rune4 = Rune4;
		this.Rune5 = Rune5;
		this.Rune6 = Rune6;
		if (Property1.Length == 0)
		{
			this.Property1 = null;
		}
		else
		{
			this.Property1 = BaseProperty.GetByCode(Property1);
		}
		this.Param1 = Param1;
		this.Min1 = Min1;
		this.Max1 = Max1;
		if (Property2.Length == 0)
		{
			this.Property2 = null;
		}
		else
		{
			this.Property2 = BaseProperty.GetByCode(Property2);
		}
		this.Param2 = Param2;
		this.Min2 = Min2;
		this.Max2 = Max2;
		if (Property3.Length == 0)
		{
			this.Property3 = null;
		}
		else
		{
			this.Property3 = BaseProperty.GetByCode(Property3);
		}
		this.Param3 = Param3;
		this.Min3 = Min3;
		this.Max3 = Max3;
		if (Property4.Length == 0)
		{
			this.Property4 = null;
		}
		else
		{
			this.Property4 = BaseProperty.GetByCode(Property4);
		}
		this.Param4 = Param4;
		this.Min4 = Min4;
		this.Max4 = Max4;
		if (Property5.Length == 0)
		{
			this.Property5 = null;
		}
		else
		{
			this.Property5 = BaseProperty.GetByCode(Property5);
		}
		this.Param5 = Param5;
		this.Min5 = Min5;
		this.Max5 = Max5;
		if (Property6.Length == 0)
		{
			this.Property6 = null;
		}
		else
		{
			this.Property6 = BaseProperty.GetByCode(Property6);
		}
		this.Param6 = Param6;
		this.Min6 = Min6;
		this.Max6 = Max6;
		if (Property7.Length == 0)
		{
			this.Property7 = null;
		}
		else
		{
			this.Property7 = BaseProperty.GetByCode(Property7);
		}
		this.Param7 = Param7;
		this.Min7 = Min7;
		this.Max7 = Max7;
	}

	public override string ToString()
	{
		return Name;
	}

	public static BaseRuneword Get(RunewordType type)
	{
		if (type < RunewordType.AncientsPledge || (int)type > ItemCount)
		{
			throw new ArgumentOutOfRangeException();
		}
		return items[(int)(type - 1)];
	}

	public static BaseRuneword Get(int index)
	{
		if (index < 1 || index > ItemCount)
		{
			throw new ArgumentOutOfRangeException();
		}
		return items[index - 1];
	}

	public static BaseRuneword Get(string name)
	{
		return GetByName(name);
	}

	public static BaseRuneword GetByName(string name)
	{
		for (int i = 0; i <= ItemCount; i++)
		{
			if (items[i] != null && items[i].Name == name)
			{
				return items[i];
			}
		}
		throw new ArgumentException($"No Runeword named {name} found !");
	}
}
