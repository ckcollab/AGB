using AGB.Net;

namespace AGB.D2;

internal class ProxyInstance : Proxy
{
	public int Instances = 0;

	public int AllowedInstances = 4;

	public ProxyInstance(string proxy, int port)
	{
		IP = proxy;
		Port = port;
	}

	public ProxyInstance(string proxy, int port, string username, string password)
	{
		IP = proxy;
		Port = port;
		Username = username;
		Password = password;
	}
}
