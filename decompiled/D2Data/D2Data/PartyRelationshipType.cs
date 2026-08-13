using System;

namespace D2Data;

[Flags]
public enum PartyRelationshipType
{
	/// <summary>
	/// Player is not in any party or wanting to party
	/// </summary>
	None = 0,
	/// <summary>
	/// Player is in a party
	/// </summary>
	InAParty = 1,
	/// <summary>
	/// Player is waiting for you to accept invitation
	/// </summary>
	InvitedYou = 2,
	/// <summary>
	/// Waiting for player to accept invitation
	/// </summary>
	Invited = 4
}
