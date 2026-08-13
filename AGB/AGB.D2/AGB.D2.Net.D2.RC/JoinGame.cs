namespace AGB.D2.Net.D2.RC;

public class JoinGame : BasePacket
{
	private readonly ushort GameNumber;

	private readonly string GameName;

	private readonly string GamePassword;

	public byte[] Data;

	public JoinGame(ushort gameNumber, string name, string password)
		: base(4)
	{
		GameNumber = gameNumber;
		GameName = name;
		GamePassword = password;
		Data = GetData();
	}

	public override byte[] GetData()
	{
		InsertUInt16(GameNumber);
		InsertCString(GameName);
		InsertCString(GamePassword);
		return base.GetData();
	}
}
