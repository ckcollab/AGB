namespace D2Data;

public class SignedStat : StatBase
{
	public int Value;

	public SignedStat(StatType stat, int val)
		: base(BaseStat.Get(stat))
	{
		Value = val;
	}

	public SignedStat(BaseStat stat, int val)
		: base(stat)
	{
		Value = val;
	}

	public override StatBase Clone()
	{
		return new SignedStat(BaseStat, Value);
	}

	public override string ToString()
	{
		return $"{BaseStat}: {Value}";
	}
}
