using System;

namespace D2Data;

public class ColdDamageStat : DamageRangeStat
{
	public readonly int Frames;

	public readonly double Seconds;

	public ColdDamageStat(BaseStat stat, int min, int max, int frames)
		: base(stat, min, max)
	{
		Frames = frames;
		Seconds = (double)frames / 25.0;
	}

	public override StatBase Clone()
	{
		return new ColdDamageStat(BaseStat, Value, Param, Frames);
	}

	public override string ToString()
	{
		if (base.Min == base.Max)
		{
			return $"+{base.Min} Cold Damage (Chills {Math.Round(Seconds, 2)} seconds)";
		}
		return $"+{base.Min}-{base.Max} Cold Damage (Chills {Math.Round(Seconds, 2)} seconds)";
	}
}
