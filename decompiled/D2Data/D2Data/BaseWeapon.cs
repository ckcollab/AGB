namespace D2Data;

/// <summary>
/// Data classs holding a single base item's information (as in Weapons.txt)
/// </summary>
public class BaseWeapon : BaseItem
{
	public readonly uint NormalID;

	public readonly uint ExceptionalID;

	public readonly uint EliteID;

	public readonly int DexBonus;

	public readonly int Durability;

	public readonly string HitClass;

	public readonly int MagicLevel;

	public readonly int MaxMisDamage;

	public readonly int MinMisDamage;

	public readonly bool OneOrTwoHanded;

	public readonly string OpenBetaGfx;

	public readonly bool PermStoreItem;

	public readonly bool QuestDiffCheck;

	public readonly bool Quivered;

	public readonly int RangeAdder;

	public readonly int ReqDex;

	public readonly int ReqStr;

	public readonly int StrBonus;

	public readonly bool TwoHanded;

	public readonly string TwoHandedWeaponClass;

	public readonly int TwoHandMaxDamage;

	public readonly int TwoHandMinDamage;

	public readonly string WeaponClass;

	public readonly string SetInvFile;

	public readonly int SpawnStack;

	public readonly string Special;

	public readonly int Unknown;

	public BaseWeapon(int Index, string Name, string Type, string Type2, string Code, string AlternateGfx, string NameString, int Version, int CompactSave, int Rarity, int Spawnable, int MinDamage, int MaxDamage, int OneOrTwoHanded, int TwoHanded, int TwoHandMinDamage, int TwoHandMaxDamage, int MinMisDamage, int MaxMisDamage, int Unknown, int RangeAdder, int Speed, int StrBonus, int DexBonus, int ReqStr, int ReqDex, int Durability, int NoDurability, int Level, int LevelReq, int Cost, int GambleCost, int MagicLevel, int AutoPrefix, string OpenBetaGfx, string NormCode, string UberCode, string UltraCode, string WeaponClass, string TwoHandedWeaponClass, int Component, string HitClass, int InvWidth, int InvHeight, int Stackable, int MinStack, int MaxStack, int SpawnStack, string FlippyFile, string InvFile, string UniqueInvFile, string SetInvFile, int HasInv, int GemSockets, int GemApplyType, string Special, int Useable, string DropSound, int DropSfxFrame, string UseSound, int Unique, int Transparent, int TransTbl, int Quivered, int LightRadius, int Belt, int Quest, int QuestDiffCheck, int MissileType, int DurabilityWarning, int QuantityWarning, int GemOffset, int BitField1, int CharsiMin, int CharsiMax, int CharsiMagicMin, int CharsiMagicMax, int CharsiMagicLvl, int GheedMin, int GheedMax, int GheedMagicMin, int GheedMagicMax, int GheedMagicLvl, int AkaraMin, int AkaraMax, int AkaraMagicMin, int AkaraMagicMax, int AkaraMagicLvl, int FaraMin, int FaraMax, int FaraMagicMin, int FaraMagicMax, int FaraMagicLvl, int LysanderMin, int LysanderMax, int LysanderMagicMin, int LysanderMagicMax, int LysanderMagicLvl, int DrognanMin, int DrognanMax, int DrognanMagicMin, int DrognanMagicMax, int DrognanMagicLvl, int HraltiMin, int HraltiMax, int HraltiMagicMin, int HraltiMagicMax, int HraltiMagicLvl, int AlkorMin, int AlkorMax, int AlkorMagicMin, int AlkorMagicMax, int AlkorMagicLvl, int OrmusMin, int OrmusMax, int OrmusMagicMin, int OrmusMagicMax, int OrmusMagicLvl, int ElzixMin, int ElzixMax, int ElzixMagicMin, int ElzixMagicMax, int ElzixMagicLvl, int AshearaMin, int AshearaMax, int AshearaMagicMin, int AshearaMagicMax, int AshearaMagicLvl, int CainMin, int CainMax, int CainMagicMin, int CainMagicMax, int CainMagicLvl, int HalbuMin, int HalbuMax, int HalbuMagicMin, int HalbuMagicMax, int HalbuMagicLvl, int JamellaMin, int JamellaMax, int JamellaMagicMin, int JamellaMagicMax, int JamellaMagicLvl, int LarzukMin, int LarzukMax, int LarzukMagicMin, int LarzukMagicMax, int LarzukMagicLvl, int DrehyaMin, int DrehyaMax, int DrehyaMagicMin, int DrehyaMagicMax, int DrehyaMagicLvl, int MalahMin, int MalahMax, int MalahMagicMin, int MalahMagicMax, int MalahMagicLvl, int SourceArt, int GameArt, int Transform, int InvTrans, int SkipName, string NightmareUpgrade, string HellUpgrade, int Nameable, int PermStoreItem)
	{
		_class = (ItemClass)Index;
		index = Index;
		tableIndex = Index - BaseItem.BASE_WEAPON_START;
		baseType = ((Type == "") ? null : BaseItemType.GetByCode(Type));
		baseType2 = ((Type2 == "") ? null : BaseItemType.GetByCode(Type2));
		code = Code;
		id = BaseItem.GetIdFromCode(Code);
		nightmareUpgrade = ((!(NightmareUpgrade == "xxx")) ? BaseItem.GetIdFromCode(NightmareUpgrade) : 0u);
		hellUpgrade = ((!(HellUpgrade == "xxx")) ? BaseItem.GetIdFromCode(HellUpgrade) : 0u);
		NormalID = BaseItem.GetIdFromCode(NormCode);
		ExceptionalID = BaseItem.GetIdFromCode(UberCode);
		EliteID = BaseItem.GetIdFromCode(UltraCode);
		this.OneOrTwoHanded = OneOrTwoHanded == 1;
		this.TwoHanded = TwoHanded == 1;
		this.TwoHandMinDamage = TwoHandMinDamage;
		this.TwoHandMaxDamage = TwoHandMaxDamage;
		this.MinMisDamage = MinMisDamage;
		this.MaxMisDamage = MaxMisDamage;
		this.Unknown = Unknown;
		this.RangeAdder = RangeAdder;
		this.StrBonus = StrBonus;
		this.DexBonus = DexBonus;
		this.ReqStr = ReqStr;
		this.ReqDex = ReqDex;
		this.Durability = Durability;
		this.MagicLevel = MagicLevel;
		this.WeaponClass = WeaponClass;
		this.TwoHandedWeaponClass = TwoHandedWeaponClass;
		this.HitClass = HitClass;
		this.SpawnStack = SpawnStack;
		this.SetInvFile = SetInvFile;
		this.Special = Special;
		this.Quivered = Quivered == 1;
		this.QuestDiffCheck = QuestDiffCheck == 1;
		this.PermStoreItem = PermStoreItem == 1;
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
