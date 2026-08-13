namespace D2Data;

public enum PlayerRelationActionType
{
	/// <summary>
	/// Player has removed Hostile
	/// </summary>
	Unhostile = 4,
	/// <summary>
	/// Player Is Asking To Party With You
	/// </summary>
	InvitedYou = 5,
	/// <summary>
	/// New Player Has Joined Party
	/// </summary>
	JoinedParty = 7,
	/// <summary>
	/// Your Now In This Players party ?
	/// </summary>
	JoinedYourParty = 8,
	/// <summary>
	/// Player has given you permission to loot his body
	/// </summary>
	AllowLoot = 2,
	/// <summary>
	/// Player has gone hostile
	/// </summary>
	Hostile = 3,
	/// <summary>
	/// Player has left your party
	/// </summary>
	LeftParty = 9,
	/// <summary>
	/// Player has canlceled the party invitation
	/// </summary>
	CanceledInvite = 6,
	/// <summary>
	/// Player has canceled permission to loot his body
	/// </summary>
	CanceledLootPermission = 11,
	NotApplicable = 255
}
