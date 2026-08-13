using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x7D - Set Item State
/// <para>Only known when an item breaks or a broken item is repaired...</para>
/// </summary>
public class SetItemState : GSPacket
{
	protected UnitType ownerType;

	protected uint ownerUID;

	protected uint itemUID;

	protected byte unknown10;

	protected ItemStateType state;

	protected ItemStateType state2;

	protected byte unknown17;

	public ItemStateType State => state;

	public uint ItemUID => itemUID;

	public UnitType OwnerType => ownerType;

	public uint OwnerUID => ownerUID;

	public ItemStateType State2 => state2;

	public byte Unknown10 => unknown10;

	public byte Unknown17 => unknown17;

	public SetItemState(byte[] data)
		: base(data)
	{
		ownerType = (UnitType)data[1];
		ownerUID = BitConverter.ToUInt32(data, 2);
		itemUID = BitConverter.ToUInt32(data, 6);
		unknown10 = data[10];
		state = (ItemStateType)BitConverter.ToUInt32(data, 11);
		state2 = (ItemStateType)BitConverter.ToUInt16(data, 15);
		unknown17 = data[17];
	}

	public SetItemState(UnitType ownerType, uint ownerUID, uint itemUID, ItemStateType state)
		: base(Build(ownerType, ownerUID, itemUID, state))
	{
		this.ownerType = ownerType;
		this.ownerUID = ownerUID;
		this.itemUID = itemUID;
		this.state = state;
		state2 = state;
	}

	public static byte[] Build(UnitType ownerType, uint ownerUID, uint itemUID, ItemStateType state)
	{
		return new byte[18]
		{
			125,
			(byte)ownerType,
			(byte)ownerUID,
			(byte)(ownerUID >> 8),
			(byte)(ownerUID >> 16),
			(byte)(ownerUID >> 24),
			(byte)itemUID,
			(byte)(itemUID >> 8),
			(byte)(itemUID >> 16),
			(byte)(itemUID >> 24),
			0,
			(byte)state,
			(byte)((uint)state >> 8),
			(byte)((uint)state >> 16),
			(byte)((uint)state >> 24),
			(byte)state,
			(byte)((ushort)state >> 8),
			0
		};
	}
}
