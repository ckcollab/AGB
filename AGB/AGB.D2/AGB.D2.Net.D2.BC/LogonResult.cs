namespace AGB.D2.Net.D2.BC;

public enum LogonResult
{
	PassedChallenge = 0,
	OldGameVersion = 256,
	InvalidVersion = 257,
	VersionNeedDowngrade = 258,
	InvalidCdKey = 512,
	CdKeyInUse = 513,
	CdKeyBanned = 514,
	WrongProductVersion = 515,
	InvalidVersionCode = 4095,
	NOTSET = 3822,
	ERROR = 3549
}
