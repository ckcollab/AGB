using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace D2Data;

public class Skills : Collection<SkillInfo>
{
	public SkillInfo this[SkillType skill]
	{
		get
		{
			using (IEnumerator<SkillInfo> enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SkillInfo current = enumerator.Current;
					if (current.Skill == skill)
					{
						return current;
					}
				}
			}
			return null;
		}
	}

	public void SetBaseSkillLevel(SkillType skill, int level)
	{
		using (IEnumerator<SkillInfo> enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				SkillInfo current = enumerator.Current;
				if (current.Skill == skill && current.ChargedItem == uint.MaxValue)
				{
					current.BaseLevel = level;
					return;
				}
			}
		}
		Add(new SkillInfo(skill, level));
	}

	public void Remove(SkillType skill)
	{
		for (int i = 0; i < base.Count; i++)
		{
			if (base[i].Skill == skill && base[i].ChargedItem == uint.MaxValue)
			{
				RemoveAt(i);
				break;
			}
		}
	}

	public void Remove(uint chargedItem)
	{
		for (int i = 0; i < base.Count; i++)
		{
			if (base[i].ChargedItem == chargedItem)
			{
				RemoveAt(i);
				break;
			}
		}
	}
}
