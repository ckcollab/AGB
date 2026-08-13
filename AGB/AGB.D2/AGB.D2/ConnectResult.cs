using D2Packets.BnetServer;

namespace AGB.D2;

public class ConnectResult
{
	public BnetConnectionResponse ConnectionResponse;

	public BnetAuthResponse AuthResponse;

	public bool HasFailed
	{
		get
		{
			if (ConnectionResponse == null)
			{
				return true;
			}
			if (AuthResponse == null || AuthResponse.Result != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool HasCompletedSuccessfully => AuthResponse != null && AuthResponse.Result == BnetAuthResult.Success;
}
