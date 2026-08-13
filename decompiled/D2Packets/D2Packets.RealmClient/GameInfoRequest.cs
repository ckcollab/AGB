using System;
using ETUtils;

namespace D2Packets.RealmClient;

/// <summary>
/// Realm Client Packet 0x06 - Game Info Request
/// <para>Requests information about a game.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.RealmServer.GameInfo" />
/// </remarks>
public class GameInfoRequest : RCPacket
{
	protected ushort requestID;

	protected string name;

	public ushort RequestID => requestID;

	public string Name => name;

	public GameInfoRequest(byte[] data)
		: base(data)
	{
		requestID = BitConverter.ToUInt16(data, 3);
		name = ByteConverter.GetNullString(data, 5);
	}
}
