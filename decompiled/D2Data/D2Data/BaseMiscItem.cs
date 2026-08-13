namespace D2Data;

/// <summary>
/// Data classs holding a single base item's information (as in Misc.txt)
/// </summary>
public class BaseMiscItem : BaseItem
{
	public readonly int SpawnStack;

	public readonly bool AutoBelt;

	public readonly ItemClass BetterGem;

	public readonly int Calc1;

	public readonly int Calc2;

	public readonly int Calc3;

	public readonly StateType CState1;

	public readonly StateType CState2;

	public readonly int Len;

	public readonly bool MultiBuy;

	public readonly string Name2;

	public readonly bool PermStoreItem;

	public readonly int pSpell;

	public readonly bool QuestDiffCheck;

	public readonly string Special;

	public readonly int SpellDesc;

	public readonly int SpellDescCalc;

	public readonly string SpellDescStr;

	public readonly int SpellIcon;

	public readonly StatType Stat1;

	public readonly StatType Stat2;

	public readonly StatType Stat3;

	public readonly StateType State;

	public readonly int SzFlavorText;

	public readonly bool Throwable;

	public readonly int TMogMax;

	public readonly int TMogMin;

	public readonly string TMogType;

	public readonly bool Transmogrify;

	public BaseMiscItem(int Index, string Name, string Name2, int SzFlavorText, int CompactSave, int Version, int Level, int LevelReq, int Rarity, int Spawnable, int Speed, int NoDurability, int Cost, int GambleCost, int AutoPrefix, string Code, string AlternateGfx, string NameString, int Component, int InvWidth, int InvHeight, int HasInv, int GemSockets, int GemApplyType, string FlippyFile, string InvFile, string UniqueInvFile, string Special, int Transmogrify, string TMogType, int TMogMin, int TMogMax, int Useable, int Throwable, string Type, string Type2, string DropSound, int DropSfxFrame, string UseSound, int Unique, int Transparent, int TransTbl, int LightRadius, int Belt, int AutoBelt, int Stackable, int MinStack, int MaxStack, int SpawnStack, int Quest, int QuestDiffCheck, int MissileType, int SpellIcon, int pSpell, int State, int CState1, int CState2, int Len, int Stat1, int Calc1, int Stat2, int Calc2, int Stat3, int Calc3, int SpellDesc, string SpellDescStr, int SpellDescCalc, int DurabilityWarning, int QuantityWarning, int GemOffset, int BetterGem, int BitField1, int CharsiMin, int CharsiMax, int CharsiMagicMin, int CharsiMagicMax, int CharsiMagicLvl, int GheedMin, int GheedMax, int GheedMagicMin, int GheedMagicMax, int GheedMagicLvl, int AkaraMin, int AkaraMax, int AkaraMagicMin, int AkaraMagicMax, int AkaraMagicLvl, int FaraMin, int FaraMax, int FaraMagicMin, int FaraMagicMax, int FaraMagicLvl, int LysanderMin, int LysanderMax, int LysanderMagicMin, int LysanderMagicMax, int LysanderMagicLvl, int DrognanMin, int DrognanMax, int DrognanMagicMin, int DrognanMagicMax, int DrognanMagicLvl, int HraltiMin, int HraltiMax, int HraltiMagicMin, int HraltiMagicMax, int HraltiMagicLvl, int AlkorMin, int AlkorMax, int AlkorMagicMin, int AlkorMagicMax, int AlkorMagicLvl, int OrmusMin, int OrmusMax, int OrmusMagicMin, int OrmusMagicMax, int OrmusMagicLvl, int ElzixMin, int ElzixMax, int ElzixMagicMin, int ElzixMagicMax, int ElzixMagicLvl, int AshearaMin, int AshearaMax, int AshearaMagicMin, int AshearaMagicMax, int AshearaMagicLvl, int CainMin, int CainMax, int CainMagicMin, int CainMagicMax, int CainMagicLvl, int HalbuMin, int HalbuMax, int HalbuMagicMin, int HalbuMagicMax, int HalbuMagicLvl, int MalahMin, int MalahMax, int MalahMagicMin, int MalahMagicMax, int MalahMagicLvl, int LarzukMin, int LarzukMax, int LarzukMagicMin, int LarzukMagicMax, int LarzukMagicLvl, int DrehyaMin, int DrehyaMax, int DrehyaMagicMin, int DrehyaMagicMax, int DrehyaMagicLvl, int JamellaMin, int JamellaMax, int JamellaMagicMin, int JamellaMagicMax, int JamellaMagicLvl, int SourceArt, int GameArt, int Transform, int InvTrans, int SkipName, string NightmareUpgrade, string HellUpgrade, int MinDamage, int MaxDamage, int PermStoreItem, int MultiBuy, int Nameable)
	{
		_class = (ItemClass)Index;
		index = Index;
		tableIndex = Index;
		baseType = ((Type == "") ? null : BaseItemType.GetByCode(Type));
		baseType2 = ((Type2 == "") ? null : BaseItemType.GetByCode(Type2));
		code = Code;
		id = BaseItem.GetIdFromCode(Code);
		nightmareUpgrade = ((!(NightmareUpgrade == "xxx")) ? BaseItem.GetIdFromCode(NightmareUpgrade) : 0u);
		hellUpgrade = ((!(HellUpgrade == "xxx")) ? BaseItem.GetIdFromCode(HellUpgrade) : 0u);
		this.Name2 = Name2;
		this.SzFlavorText = SzFlavorText;
		this.Special = Special;
		this.Transmogrify = Transmogrify == 1;
		this.TMogType = TMogType;
		this.TMogMin = TMogMin;
		this.TMogMax = TMogMax;
		this.Throwable = Throwable == 1;
		this.AutoBelt = AutoBelt == 1;
		this.SpawnStack = SpawnStack;
		this.QuestDiffCheck = QuestDiffCheck == 1;
		this.SpellIcon = SpellIcon;
		this.pSpell = pSpell;
		this.State = (StateType)State;
		this.CState1 = (StateType)CState1;
		this.CState2 = (StateType)CState2;
		this.Len = Len;
		this.Stat1 = (StatType)Stat1;
		this.Calc1 = Calc1;
		this.Stat2 = (StatType)Stat2;
		this.Calc2 = Calc2;
		this.Stat3 = (StatType)Stat3;
		this.Calc3 = Calc3;
		this.SpellDesc = SpellDesc;
		this.SpellDescStr = SpellDescStr;
		this.SpellDescCalc = SpellDescCalc;
		this.BetterGem = (ItemClass)BetterGem;
		this.PermStoreItem = PermStoreItem == 1;
		this.MultiBuy = MultiBuy == 1;
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
