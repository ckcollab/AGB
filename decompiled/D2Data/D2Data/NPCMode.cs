namespace D2Data;

/// <summary>
/// NPC state type used by <see cref="!:GameServer.SetNPCMode" />.
/// <para>Not the same as MonMode...</para>
/// </summary>
public enum NPCMode
{
	Alive = 6,
	/// <summary>
	/// Null X and Y means the monster displays a 'in air' dying animation..
	/// </summary>
	Dying = 8,
	/// <summary>
	/// Dead monsters coming into view as well.
	/// </summary>
	Dead = 9
}
