namespace D2Packets.BnetServer;

/// <summary>
/// Error code of a realm connection startup attempt.
/// <para>Used by <see cref="T:D2Packets.BnetServer.RealmLogonResponse" />.</para>
/// </summary>
public enum RealmLogonResult : uint
{
	Success = 0u,
	RealmUnavailable = 2147483649u,
	LogonFailed = 2147483650u
}
