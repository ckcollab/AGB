using System;

namespace D2Data;

public class PoisonDamageStat : DamageRangeStat
{
	public readonly int Frames;

	public readonly double Seconds;

	public double MinPerSecond => (double)base.Min / 10.25;

	public double MaxPerSecond => (double)base.Max / 10.25;

	public PoisonDamageStat(BaseStat stat, int min, int max, int frames)
		: base(stat, min, max)
	{
		Frames = frames;
		Seconds = (double)frames / 25.0;
	}

	public override StatBase Clone()
	{
		return new PoisonDamageStat(BaseStat, Value, Param, Frames);
	}

	public override string ToString()
	{
		if (base.Min == base.Max)
		{
			return string.Format("+{1} Poison Damage over {0} seconds", Seconds, Math.Floor(MinPerSecond * Seconds));
		}
		return string.Format("+{1}-{2} Poison Damage over {0} seconds", Seconds, Math.Floor(MinPerSecond * Seconds) + 1.0, Math.Floor(MaxPerSecond * Seconds));
	}
}
