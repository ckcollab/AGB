namespace D2Packets.RealmServer;

/// <summary>
/// Result of a realm connection startup.
/// <para>Used by <see cref="T:D2Packets.RealmServer.RealmStartupResponse" />.</para>
/// </summary>
public enum RealmStartupResult
{
	Success = 0,
	NoBattleNetConnection = 12,
	InvalidCDKey = 126,
	/// <summary>
	/// "Your connection has been temporarily restricted from this realm.
	/// Please try to log in at another time."
	/// </summary>
	TemporaryIPBan = 127
}
