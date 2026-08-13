using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x01 - Game Logon Receipt
/// <para>Acknowledgement of a valid game logon (join) request.</para>
/// <para>The join can still fail after this...</para>
/// </summary>
/// <remarks>
/// Part of logon sequence
/// <para>Response to: <see cref="T:D2Packets.GameClient.GameLogonRequest" /></para>
/// <para>Next packet: <see cref="T:D2Packets.GameServer.GameLoading" /></para>
/// </remarks>
public class GameLogonReceipt : GSPacket
{
	protected GameDifficulty difficulty;

	protected byte unknown2;

	protected bool hardcore;

	protected bool expansion;

	protected bool ladder;

	public GameDifficulty Difficulty => difficulty;

	public bool Hardcore => hardcore;

	public bool Expansion => expansion;

	public bool Ladder => ladder;

	/// <summary>
	/// A version of some kind?
	/// </summary>
	public byte Unknown2 => unknown2;

	public GameLogonReceipt(byte[] data)
		: base(data)
	{
		difficulty = (GameDifficulty)data[1];
		unknown2 = data[2];
		hardcore = (data[3] & 8) == 8;
		expansion = data[6] == 1;
		ladder = data[7] == 1;
	}
}
