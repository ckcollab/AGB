using D2Packets.BnetServer;
using D2Packets.RealmServer;

namespace AGB.D2;

public class RealmConnectResult
{
	public QueryRealmsResponse QueryRealmsResponse;

	public RealmLogonResponse RealmLogonResponse;

	public CharacterLogonResponse CharacterLogonResponse;

	public RealmStartupResponse RealmStartupResponse;

	public bool HasCompletedSuccessfully => CharacterLogonResponse != null && CharacterLogonResponse.Result == RealmCharacterActionResult.Success;

	public bool HasFailed
	{
		get
		{
			if (RealmLogonResponse != null && RealmLogonResponse.Result != 0)
			{
				return true;
			}
			if (CharacterLogonResponse != null && CharacterLogonResponse.Result != 0)
			{
				return true;
			}
			if (RealmStartupResponse != null && RealmStartupResponse.Result != 0)
			{
				return true;
			}
			return false;
		}
	}

	public override string ToString()
	{
		string str = "";
		str += "QueryRealmsResponse = ";
		str = ((QueryRealmsResponse == null) ? (str + "null") : (str + QueryRealmsResponse.Realms[0]));
		str += "; RealmLogonResponse = ";
		str = ((RealmLogonResponse == null) ? (str + "null") : (str + RealmLogonResponse.Result));
		str += "; CharacterLogonResponse = ";
		str = ((CharacterLogonResponse == null) ? (str + "null") : (str + CharacterLogonResponse.Result));
		str += "; RealmStartupResponse = ";
		if (RealmStartupResponse != null)
		{
			return str + RealmStartupResponse.Result;
		}
		return str + "null";
	}
}
