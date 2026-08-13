namespace D2Packets.BnetServer;

/// <summary>
/// Result of a Bnet logon request.
/// <para>Used by <see cref="T:D2Packets.BnetServer.BnetLogonResponse" />.</para>
/// </summary>
public enum BnetLogonResult
{
	Success,
	AccountDoesNotExist,
	PasswordIncorrect
}
