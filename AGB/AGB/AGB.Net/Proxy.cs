namespace AGB.Net;

public class Proxy
{
	public string IP;

	public int Port;

	public string Username;

	public string Password;

	public override string ToString()
	{
		string toReturn = "";
		if (Username != null)
		{
			string text = toReturn;
			toReturn = text + Username + ":" + Password + "@";
		}
		object obj = toReturn;
		return string.Concat(obj, IP, ":", Port);
	}
}
