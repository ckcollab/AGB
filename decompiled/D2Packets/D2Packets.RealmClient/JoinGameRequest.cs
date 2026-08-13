using System;
using ETUtils;

namespace D2Packets.RealmClient;

/// <summary>
/// Realm Client Packet 0x04 - Join Game Request
/// <para>Requests joining a game - must be sent after successful game creation.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.RealmServer.JoinGameResponse" />
/// </remarks>
public class JoinGameRequest : RCPacket
{
	protected ushort requestID;

	protected string name;

	protected string password = null;

	public ushort RequestID => requestID;

	public string Name => name;

	public string Password => password;

	public JoinGameRequest(byte[] data)
		: base(data)
	{
		requestID = BitConverter.ToUInt16(data, 3);
		name = ByteConverter.GetNullString(data, 5);
		if (data.Length > 7 + name.Length)
		{
			password = ByteConverter.GetNullString(data, 6 + name.Length);
		}
	}
}
