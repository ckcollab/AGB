namespace D2Packets.GameServer;

/// <summary>
/// <see cref="T:D2Packets.GameServer.InformationMessage" /> packet types.
/// </summary>
public enum InformationMessageType
{
	/// <summary>
	/// Player Has Dropped Due To Time Out
	/// </summary>
	DroppedFromGame = 0,
	/// <summary>
	/// Player Has Joined The Game
	/// </summary>
	JoinedGame = 2,
	/// <summary>
	/// Player Has Left The Game
	/// </summary>
	LeftGame = 3,
	/// <summary>
	/// Player Is Not In The Game (answer from @whisper command)
	/// The server will send you this packet after you have used the @charname command to whisper a player that is not in the game.
	/// </summary>
	NotInGame = 4,
	/// <summary>
	/// A Player Has Been Slained
	/// </summary>
	PlayerSlain = 6,
	/// <summary>
	/// Player To Player Relations
	/// The server will send you this packet to notify you of any changes that a D2 client would use to set up parties/hostile players/looting etc.
	/// This packet is a very important packet when it comes to party relations and should be the packet you take note of when adding party support into your bot.
	/// TEST: does this mean AboutPlayer etc are unreliable ??
	/// </summary>
	PlayerRelation = 7,
	/// <summary>
	/// #### Stones of Jordan Sold to Merchants
	/// </summary>
	SoJsSoldToMerchants = 17,
	DiabloWalksTheEarth = 18
}
