using System;
using System.Net;

namespace D2Packets.RealmServer;

/// <summary>
/// Realm Server Packet 0x04 - Join Game Response
/// <para>Notifies client of the join game result.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.RealmClient.JoinGameRequest" />
/// </remarks>
public class JoinGameResponse : RSPacket
{
	protected ushort requestID;

	protected ushort gameToken;

	protected ushort unknown;

	protected IPAddress gameServerIP;

	protected uint gameHash;

	protected JoinGameResult result;

	public ushort RequestID => requestID;

	public ushort GameToken => gameToken;

	public IPAddress GameServerIP
	{
		get
		{
			return gameServerIP;
		}
		set
		{
			IsWritableEx();
			gameServerIP = value;
			byte[] addy = value.GetAddressBytes();
			data[9] = addy[0];
			data[10] = addy[1];
			data[11] = addy[2];
			data[12] = addy[3];
		}
	}

	public uint GameHash => gameHash;

	public JoinGameResult Result => result;

	public ushort Unknown => unknown;

	public JoinGameResponse(byte[] data)
		: base(data)
	{
		requestID = BitConverter.ToUInt16(data, 3);
		gameToken = BitConverter.ToUInt16(data, 5);
		unknown = BitConverter.ToUInt16(data, 7);
		gameServerIP = new IPAddress(BitConverter.ToUInt32(data, 9));
		gameHash = BitConverter.ToUInt32(data, 13);
		result = (JoinGameResult)BitConverter.ToUInt32(data, 17);
	}
}
