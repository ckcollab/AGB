namespace D2Data;

/// <summary>
/// Notifies of an update to item related interface. Used by GS packet 0x77
/// </summary>
public enum ItemUIAction : byte
{
	/// <summary>
	/// Requesting to trade with someone (result of interacting with a player who isn't "busy").
	/// </summary>
	RequestTrade = 0,
	/// <summary>
	/// Another player is requesting to trade with you.
	/// </summary>
	TradeRequest = 1,
	/// <summary>
	/// Trade partner has accepted your trade offer (Acceot button goes green.)
	/// </summary>
	TradeAccepted = 5,
	/// <summary>
	/// Unchecks all Accept buttons - sent whenever a trader changes anything in trade offer.
	/// Or might do so soon... e.g. picks up an item from inventory, clicks the offer gold button...
	/// </summary>
	UnacceptAllTrades = 6,
	/// <summary>
	/// You don't have enough room to trade, sent instead of TradeCompleted when both accept but trade can't be performed.
	/// </summary>
	NoRoomForTrade = 9,
	/// <summary>
	/// Trade partner doesn't have enough room to trade, sent instead of TradeCompleted when both accept but trade can't be performed.
	/// </summary>
	TraderHasNoRoom = 10,
	/// <summary>
	/// A player refuses trade request or cancels trade before it is completed.
	/// In the later case, this resets inventory / equipment to what they were before trade and closes the trade window.
	/// </summary>
	RefuseTrade = 12,
	/// <summary>
	/// Both player have accepted trade and completion is successful. Trade window closes and each player get the other player's trade buffer.
	/// </summary>
	TradeCompleted = 13,
	/// <summary>
	/// This turns the Accept button red, making it unclickable for a certain duration (until EnableAcceptTrade is sent.)
	/// </summary>
	DisableAcceptTrade = 14,
	/// <summary>
	/// Enables the Accept trade button.
	/// </summary>
	EnableAcceptTrade = 15,
	/// <summary>
	/// Open the stash panel.
	/// </summary>
	OpenStash = 16,
	/// <summary>
	/// Hide the stash panel with another panel, e.g. the Horadric cube panel.
	/// The stash remains open and should (but doesn't always) get redisplayed when the hiding UI is closed...
	/// </summary>
	HideStash = 17,
	/// <summary>
	/// Open the Horadric cube panel.
	/// </summary>
	OpenCube = 21
}
