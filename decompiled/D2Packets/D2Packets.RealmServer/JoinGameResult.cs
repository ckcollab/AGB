namespace D2Packets.RealmServer;

/// <summary>
/// Result of a game join request.
/// <para>Used by <see cref="T:D2Packets.RealmServer.JoinGameResponse" />.</para>
/// </summary>
public enum JoinGameResult
{
	/// <summary>
	/// Terminate the connection with the Realm Server and initiate with Game Server.
	/// </summary>
	Success = 0,
	PasswordIncorrect = 41,
	GameDoesNotExist = 42,
	GameFull = 43,
	/// <summary>
	/// You do not meet the level requirements for this game.
	/// </summary>
	LevelRequirementsNotMet = 44,
	/// <summary>
	/// A dead hardcore character cannot join a game.
	/// </summary>
	DeadHardcoreCharacter = 110,
	/// <summary>
	/// A non-hardcore character cannot join a game created by a hardcore character.
	/// </summary>
	UnableToJoinHardcoreGame = 113,
	UnableToJoinNightmareGame = 115,
	UnableToJoinHellGame = 116,
	/// <summary>
	/// A non-expansion character cannot join a game created by an expansion character.
	/// </summary>
	UnableToJoinExpansionGame = 120,
	/// <summary>
	/// An expansion character cannot join a game created by a non-expansion character.
	/// </summary>
	UnableToJoinClassicGame = 121,
	/// <summary>
	/// A non-ladder character cannot join a game created by a Ladder character.
	/// </summary>
	UnableToJoinLadderGame = 125
}
