using System;

namespace D2Packets.RealmServer;

[Flags]
public enum GameFlags : uint
{
	/// <summary>
	/// If not present, the packet contains no useful information.
	/// <para>For <see cref="T:D2Packets.RealmServer.GameList" />, this means the packet marks the end of listing.</para>
	/// <para>For <see cref="T:D2Packets.RealmServer.GameInfo" />, the game was destroyed since listed !?</para>
	/// </summary>
	Valid = 4u,
	Hardcore = 0x800u,
	Nightmare = 0x1000u,
	Hell = 0x2000u,
	Empty = 0x20000u,
	Expansion = 0x100000u,
	Ladder = 0x200000u,
	ServerDown = uint.MaxValue,
	GameDestroyed = 0xFFFFFFFEu
}
