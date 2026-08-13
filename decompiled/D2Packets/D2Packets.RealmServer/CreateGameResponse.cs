using System;

namespace D2Packets.RealmServer;

/// <summary>
/// Realm Server Packet 0x03 - Create Game Response
/// <para>Notifies client of the game creation result.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.RealmClient.CreateGameRequest" />
/// </remarks>
public class CreateGameResponse : RSPacket
{
	protected ushort requestID;

	protected CreateGameResult result;

	protected uint unknown;

	public ushort RequestID => requestID;

	public CreateGameResult Result => result;

	/// <summary>
	/// If game creation succeeded, this is a nonzero value whose meaning is unknown.
	/// </summary>
	public uint Unknown => unknown;

	public CreateGameResponse(byte[] data)
		: base(data)
	{
		requestID = BitConverter.ToUInt16(data, 3);
		unknown = BitConverter.ToUInt32(data, 5);
		result = (CreateGameResult)BitConverter.ToUInt32(data, 9);
	}
}
