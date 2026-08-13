using MBNCSUtil;

namespace AGB.D2.Net.D2.BC;

public class NotifyJoin
{
	private string _ProductId = "D2XP";

	private Version _Version;

	private string _GameName;

	private string _GamePassword;

	public byte[] Data;

	public NotifyJoin(string GameName, string GamePassword)
	{
		_ProductId = "D2XP";
		_Version = Version.Default;
		_GameName = GameName;
		_GamePassword = GamePassword;
		Data = GetData();
	}

	public byte[] GetData()
	{
		BncsPacket myPacket = new BncsPacket(34);
		myPacket.InsertDwordString(_ProductId);
		myPacket.InsertInt32((int)_Version);
		myPacket.InsertCString(_GameName);
		myPacket.InsertCString(_GamePassword);
		return myPacket.GetData();
	}
}
