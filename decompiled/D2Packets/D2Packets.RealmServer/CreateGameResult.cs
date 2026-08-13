namespace D2Packets.RealmServer;

/// <summary>
/// Result of a game creation.
/// <para>Used by <see cref="T:D2Packets.RealmServer.CreateGameResponse" />.</para>
/// </summary>
public enum CreateGameResult : uint
{
	Success = 0u,
	InvalidGameName = 30u,
	GameAlreadyExists = 31u,
	DeadHardcoreCharacter = 110u
}
