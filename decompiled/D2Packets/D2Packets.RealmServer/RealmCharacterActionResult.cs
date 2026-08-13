namespace D2Packets.RealmServer;

/// <summary>
/// Result of various realm actions on a character.
/// <para>Used by <see cref="T:D2Packets.RealmServer.CharacterCreationResponse" />, 
/// <see cref="T:D2Packets.RealmServer.CharacterLogonResponse" />, 
/// <see cref="T:D2Packets.RealmServer.CharacterDeletionResponse" /> and 
/// <see cref="T:D2Packets.RealmServer.CharacterUpgradeResponse" />.</para>
/// </summary>
public enum RealmCharacterActionResult
{
	/// <summary>
	/// The action was completed successfully.
	/// </summary>
	Success = 0,
	/// <summary>
	/// Character already exists or account already has maximum number of characters (8).
	/// </summary>
	CharacterOverlap = 20,
	/// <summary>
	/// Character name is longer than 15 characters or contains illegal characters.
	/// </summary>
	InvalidCharacterName = 21,
	/// <summary>
	/// Invalid character name specified for action.
	/// </summary>
	CharacterNotFound = 70,
	/// <summary>
	/// Invalid character name specified for deletion.
	/// </summary>
	CharacterDoesNotExist = 73,
	/// <summary>
	/// The action (logon, upgrade, etc.) has failed for an unspecified reason.
	/// </summary>
	Failed = 122,
	/// <summary>
	/// All actions except delete are invalid on an expired character.
	/// </summary>
	CharacterExpired = 123,
	/// <summary>
	/// When trying to upgrade an expansion character.
	/// </summary>
	CharacterAlreadyUpgraded = 124
}
