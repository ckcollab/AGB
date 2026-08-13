using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x4B - Request Reassign
/// <para>Requests a PlayerReassign (GS 0x15) from server.</para>
/// <para>Sent when the client is out of sync to request the current position on server.</para>
/// <para>This is also used to bring pets along when using a waypoint or portal, with a NPC UnitType.</para>
/// </summary>
public class RequestReassign : GCPacket
{
	protected UnitType unitType;

	protected uint meUID;

	/// <summary>
	/// UnitType.Player if requesting player position or UnitType.NPC for his pets.
	/// Offset: 1, Length: 4
	/// </summary>
	public UnitType UnitType => unitType;

	/// <summary>
	/// This is always the player's UID, regardless of the UnitType requested.
	/// Offset: 5, Length: 4
	/// </summary>
	public uint MeUID => meUID;

	public RequestReassign(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		meUID = BitConverter.ToUInt32(data, 5);
	}

	public RequestReassign(UnitType unitType, uint meUID)
		: base(Build(unitType, meUID))
	{
		this.unitType = unitType;
		this.meUID = meUID;
	}

	public static byte[] Build(UnitType unitType, uint meUID)
	{
		return new byte[9]
		{
			75,
			(byte)unitType,
			0,
			0,
			0,
			(byte)meUID,
			(byte)(meUID >> 8),
			(byte)(meUID >> 16),
			(byte)(meUID >> 24)
		};
	}
}
