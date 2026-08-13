namespace D2Data;

public class PerLevelStat : SignedStat
{
	public double PerLevel => (double)Value / (double)(1 << BaseStat.OpParam);

	public PerLevelStat(BaseStat stat, int val)
		: base(stat, val)
	{
	}

	public override StatBase Clone()
	{
		return new PerLevelStat(BaseStat, Value);
	}

	public override string ToString()
	{
		return $"+{PerLevel} To {BaseStat.OpStat1} Per Level";
	}
}
