using System;
using System.Collections.Generic;
using System.Threading;
using AGB.D2.Net.Packets;
using AGB.Net;
using ALAZ.SystemEx.NetEx.SocketsEx;
using D2Packets.D2Packets;
using MBNCSUtil;

namespace AGB.D2.Net;

public class D2Socket
{
	private bool DecryptD2GS = false;

	private readonly PacketBuffer BSPacketBuffer;

	private readonly PacketBuffer RSPacketBuffer;

	private readonly PacketBuffer GSPacketBuffer;

	public D2PacketHandler PacketHandler;

	public Socket BattleNet;

	public Socket Realm;

	public Socket Game;

	public event UnknownPacketEventHandler PacketReceived;

	public D2Socket()
	{
		BSPacketBuffer = new PacketBuffer();
		RSPacketBuffer = new PacketBuffer();
		GSPacketBuffer = new PacketBuffer();
		PacketHandler = new D2PacketHandler();
		Thread decodeThread = new Thread(DecodeLoop);
		decodeThread.Start();
		BattleNet = new Socket();
		Realm = new Socket();
		Game = new Socket();
		AGB.Net.ConnectionBase connectionBase = BattleNet.ConnectionBase;
		connectionBase.Disconnected = (AGB.Net.ConnectionBase.ConnectionEventHandler)Delegate.Combine(connectionBase.Disconnected, new AGB.Net.ConnectionBase.ConnectionEventHandler(BattleNet_Disconnected));
		AGB.Net.ConnectionBase connectionBase2 = Realm.ConnectionBase;
		connectionBase2.Disconnected = (AGB.Net.ConnectionBase.ConnectionEventHandler)Delegate.Combine(connectionBase2.Disconnected, new AGB.Net.ConnectionBase.ConnectionEventHandler(Realm_Disconnected));
		AGB.Net.ConnectionBase connectionBase3 = Game.ConnectionBase;
		connectionBase3.Disconnected = (AGB.Net.ConnectionBase.ConnectionEventHandler)Delegate.Combine(connectionBase3.Disconnected, new AGB.Net.ConnectionBase.ConnectionEventHandler(Game_Disconnected));
		AGB.Net.ConnectionBase connectionBase4 = BattleNet.ConnectionBase;
		AGB.Net.ConnectionBase.PacketEventHandler received = connectionBase4.Received;
		AGB.Net.ConnectionBase.PacketEventHandler b = delegate(MessageEventArgs e)
		{
			BSPacketBuffer.Enqueue(e.Buffer, e.Buffer.Length);
		};
		connectionBase4.Received = (AGB.Net.ConnectionBase.PacketEventHandler)Delegate.Combine(received, b);
		AGB.Net.ConnectionBase connectionBase5 = Realm.ConnectionBase;
		connectionBase5.Received = (AGB.Net.ConnectionBase.PacketEventHandler)Delegate.Combine(connectionBase5.Received, (AGB.Net.ConnectionBase.PacketEventHandler)delegate(MessageEventArgs e)
		{
			RSPacketBuffer.Enqueue(e.Buffer, e.Buffer.Length);
		});
		AGB.Net.ConnectionBase connectionBase6 = Game.ConnectionBase;
		connectionBase6.Received = (AGB.Net.ConnectionBase.PacketEventHandler)Delegate.Combine(connectionBase6.Received, (AGB.Net.ConnectionBase.PacketEventHandler)delegate(MessageEventArgs e)
		{
			GSPacketBuffer.Enqueue(e.Buffer, e.Buffer.Length);
		});
	}

	private void BattleNet_Disconnected(ConnectionEventArgs e)
	{
		BSPacketBuffer.Clear();
	}

	private void Realm_Disconnected(ConnectionEventArgs e)
	{
		RSPacketBuffer.Clear();
	}

	private void Game_Disconnected(ConnectionEventArgs e)
	{
		GSPacketBuffer.Clear();
		DecryptD2GS = false;
	}

	public void Close()
	{
		if (BattleNet != null)
		{
			BattleNet.Close();
		}
		if (Realm != null)
		{
			Realm.Close();
		}
		if (Game != null)
		{
			Game.Close();
		}
	}

