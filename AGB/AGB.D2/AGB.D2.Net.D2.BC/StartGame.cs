using MBNCSUtil;

namespace AGB.D2.Net.D2.BC;

public class StartGame
{
	private readonly byte _Flag;

	private readonly string _GameName;

	private readonly string _GamePassword;

	public byte[] Data;

	public StartGame(string GameName, string GamePassword)
	{
		_GameName = GameName;
		_GamePassword = GamePassword;
		_Flag = (byte)((!(GamePassword == "")) ? 1 : 0);
		Data = Build();
	}

	private byte[] Build()
	{
		BncsPacket myPacket = new BncsPacket(28);
		myPacket.InsertByte(_Flag);
		myPacket.InsertByteArray(new byte[19]);
		myPacket.InsertCString(_GameName);
		myPacket.InsertCString(_GamePassword);
		myPacket.InsertByte(0);
		return myPacket.GetData();
	}
}
