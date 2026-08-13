using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Readonly information 
/// </summary>
public struct BaseSkillLevel
{
	public readonly SkillType Skill;

	public readonly int Level;

	public BaseSkillLevel(int skill, int level)
	{
		Skill = (SkillType)skill;
		Level = level;
	}

	public BaseSkillLevel(SkillType skill, int level)
	{
		Skill = skill;
		Level = level;
	}

	public override string ToString()
	{
		return $"{Skill}: {Level}";
	}
}
