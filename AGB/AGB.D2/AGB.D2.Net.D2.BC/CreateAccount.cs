using MBNCSUtil;

namespace AGB.D2.Net.D2.BC;

public class CreateAccount
{
	private string Username;

	private string Password;

	public byte[] Data;

	public CreateAccount(string username, string password)
	{
		Username = username;
		Password = password;
		Data = Build();
	}

	private byte[] Build()
	{
		BncsPacket myPacket = new BncsPacket(61);
		myPacket.InsertByteArray(OldAuth.HashPassword(Password));
		myPacket.InsertCString(Username);
		return myPacket.GetData();
	}
}
