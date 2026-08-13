namespace D2Packets.GameServer;

/// <summary>
/// Event type of item triggered skill used by <see cref="T:D2Packets.GameServer.ItemTriggerSkill" />.
/// TODO: test StatType.SkillOnKill, StatType.SkillOnDeath and StatType.SkillOnLevelUp
/// </summary>
public enum ItemEventCause
{
	/// <summary>
	/// StatType.SkillOnStriking, StatType.SkillOnAttack (and StatType.SkillOnKill ?)
	/// </summary>
	Target,
	/// <summary>
	/// StatType.SkillOnGetHit
	/// </summary>
	Owner
}
