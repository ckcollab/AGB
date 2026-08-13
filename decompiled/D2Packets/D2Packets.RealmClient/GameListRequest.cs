using System;
using ETUtils;

namespace D2Packets.RealmClient;

/// <summary>
/// Realm Client Packet 0x05 - Game List Request
/// <para>Request a list of available games.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.RealmServer.GameList" />
/// </remarks>
public class GameListRequest : RCPacket
{
	protected ushort requestID;

	protected uint unknown1;

	protected string unknown2 = null;

	public ushort RequestID => requestID;

	/// <summary>
	/// Player / session ID ?
	/// </summary>
	public uint Unknown1 => unknown1;

	public string Unknown2 => unknown2;

	public GameListRequest(byte[] data)
		: base(data)
	{
		requestID = BitConverter.ToUInt16(data, 3);
		unknown1 = BitConverter.ToUInt32(data, 5);
		if (data.Length > 10)
		{
			unknown2 = ByteConverter.GetNullString(data, 9);
		}
	}
}
