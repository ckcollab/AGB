namespace D2Packets.GameServer;

/// <summary>
/// Type of transaction with a dealer used by <see cref="T:D2Packets.GameServer.TransactionComplete" />.
/// </summary>
public enum TransactionType
{
	Hire = 0,
	Repair = 1,
	Sell = 3,
	Buy = 4,
	ToStack = 5
}
