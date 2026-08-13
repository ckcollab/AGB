using D2Packets.GameServer;
using D2Packets.RealmServer;

namespace AGB.D2;

public class EnterGameResult
{
	public CreateGameResponse CreateGameResponse;

	public JoinGameResponse JoinGameResponse;

	public RequestLogonInfo RequestLogonInfo;

	public GameLogonReceipt GameLogonReceipt;

	public GameLogonSuccess GameLogonSuccess;

	public bool HasCompletedSuccessfully => GameLogonSuccess != null && GameLogonReceipt != null;

	public bool HasFailed
	{
		get
		{
			if (CreateGameResponse != null && CreateGameResponse.Result != 0)
			{
				return true;
			}
			if (JoinGameResponse != null && JoinGameResponse.Result != 0)
			{
				return true;
			}
			return false;
		}
	}

	public override string ToString()
	{
		string str = "";
		str += "CreateGameResponse = ";
		str = ((CreateGameResponse == null) ? (str + "null") : (str + CreateGameResponse.Result));
		str += "; JoinGameResponse = ";
		str = ((JoinGameResponse == null) ? (str + "null") : (str + JoinGameResponse.Result));
		str += "; RequestLogonInfo = ";
		str = ((RequestLogonInfo == null) ? (str + "null") : (str + RequestLogonInfo.ProtocolVersion));
		str += "; GameLogonReceipt = ";
		str = ((GameLogonReceipt == null) ? (str + "null") : (str + GameLogonReceipt.Difficulty));
		str += "; GameLogonSuccess = ";
		if (GameLogonSuccess != null)
		{
			return str + "Good";
		}
		return str + "null";
	}
}
