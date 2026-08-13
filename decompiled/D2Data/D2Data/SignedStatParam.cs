namespace D2Data;

public class SignedStatParam : SignedStat
{
	public int Param;

	public SignedStatParam(BaseStat stat, int val, int param)
		: base(stat, val)
	{
		Param = param;
	}

	public override StatBase Clone()
	{
		return new SignedStatParam(BaseStat, Value, Param);
	}

	public override string ToString()
	{
		return $"{BaseStat}: {Value}, {Param}";
	}
}
