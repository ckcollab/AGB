namespace D2Data;

public class SkillInfo
{
	public SkillType Skill;

	public int BaseLevel;

	public int ItemBonus;

	public int StateBonus;

	public uint ChargedItem = uint.MaxValue;

	public DamageTypes DamageTypes;

	public int CastDelay;

	public int Level
	{
		get
		{
			int num = BaseLevel;
			if (ItemBonus > 0)
			{
				num += ItemBonus;
			}
			if (StateBonus > 0)
			{
				num += StateBonus;
			}
			return num;
		}
	}

	public SkillInfo(SkillType skill, int baseLevel)
	{
		Skill = skill;
		BaseLevel = baseLevel;
		DamageTypes = GetDamageTypes(skill);
		CastDelay = GetCastDelay(skill);
	}

	public SkillInfo(SkillType skill, uint chargedItem)
	{
		Skill = skill;
		ChargedItem = chargedItem;
		DamageTypes = GetDamageTypes(skill);
		CastDelay = GetCastDelay(skill);
	}

	public static DamageTypes GetDamageTypes(SkillType skill)
	{
		switch (skill)
		{
		case SkillType.FireArrow:
		case SkillType.ExplodingArrow:
		case SkillType.ImmolationArrow:
		case SkillType.FireBolt:
		case SkillType.Inferno:
		case SkillType.Blaze:
		case SkillType.FireBall:
		case SkillType.FireWall:
		case SkillType.Enchant:
		case SkillType.Meteor:
		case SkillType.Hydra:
		case SkillType.Firestorm:
		case SkillType.MoltenBoulder:
		case SkillType.Fissure:
		case SkillType.FireClaws:
		case SkillType.Armageddon:
		case SkillType.FireBlast:
		case SkillType.FistsOfFire:
		case SkillType.WakeOfFire:
			return DamageTypes.Fire;
		case SkillType.PowerStrike:
		case SkillType.LightningBolt:
		case SkillType.ChargedStrike:
		case SkillType.LightningStrike:
		case SkillType.LightningFury:
		case SkillType.ChargedBolt:
		case SkillType.StaticField:
		case SkillType.Nova:
		case SkillType.Lightning:
		case SkillType.ChainLightning:
		case SkillType.ThunderStorm:
		case SkillType.ShockWeb:
		case SkillType.ChargedBoltSentry:
		case SkillType.ClawsOfThunder:
		case SkillType.LightningSentry:
			return DamageTypes.Lightning;
		case SkillType.ColdArrow:
		case SkillType.IceArrow:
		case SkillType.FreezingArrow:
		case SkillType.IceBolt:
		case SkillType.FrozenArmor:
		case SkillType.FrostNova:
		case SkillType.IceBlast:
		case SkillType.ShiverArmor:
		case SkillType.GlacialSpike:
		case SkillType.Blizzard:
		case SkillType.ChillingArmor:
		case SkillType.FrozenOrb:
		case SkillType.ArcticBlast:
		case SkillType.Hurricane:
		case SkillType.BladesOfIce:
			return DamageTypes.Cold;
		case SkillType.Attack:
		case SkillType.Throw:
		case SkillType.Jab:
		case SkillType.MultipleShot:
		case SkillType.Impale:
		case SkillType.GuidedArrow:
		case SkillType.Strafe:
		case SkillType.Fend:
		case SkillType.Sacrifice:
		case SkillType.Smite:
		case SkillType.Zeal:
		case SkillType.Charge:
		case SkillType.Bash:
		case SkillType.DoubleSwing:
		case SkillType.DoubleThrow:
		case SkillType.LeapAttack:
		case SkillType.Concentrate:
		case SkillType.Frenzy:
		case SkillType.Whirlwind:
		case SkillType.Maul:
		case SkillType.Twister:
		case SkillType.Hunger:
		case SkillType.Tornado:
		case SkillType.Fury:
		case SkillType.TigerStrike:
		case SkillType.DragonTalon:
		case SkillType.BladeSentinel:
		case SkillType.DragonClaw:
		case SkillType.CobraStrike:
		case SkillType.BladeFury:
		case SkillType.DragonFlight:
			return DamageTypes.Physical;
		case SkillType.MagicArrow:
		case SkillType.Teeth:
		case SkillType.BoneSpear:
		case SkillType.BoneSpirit:
		case SkillType.BlessedHammer:
		case SkillType.Berserk:
			return DamageTypes.Magic;
		case SkillType.PoisonJavelin:
		case SkillType.PlagueJavelin:
		case SkillType.PoisonDagger:
		case SkillType.PoisonExplosion:
		case SkillType.PoisonNova:
		case SkillType.Rabies:
			return DamageTypes.Poison;
		case SkillType.HolyBolt:
			return DamageTypes.Undead;
		case SkillType.Volcano:
			return DamageTypes.Fire | DamageTypes.Physical;
		case SkillType.FistOfTheHeavens:
			return DamageTypes.Lightning | DamageTypes.Undead;
		case SkillType.Vengeance:
			return DamageTypes.Fire | DamageTypes.Lightning | DamageTypes.Cold;
		case SkillType.DeathSentry:
			return DamageTypes.Fire | DamageTypes.Lightning | DamageTypes.Physical;
		case SkillType.DragonTail:
			return DamageTypes.Fire | DamageTypes.Physical;
		case SkillType.CorpseExplosion:
			return DamageTypes.Fire | DamageTypes.Physical;
		default:
			return DamageTypes.None;
		}
	}

	public static int GetCastDelay(SkillType skill)
	{
		switch (skill)
		{
		case SkillType.FireWall:
			return 1400;
		case SkillType.Meteor:
			return 1200;
		case SkillType.Hydra:
			return 2000;
		case SkillType.Blizzard:
			return 1800;
		case SkillType.FrozenOrb:
			return 1000;
		case SkillType.DragonFlight:
			return 1000;
		case SkillType.BladeSentinel:
			return 2000;
		case SkillType.ShadowWarrior:
		case SkillType.ShadowMaster:
			return 6000;
		case SkillType.Firestorm:
			return 600;
		case SkillType.MoltenBoulder:
		case SkillType.Fissure:
			return 2000;
		case SkillType.Volcano:
			return 4000;
		case SkillType.Armageddon:
		case SkillType.Hurricane:
			return 6000;
		case SkillType.Wearwolf:
		case SkillType.Wearbear:
		case SkillType.SummonGrizzly:
			return 1000;
		case SkillType.PoisonJavelin:
			return 500;
		case SkillType.PlagueJavelin:
			return 4000;
		case SkillType.ImmolationArrow:
			return 1000;
		default:
			return 0;
		}
	}

	public override string ToString()
	{
		return string.Concat(Skill, "(", Level, ")");
	}
}