	private void DecodeLoop()
	{
		while (true)
		{
			bool flag = true;
			if (BSPacketBuffer.Count > 3)
			{
				List<byte> packet = new List<byte>();
				packet.Add(BSPacketBuffer.Dequeue());
				packet.Add(BSPacketBuffer.Dequeue());
				packet.Add(BSPacketBuffer.Dequeue());
				packet.Add(BSPacketBuffer.Dequeue());
				int length = BitConverter.ToInt16(packet.ToArray(), 2);
				while (BSPacketBuffer.Count < length - 4)
				{
					Thread.Sleep(1);
				}
				for (int i = 0; i < length - 4; i++)
				{
					packet.Add(BSPacketBuffer.Dequeue());
				}
				byte[] data = packet.ToArray();
				if (this.PacketReceived != null)
				{
					this.PacketReceived(PacketOrigin.BattleNetServer, data, length);
				}
				PacketHandler.AddPacket(PacketOrigin.BattleNetServer, data);
			}
			if (RSPacketBuffer.Count > 1)
			{
				List<byte> packet = new List<byte>();
				packet.Add(RSPacketBuffer.Dequeue());
				packet.Add(RSPacketBuffer.Dequeue());
				int length = BitConverter.ToInt16(packet.ToArray(), 0);
				while (RSPacketBuffer.Count < length - 2)
				{
					Thread.Sleep(1);
				}
				for (int i = 0; i < length - 2; i++)
				{
					packet.Add(RSPacketBuffer.Dequeue());
				}
				byte[] data = packet.ToArray();
				if (this.PacketReceived != null)
				{
					this.PacketReceived(PacketOrigin.RealmServer, data, length);
				}
				PacketHandler.AddPacket(PacketOrigin.RealmServer, data);
			}
			if (GSPacketBuffer.Count > 0 && !DecryptD2GS)
			{
				byte b = GSPacketBuffer.Peek();
				if (b == 175)
				{
					byte[] packet2 = new byte[2]
					{
						GSPacketBuffer.Dequeue(),
						GSPacketBuffer.Dequeue()
					};
					if (this.PacketReceived != null)
					{
						this.PacketReceived(PacketOrigin.GameServer, packet2, packet2.Length);
					}
					PacketHandler.AddPacket(PacketOrigin.GameServer, packet2);
					DecryptD2GS = true;
					continue;
				}
			}
			if (GSPacketBuffer.Count > 1)
			{
				byte[] dataHeader;
				try
				{
					dataHeader = GSPacketBuffer.ToArray();
				}
				catch (ArgumentException)
				{
					continue;
				}
				int offset;
				int len = D2GSCompression.GetCompressedDataLength(dataHeader, 0, out offset);
				if (GSPacketBuffer.Count < len + offset)
				{
					continue;
				}
				List<byte> packet = new List<byte>();
				packet.AddRange(GSPacketBuffer.Dequeue(len + offset));
				byte[] decompressionBuffer = new byte[10000];
				List<byte[]> packets = new List<byte[]>();
				int dLen = D2GSCompression.DecompressData(packet.ToArray(), offset, len, decompressionBuffer);
				int i = 0;
				int pLen;
				for (pLen = 0; i < dLen; i += pLen)
				{
					pLen = D2PacketsInfo.GetGSPacketSize(decompressionBuffer, i, dLen - i);
					if (pLen == 0 || pLen == -1)
					{
						Util.FileAppend("badpackets.txt", "Decompression buffer: ");
						Util.FileAppend("badpackets.txt", DataFormatter.Format(decompressionBuffer));
						throw new Exception("bad packets yo");
					}
					if (pLen < 1)
					{
						break;
					}
					byte[] data = new byte[pLen];
					Array.Copy(decompressionBuffer, i, data, 0, pLen);
					packets.Add(data);
				}
				if (pLen >= 1)
				{
					foreach (byte[] packetData in packets)
					{
						PacketHandler.AddPacket(PacketOrigin.GameServer, packetData);
					}
				}
			}
			Thread.Sleep(1);
		}
	}
}
