namespace D2Data;

public class ReplenishStat : SignedStat
{
	public readonly int Seconds;

	public ReplenishStat(BaseStat stat, int val)
		: base(stat, val)
	{
		Seconds = 100 / val;
	}

	public override StatBase Clone()
	{
		return new ReplenishStat(BaseStat, Value);
	}

	public override string ToString()
	{
		if (BaseStat.Type == StatType.ReplenishQuantity)
		{
			return $"Replenishes Quantity (1 in {Seconds} seconds)";
		}
		return $"Repairs 1 Durability in {Seconds} seconds";
	}
}
