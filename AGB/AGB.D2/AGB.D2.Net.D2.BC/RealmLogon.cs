using MBNCSUtil;

namespace AGB.D2.Net.D2.BC;

public class RealmLogon
{
	private readonly uint _ClientToken;

	private readonly byte[] _HashedPassword;

	private readonly string _RealmName;

	public byte[] Data;

	public RealmLogon(string realm, uint clienttoken, uint servertoken)
	{
		_ClientToken = clienttoken;
		_RealmName = realm;
		_HashedPassword = OldAuth.DoubleHashPassword("password", clienttoken, servertoken);
		Data = Build();
	}

	private byte[] Build()
	{
		BncsPacket myPacket = new BncsPacket(62);
		myPacket.InsertUInt32(_ClientToken);
		myPacket.InsertByteArray(_HashedPassword);
		myPacket.InsertCString(_RealmName);
		return myPacket.GetData();
	}
}
