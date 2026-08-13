using System;
using System.Net;
using System.Threading;
using AGB.D2.Net.Packets;
using AGB.Net;
using ALAZ.SystemEx.NetEx.SocketsEx;
using D2Data;

namespace AGB.D2.Net;

public class AgbSocket
{
	public static readonly AgbSocket Instance = new AgbSocket();

	public ISocketConnection Connection;

	private PacketBuffer PacketBuffer;

	public AutoResetEvent WaitForConnection;

	public bool IsConnected;

	public PacketHandler<AGBPacket> PacketHandler;

	public string UserName;

	public event ConnectionStateChangeEvent Disconnected;

	private AgbSocket()
	{
		PacketHandler = new PacketHandler<AGBPacket>(Enum.GetNames(typeof(PacketType)).Length);
		PacketBuffer = new PacketBuffer();
		IsConnected = false;
		WaitForConnection = new AutoResetEvent(initialState: false);
		UserName = "";
	}

	public bool Connect(string ip, int port)
	{
		if (IsConnected)
		{
			return true;
		}
		ConnectionBase baseConnection = new ConnectionBase();
		baseConnection.Connected = (ConnectionBase.ConnectionEventHandler)Delegate.Combine(baseConnection.Connected, new ConnectionBase.ConnectionEventHandler(AGB_Connected));
		baseConnection.Sent = (ConnectionBase.PacketEventHandler)Delegate.Combine(baseConnection.Sent, new ConnectionBase.PacketEventHandler(AGB_PacketSent));
		baseConnection.Received = (ConnectionBase.PacketEventHandler)Delegate.Combine(baseConnection.Received, new ConnectionBase.PacketEventHandler(AGB_PacketReceived));
		baseConnection.Disconnected = (ConnectionBase.ConnectionEventHandler)Delegate.Combine(baseConnection.Disconnected, new ConnectionBase.ConnectionEventHandler(AGB_Disconnected));
		baseConnection.ExceptionThrown = (EventHandler<ExceptionEventArgs>)Delegate.Combine(baseConnection.ExceptionThrown, new EventHandler<ExceptionEventArgs>(AGB_ExceptionThrown));
		SocketClient client = new SocketClient(baseConnection);
		client.SocketBufferSize = 4096;
		client.MessageBufferSize = 16384;
		SocketConnector connector = client.AddConnector("Agb Client", new IPEndPoint(IPAddress.Parse(ip), port));
		connector.CompressionType = CompressionType.ctNone;
		connector.EncryptType = EncryptType.etNone;
		connector.ReconnectAttempts = 2;
		connector.ReconnectAttemptInterval = 5000;
		client.Start();
		IsConnected = WaitForConnection.WaitOne(15000, exitContext: false);
		return IsConnected;
	}

	private void AGB_Connected(ConnectionEventArgs e)
	{
		IsConnected = true;
		Connection = e.Connection;
		WaitForConnection.Set();
	}

	private void AGB_PacketSent(MessageEventArgs e)
	{
		PacketHandler.AddPacket(e.Buffer[0], PacketSenderType.Client, AGBPacket.Parse(e.Buffer));
	}

	private void AGB_PacketReceived(MessageEventArgs e)
	{
		lock (PacketBuffer)
		{
			if (e.Buffer.Length > 0)
			{
				PacketBuffer.Enqueue(e.Buffer, e.Buffer.Length);
			}
			if (PacketBuffer.Count <= 4)
			{
				return;
			}
			byte[] dequeuedData = PacketBuffer.ToArray();
			int length = BitConverter.ToInt32(dequeuedData, 1);
			if (length <= dequeuedData.Length)
			{
				dequeuedData = PacketBuffer.Dequeue(length);
				PacketHandler.AddPacket(dequeuedData[0], PacketSenderType.Server, AGBPacket.Parse(dequeuedData));
				if (PacketBuffer.Count > 0)
				{
					AGB_PacketReceived(new MessageEventArgs(Connection, new byte[0], sentByServer: true));
				}
			}
		}
	}

	private void AGB_Disconnected(ConnectionEventArgs e)
	{
		IsConnected = false;
		if (this.Disconnected != null)
		{
			this.Disconnected();
		}
	}

	private void AGB_ExceptionThrown(object sender, ExceptionEventArgs e)
	{
		string message = e.Exception.Message;
		if (message != null && message == "An existing connection was forcibly closed by the remote host")
		{
			AGB_Disconnected(null);
		}
	}

	public WelcomeResult Welcome(int timeout)
	{
		Connection.BeginSend(AGBPacket.Construct(new Welcome()));
		WelcomeResult result = (WelcomeResult)PacketHandler.WaitForPacket(1, timeout);
		if (result == null)
		{
			return null;
		}
		return result;
	}

	public LoginResult Login(string username, string password, int timeout)
	{
		Connection.BeginSend(AGBPacket.Construct(new Login(username, password)));
		LoginResult result = (LoginResult)PacketHandler.WaitForPacket(3, timeout);
		if (result == null)
		{
			return null;
		}
		UserName = username;
		return result;
	}

	public SetNewGameInfoResult SetNewGameInfo(Character character, int seed, int gameHash, GameDifficulty difficulty, int timeout)
	{
		Connection.BeginSend(AGBPacket.Construct(new SetNewGameInfo(character, seed, gameHash, difficulty)));
		SetNewGameInfoResult result = (SetNewGameInfoResult)PacketHandler.WaitForPacket(5, timeout);
		if (result == null)
		{
			return null;
		}
		return result;
	}

	public GetMapResult GetMap(Character character, AreaLevel areaLevel, int timeout)
	{
		Connection.BeginSend(AGBPacket.Construct(new GetMap(character, areaLevel)));
		GetMapResult result = (GetMapResult)PacketHandler.WaitForPacket(9, timeout);
		if (result == null)
		{
			return null;
		}
		result.Map.LoadCollisions();
		return result;
	}

	public void Message(string userName, Character sender, Character receiver, string message)
	{
		Connection.BeginSend(AGBPacket.Construct(new Message(userName, sender, receiver, message)));
	}

	public bool Ping(int timeOut)
	{
		Connection.BeginSend(AGBPacket.Construct(new Ping()));
		return PacketHandler.WaitForPacket(12, timeOut) != null;
	}

	public void Quit()
	{
		if (Connection != null)
		{
			Connection.BeginSend(AGBPacket.Construct(new Quit()));
			Connection.BeginDisconnect();
		}
	}
}
