using MBNCSUtil;

namespace AGB.D2.Net.D2.BC;

public class LogonRequest
{
	private uint _ClientToken;

	private uint _ServerToken;

	private byte[] _HashedPassword;

	private string _AccountName;

	public byte[] Data;

	public LogonRequest(string AccountName, string AccountPassword, uint ClientToken, uint ServerToken)
	{
		_ClientToken = ClientToken;
		_ServerToken = ServerToken;
		_HashedPassword = OldAuth.DoubleHashPassword(AccountPassword, _ClientToken, _ServerToken);
		_AccountName = AccountName;
		Data = Build();
	}

	private byte[] Build()
	{
		BncsPacket myPacket = new BncsPacket(58);
		myPacket.InsertUInt32(_ClientToken);
		myPacket.InsertUInt32(_ServerToken);
		myPacket.InsertByteArray(_HashedPassword);
		myPacket.InsertCString(_AccountName);
		return myPacket.GetData();
	}
}
