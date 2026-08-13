using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ALAZ.SystemEx.NetEx.SocketsEx;

namespace AGB.Net;

public class Socket
{
	private ISocketConnection Connection;

	private AutoResetEvent WaitForConnection;

	public ConnectionBase ConnectionBase;

	public bool IsConnected;

	public Socket()
	{
		IsConnected = false;
		WaitForConnection = new AutoResetEvent(initialState: false);
		ConnectionBase = new ConnectionBase();
		ConnectionBase connectionBase = ConnectionBase;
		connectionBase.Connected = (ConnectionBase.ConnectionEventHandler)Delegate.Combine(connectionBase.Connected, new ConnectionBase.ConnectionEventHandler(Connected));
		ConnectionBase connectionBase2 = ConnectionBase;
		connectionBase2.Disconnected = (ConnectionBase.ConnectionEventHandler)Delegate.Combine(connectionBase2.Disconnected, new ConnectionBase.ConnectionEventHandler(Disconnected));
		ConnectionBase connectionBase3 = ConnectionBase;
		connectionBase3.ExceptionThrown = (EventHandler<ExceptionEventArgs>)Delegate.Combine(connectionBase3.ExceptionThrown, new EventHandler<ExceptionEventArgs>(ExceptionThrown));
	}

	public bool Connect(string ip, int port)
	{
		return Connect(new IPEndPoint(Dns.Resolve(ip).AddressList[0], port));
	}

	public bool Connect(IPAddress ip, int port)
	{
		return Connect(new IPEndPoint(ip, port));
	}

	public bool Connect(IPEndPoint endPoint)
	{
		if (IsConnected)
		{
			return true;
		}
		SocketClient client = new SocketClient(ConnectionBase);
		client.SocketBufferSize = 4096;
		client.MessageBufferSize = 16384;
		SocketConnector connector = client.AddConnector("Agb Client", endPoint);
		connector.CompressionType = CompressionType.ctNone;
		connector.EncryptType = EncryptType.etNone;
		connector.ReconnectAttempts = 2;
		connector.ReconnectAttemptInterval = 5000;
		client.Start();
		IsConnected = WaitForConnection.WaitOne(15000, exitContext: false);
		return IsConnected;
	}

	private void Connected(ConnectionEventArgs e)
	{
		IsConnected = true;
		Connection = e.Connection;
		WaitForConnection.Set();
	}

	private void Disconnected(ConnectionEventArgs e)
	{
		IsConnected = false;
	}

	private void ExceptionThrown(object sender, ExceptionEventArgs e)
	{
		string message = e.Exception.Message;
		if (message != null && message == "An existing connection was forcibly closed by the remote host" && ConnectionBase.Disconnected != null)
		{
			ConnectionBase.Disconnected(null);
		}
	}

	public void Send(byte[] data)
	{
		if (Connection == null)
		{
			return;
		}
		try
		{
			Connection.BeginSend(data);
		}
		catch (SocketException)
		{
		}
	}

	public void Close()
	{
		if (Connection != null)
		{
			try
			{
				Connection.BeginDisconnect();
			}
			catch (NullReferenceException)
			{
			}
		}
	}
}
