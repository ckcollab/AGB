namespace D2Data;

public class ReanimateStat : UnsignedStat
{
	public readonly NPCClass Monster;

	public ReanimateStat(BaseStat stat, uint monster, uint val)
		: base(stat, val)
	{
		Monster = (NPCClass)monster;
	}

	public ReanimateStat(BaseStat stat, NPCClass monster, uint val)
		: base(stat, val)
	{
		Monster = monster;
	}

	public override StatBase Clone()
	{
		return new ReanimateStat(BaseStat, Monster, Value);
	}

	public override string ToString()
	{
		return $"{Value}% Reanimate As: {Monster}";
	}
}
