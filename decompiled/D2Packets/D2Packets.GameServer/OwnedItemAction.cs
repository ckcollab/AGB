using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x9D - Owned Item Action
/// </summary>
public class OwnedItemAction : ItemAction
{
	public static readonly bool WRAPPED = true;

	public static readonly int NULL_Int32 = -1;

	protected UnitType ownerType;

	protected uint ownerUID;

	public UnitType OwnerType => ownerType;

	public uint OwnerUID => ownerUID;

	public OwnedItemAction(byte[] data)
		: base(data)
	{
		ownerType = (UnitType)data[8];
		ownerUID = BitConverter.ToUInt32(data, 9);
	}
}
