using System;
using ALAZ.SystemEx.NetEx.SocketsEx;

namespace AGB.Net;

public class ConnectionBase : BaseSocketService
{
	public delegate void PacketEventHandler(MessageEventArgs e);

	public delegate void ConnectionEventHandler(ConnectionEventArgs e);

	public PacketEventHandler Received;

	public PacketEventHandler Sent;

	public ConnectionEventHandler Connected;

	public ConnectionEventHandler Disconnected;

	public EventHandler<ExceptionEventArgs> ExceptionThrown;

	public override void OnConnected(ConnectionEventArgs e)
	{
		if (Connected != null)
		{
			Connected(e);
		}
		e.Connection.BeginReceive();
	}

	public override void OnReceived(MessageEventArgs e)
	{
		e.Connection.BeginReceive();
		if (Received != null)
		{
			Received(e);
		}
	}

	public override void OnSent(MessageEventArgs e)
	{
		if (Sent != null)
		{
			Sent(e);
		}
	}

	public override void OnDisconnected(ConnectionEventArgs e)
	{
		if (Disconnected != null)
		{
			Disconnected(e);
		}
	}

	public override void OnException(ExceptionEventArgs e)
	{
		if (ExceptionThrown != null)
		{
			ExceptionThrown(this, e);
		}
	}
}
