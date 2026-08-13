using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace D2Data;

public class Stats : Collection<StatBase>
{
	public StatBase this[StatType stat]
	{
		get
		{
			using (IEnumerator<StatBase> enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StatBase current = enumerator.Current;
					if (current.BaseStat.Type == stat)
					{
						return current;
					}
				}
			}
			return null;
		}
	}

	public Stats()
	{
	}

	public Stats(IList<StatBase> stats)
	{
		for (int i = 0; i < stats.Count; i++)
		{
			Add(stats[i]);
		}
	}

	public void Set(StatBase stat)
	{
		for (int i = 0; i < base.Count; i++)
		{
			if (base[i].BaseStat.Type == stat.BaseStat.Type)
			{
				base[i] = stat;
				return;
			}
		}
		Add(stat);
	}

	public void Merge(StatBase stat)
	{
		if (stat.BaseStat.Type == StatType.ChargedSkill || stat.BaseStat.Type == StatType.Reanimate || stat.BaseStat.Type == StatType.SkillOnAttack || stat.BaseStat.Type == StatType.SkillOnKill || stat.BaseStat.Type == StatType.SkillOnDeath || stat.BaseStat.Type == StatType.SkillOnStriking || stat.BaseStat.Type == StatType.SkillOnLevelUp || stat.BaseStat.Type == StatType.SkillOnGetHit)
		{
			Add(stat.Clone());
			return;
		}
		for (int i = 0; i < base.Count; i++)
		{
			if (base[i].BaseStat.Type != stat.BaseStat.Type)
			{
				continue;
			}
			if (stat.BaseStat.Type == StatType.ElementalSkillBonus)
			{
				if ((base[i] as ElementalSkillsBonusStat).Element == (stat as ElementalSkillsBonusStat).Element)
				{
					(base[i] as ElementalSkillsBonusStat).Value += (stat as ElementalSkillsBonusStat).Value;
					return;
				}
				continue;
			}
			if (stat.BaseStat.Type == StatType.ClassSkillsBonus)
			{
				if ((base[i] as ClassSkillsBonusStat).Class == (stat as ClassSkillsBonusStat).Class)
				{
					(base[i] as ClassSkillsBonusStat).Value += (stat as ClassSkillsBonusStat).Value;
					return;
				}
				continue;
			}
			if (stat.BaseStat.Type == StatType.Aura)
			{
				if ((base[i] as AuraStat).Skill == (stat as AuraStat).Skill)
				{
					if ((base[i] as AuraStat).Value < (stat as AuraStat).Value)
					{
						base[i] = stat.Clone();
					}
					return;
				}
				continue;
			}
			if (stat is SkillBonusStat)
			{
				if ((base[i] as SkillBonusStat).Skill == (stat as SkillBonusStat).Skill)
				{
					(base[i] as SkillBonusStat).Value += (stat as SkillBonusStat).Value;
					return;
				}
				continue;
			}
			if (stat.BaseStat.Type == StatType.SkillTabBonus)
			{
				if ((base[i] as SkillTabBonusStat).Tab == (stat as SkillTabBonusStat).Tab)
				{
					(base[i] as SkillTabBonusStat).Value += (stat as SkillTabBonusStat).Value;
					return;
				}
				continue;
			}
			if (stat is SignedStat)
			{
				(base[i] as SignedStat).Value += (stat as SignedStat).Value;
			}
			else if (stat is UnsignedStat)
			{
				(base[i] as UnsignedStat).Value += (stat as UnsignedStat).Value;
			}
			return;
		}
		Add(stat.Clone());
	}

	public void Remove(StatType stat)
	{
		for (int i = 0; i < base.Count; i++)
		{
			if (base[i].BaseStat.Type == stat)
			{
				RemoveAt(i);
				break;
			}
		}
	}
}
