namespace D2Packets.BnetServer;

/// <summary>
/// Result of a Bnet connection authentication.
/// <para>Used by <see cref="T:D2Packets.BnetServer.BnetAuthResponse" />.</para>
/// <para>If value is the version code supplied in <see cref="T:D2Packets.BnetClient.BnetAuthRequest" />, the code is invalid.</para>
/// </summary>
public enum BnetAuthResult
{
	/// <summary>
	/// Passed challenge sucessfully.
	/// </summary>
	Success = 0,
	/// <summary>
	/// Additional info field supplies patch MPQ filename.
	/// </summary>
	OldVersion = 256,
	InvalidVersion = 257,
	/// <summary>
	/// Game must be downgraded. Additional info field supplies patch MPQ filename.
	/// </summary>
	BuggedVersion = 258,
	InvalidCDKey = 512,
	/// <summary>
	/// Additional info field supplies name of user.
	/// </summary>
	CDKeyInUse = 513,
	BannedCDKey = 514,
	WrongProduct = 515
}
