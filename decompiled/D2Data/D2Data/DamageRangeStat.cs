namespace D2Data;

public class DamageRangeStat : SignedStatParam
{
	public readonly DamageType Type;

	public readonly bool IsPercent;

	public int Min => Value;

	public int Max => Param;

	public DamageRangeStat(BaseStat stat, int min, int max)
		: base(stat, min, max)
	{
		switch (stat.Type)
		{
		case StatType.MaxDamagePercent:
		case StatType.MinDamagePercent:
			Type = DamageType.Physical;
			IsPercent = true;
			break;
		case StatType.FireMinDamage:
			Type = DamageType.Fire;
			break;
		case StatType.LightMinDamage:
			Type = DamageType.Lightning;
			break;
		case StatType.MagicMinDamage:
			Type = DamageType.Magic;
			break;
		case StatType.ColdMinDamage:
			Type = DamageType.Cold;
			break;
		case StatType.PoisonMinDamage:
			Type = DamageType.Poison;
			break;
		}
	}

	public override StatBase Clone()
	{
		return new DamageRangeStat(BaseStat, Value, Param);
	}

	public override string ToString()
	{
		if (Min == Max)
		{
			return string.Format("+{0}{1} {2} Damage", Min, IsPercent ? "%" : "", Type);
		}
		return string.Format("+{0}-{1}{2} {3} Damage", Min, Max, IsPercent ? "%" : "", Type);
	}
}
