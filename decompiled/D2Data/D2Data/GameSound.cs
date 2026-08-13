namespace D2Data;

public enum GameSound
{
	None = -1,
	/// <summary>
	/// Item specific sound triggered when an item is swap or picked up.
	/// </summary>
	Pickup = 1,
	/// <summary>
	/// Masochistic Baal has a good laugh because he's one step closer to getting his ass kicked.
	/// </summary>
	BaalLaugh = 16,
	/// <summary>
	/// Cannot perform action (pick up item with telekinesis or when there's not enough space in inventory).
	/// </summary>
	Impossible = 19,
	/// <summary>
	/// User triggered sound 1
	/// </summary>
	Help = 25,
	/// <summary>
	/// User triggered sound 2
	/// </summary>
	FollowMe = 26,
	/// <summary>
	/// User triggered sound 3
	/// </summary>
	ThisIsForYou = 27,
	/// <summary>
	/// User triggered sound 4
	/// </summary>
	Thanks = 28,
	/// <summary>
	/// User triggered sound 5
	/// </summary>
	Sorry = 29,
	/// <summary>
	/// User triggered sound 6
	/// </summary>
	Bye = 30,
	/// <summary>
	/// User triggered sound 7
	/// </summary>
	NowYouDie = 31,
	/// <summary>
	/// User triggered sound 8
	/// </summary>
	Retreat = 32,
	/// <summary>
	/// Mercenary thanking you for equiping it with an item.
	/// </summary>
	IllPutThatToGoodUse = 87
}
