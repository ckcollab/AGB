using System;

namespace D2Packets.BnetClient;

/// <summary>
/// Result of a game flags used by <see cref="T:D2Packets.BnetClient.StartGame" />.
/// </summary>
[Flags]
public enum StartGameFlags
{
	Public = 0,
	Private = 1
}
