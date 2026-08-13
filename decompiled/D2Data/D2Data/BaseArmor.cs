namespace D2Data;

/// <summary>
/// Data classs holding a single base item's information (as in Armors.txt)
/// </summary>
public class BaseArmor : BaseItem
{
	public readonly uint NormalID;

	public readonly uint ExceptionalID;

	public readonly uint EliteID;

	public readonly int Absorbs;

	public readonly int Block;

	public readonly int DexBonus;

	public readonly int Durability;

	public readonly int lArm;

	public readonly int Legs;

	public readonly int lSPad;

	public readonly int MagicLevel;

	public readonly int MaxAc;

	public readonly int MaxDamage2;

	public readonly int MinAc;

	public readonly int MinDamage2;

	public readonly string OpenBetaGfx;

	public readonly bool Quivered;

	public readonly int rArm;

	public readonly int ReqStr;

	public readonly int rSPad;

	public readonly string SetInvFile;

	public readonly int SpellOffset;

	public readonly int StrBonus;

	public readonly bool Throwable;

	public readonly int Torso;

	public BaseArmor(int Index, string Name, int Version, int CompactSave, int Rarity, int Spawnable, int MinAc, int MaxAc, int Absorbs, int Speed, int ReqStr, int Block, int Durability, int NoDurability, int Level, int LevelReq, int Cost, int GambleCost, string Code, string NameString, int MagicLevel, int AutoPrefix, string AlternateGfx, string OpenBetaGfx, string NormCode, string UberCode, string UltraCode, int SpellOffset, int Component, int InvWidth, int InvHeight, int HasInv, int GemSockets, int GemApplyType, string FlippyFile, string InvFile, string UniqueInvFile, string SetInvFile, int lArm, int rArm, int Torso, int Legs, int rSPad, int lSPad, int Useable, int Throwable, int Stackable, int MinStack, int MaxStack, string Type, string Type2, string DropSound, int DropSfxFrame, string UseSound, int Unique, int Transparent, int TransTbl, int Quivered, int LightRadius, int Belt, int Quest, int MissileType, int DurabilityWarning, int QuantityWarning, int MinDamage, int MaxDamage, int StrBonus, int DexBonus, int GemOffset, int BitField1, int CharsiMin, int CharsiMax, int CharsiMagicMin, int CharsiMagicMax, int CharsiMagicLvl, int GheedMin, int GheedMax, int GheedMagicMin, int GheedMagicMax, int GheedMagicLvl, int AkaraMin, int AkaraMax, int AkaraMagicMin, int AkaraMagicMax, int AkaraMagicLvl, int FaraMin, int FaraMax, int FaraMagicMin, int FaraMagicMax, int FaraMagicLvl, int LysanderMin, int LysanderMax, int LysanderMagicMin, int LysanderMagicMax, int LysanderMagicLvl, int DrognanMin, int DrognanMax, int DrognanMagicMin, int DrognanMagicMax, int DrognanMagicLvl, int HraltiMin, int HraltiMax, int HraltiMagicMin, int HraltiMagicMax, int HraltiMagicLvl, int AlkorMin, int AlkorMax, int AlkorMagicMin, int AlkorMagicMax, int AlkorMagicLvl, int OrmusMin, int OrmusMax, int OrmusMagicMin, int OrmusMagicMax, int OrmusMagicLvl, int ElzixMin, int ElzixMax, int ElzixMagicMin, int ElzixMagicMax, int ElzixMagicLvl, int AshearaMin, int AshearaMax, int AshearaMagicMin, int AshearaMagicMax, int AshearaMagicLvl, int CainMin, int CainMax, int CainMagicMin, int CainMagicMax, int CainMagicLvl, int HalbuMin, int HalbuMax, int HalbuMagicMin, int HalbuMagicMax, int HalbuMagicLvl, int JamellaMin, int JamellaMax, int JamellaMagicMin, int JamellaMagicMax, int JamellaMagicLvl, int LarzukMin, int LarzukMax, int LarzukMagicMin, int LarzukMagicMax, int LarzukMagicLvl, int MalahMin, int MalahMax, int MalahMagicMin, int MalahMagicMax, int MalahMagicLvl, int DrehyaMin, int DrehyaMax, int DrehyaMagicMin, int DrehyaMagicMax, int DrehyaMagicLvl, int SourceArt, int GameArt, int Transform, int InvTrans, int SkipName, string NightmareUpgrade, string HellUpgrade, int MinDamage2, int MaxDamage2, int Nameable)
	{
		_class = (ItemClass)Index;
		index = Index;
		tableIndex = Index - BaseItem.BASE_ARMOR_START;
		baseType = ((Type == "") ? null : BaseItemType.GetByCode(Type));
		baseType2 = ((Type2 == "") ? null : BaseItemType.GetByCode(Type2));
		code = Code;
		id = BaseItem.GetIdFromCode(Code);
		nightmareUpgrade = ((!(NightmareUpgrade == "xxx")) ? BaseItem.GetIdFromCode(NightmareUpgrade) : 0u);
		hellUpgrade = ((!(HellUpgrade == "xxx")) ? BaseItem.GetIdFromCode(HellUpgrade) : 0u);
		NormalID = BaseItem.GetIdFromCode(NormCode);
		ExceptionalID = BaseItem.GetIdFromCode(UberCode);
		EliteID = BaseItem.GetIdFromCode(UltraCode);
		this.MinAc = MinAc;
		this.MaxAc = MaxAc;
		this.Absorbs = Absorbs;
		this.ReqStr = ReqStr;
		this.Block = Block;
		this.Durability = Durability;
		this.MagicLevel = MagicLevel;
		this.SpellOffset = SpellOffset;
		this.SetInvFile = SetInvFile;
		this.lArm = lArm;
		this.rArm = rArm;
		this.Torso = Torso;
		this.Legs = Legs;
		this.rSPad = rSPad;
		this.lSPad = lSPad;
		this.Throwable = Throwable == 1;
		this.Quivered = Quivered == 1;
		this.StrBonus = StrBonus;
		this.DexBonus = DexBonus;
		name = Name;
		version = Version;
		compactSave = CompactSave;
		rarity = Rarity;
		spawnable = Spawnable == 1;
		speed = Speed;
		noDurability = NoDurability == 1;
		level = Level;
		levelReq = LevelReq;
		cost = Cost;
		gambleCost = GambleCost;
		nameString = NameString;
		autoPrefix = AutoPrefix;
		alternateGfx = AlternateGfx;
		this.OpenBetaGfx = OpenBetaGfx;
		component = Component;
		invWidth = InvWidth;
		invHeight = InvHeight;
		hasInv = HasInv == 1;
		gemSockets = GemSockets;
		gemApplyType = GemApplyType;
		flippyFile = FlippyFile;
		invFile = InvFile;
		uniqueInvFile = UniqueInvFile;
		useable = Useable == 1;
		stackable = Stackable == 1;
		minStack = MinStack;
		maxStack = MaxStack;
		dropSound = DropSound;
		dropSfxFrame = DropSfxFrame;
		useSound = UseSound;
		unique = Unique == 1;
		transparent = Transparent == 1;
		transTbl = TransTbl;
		lightRadius = LightRadius;
		belt = Belt == 1;
		quest = Quest;
		missileType = MissileType;
		durabilityWarning = DurabilityWarning;
		quantityWarning = QuantityWarning;
		minDamage = MinDamage;
		maxDamage = MaxDamage;
		gemOffset = GemOffset;
		bitField1 = BitField1;
		charsiMin = CharsiMin;
		charsiMax = CharsiMax;
		charsiMagicMin = CharsiMagicMin;
		charsiMagicMax = CharsiMagicMax;
		charsiMagicLvl = CharsiMagicLvl;
		gheedMin = GheedMin;
		gheedMax = GheedMax;
		gheedMagicMin = GheedMagicMin;
		gheedMagicMax = GheedMagicMax;
		gheedMagicLvl = GheedMagicLvl;
		akaraMin = AkaraMin;
		akaraMax = AkaraMax;
		akaraMagicMin = AkaraMagicMin;
		akaraMagicMax = AkaraMagicMax;
		akaraMagicLvl = AkaraMagicLvl;
		faraMin = FaraMin;
		faraMax = FaraMax;
		faraMagicMin = FaraMagicMin;
		faraMagicMax = FaraMagicMax;
		faraMagicLvl = FaraMagicLvl;
		lysanderMin = LysanderMin;
		lysanderMax = LysanderMax;
		lysanderMagicMin = LysanderMagicMin;
		lysanderMagicMax = LysanderMagicMax;
		lysanderMagicLvl = LysanderMagicLvl;
		drognanMin = DrognanMin;
		drognanMax = DrognanMax;
		drognanMagicMin = DrognanMagicMin;
		drognanMagicMax = DrognanMagicMax;
		drognanMagicLvl = DrognanMagicLvl;
		hraltiMin = HraltiMin;
		hraltiMax = HraltiMax;
		hraltiMagicMin = HraltiMagicMin;
		hraltiMagicMax = HraltiMagicMax;
		hraltiMagicLvl = HraltiMagicLvl;
		alkorMin = AlkorMin;
		alkorMax = AlkorMax;
		alkorMagicMin = AlkorMagicMin;
		alkorMagicMax = AlkorMagicMax;
		alkorMagicLvl = AlkorMagicLvl;
		ormusMin = OrmusMin;
		ormusMax = OrmusMax;
		ormusMagicMin = OrmusMagicMin;
		ormusMagicMax = OrmusMagicMax;
		ormusMagicLvl = OrmusMagicLvl;
		elzixMin = ElzixMin;
		elzixMax = ElzixMax;
		elzixMagicMin = ElzixMagicMin;
		elzixMagicMax = ElzixMagicMax;
		elzixMagicLvl = ElzixMagicLvl;
		ashearaMin = AshearaMin;
		ashearaMax = AshearaMax;
		ashearaMagicMin = AshearaMagicMin;
		ashearaMagicMax = AshearaMagicMax;
		ashearaMagicLvl = AshearaMagicLvl;
		cainMin = CainMin;
		cainMax = CainMax;
		cainMagicMin = CainMagicMin;
		cainMagicMax = CainMagicMax;
		cainMagicLvl = CainMagicLvl;
		halbuMin = HalbuMin;
		halbuMax = HalbuMax;
		halbuMagicMin = HalbuMagicMin;
		halbuMagicMax = HalbuMagicMax;
		halbuMagicLvl = HalbuMagicLvl;
		jamellaMin = JamellaMin;
		jamellaMax = JamellaMax;
		jamellaMagicMin = JamellaMagicMin;
		jamellaMagicMax = JamellaMagicMax;
		jamellaMagicLvl = JamellaMagicLvl;
		larzukMin = LarzukMin;
		larzukMax = LarzukMax;
		larzukMagicMin = LarzukMagicMin;
		larzukMagicMax = LarzukMagicMax;
		larzukMagicLvl = LarzukMagicLvl;
		malahMin = MalahMin;
		malahMax = MalahMax;
		malahMagicMin = MalahMagicMin;
		malahMagicMax = MalahMagicMax;
		malahMagicLvl = MalahMagicLvl;
		drehyaMin = DrehyaMin;
		drehyaMax = DrehyaMax;
		drehyaMagicMin = DrehyaMagicMin;
		drehyaMagicMax = DrehyaMagicMax;
		drehyaMagicLvl = DrehyaMagicLvl;
		sourceArt = SourceArt;
		gameArt = GameArt;
		transform = Transform;
		invTrans = InvTrans;
		skipName = SkipName;
		minDamage = MinDamage;
		maxDamage = MaxDamage;
		nameable = Nameable == 1;
	}
}
