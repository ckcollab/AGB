using System;

namespace D2Data;

[Flags]
public enum GameQuestState
{
	/// <summary>
	/// The creator had not completed the quest and it has not been done in this game yet.
	/// </summary>
	Open = 0,
	/// <summary>
	/// The creator has already completed this quest.
	/// </summary>
	Closed = 0x8000,
	/// <summary>
	/// The quest has already been completed in game.
	/// </summary>
	Completed = 0x2000
}
