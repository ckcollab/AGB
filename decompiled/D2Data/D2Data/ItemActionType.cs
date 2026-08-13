namespace D2Data;

public enum ItemActionType
{
	AddToGround,
	/// <summary>
	/// Only sent if item goes to cursor (packet 0x0A removes items from ground...)
	/// </summary>
	PickToCursor,
	DropToGround,
	OnGround,
	PutInContainer,
	RemoveFromContainer,
	Equip,
	/// <summary>
	/// Sent for the equipped item when changing from a two handed weapon to a single handed weapon or vice versa.
	/// <para>The item must be equipped on the "empty" hand or a regular SwapBodyItem will be sent instead. 
	/// If currently wearing a two handed weapon, the empty hand means the left hand. 
	/// The result will be the new item being equipped and the old going to cursor.</para>
	/// </summary>
	IndirectlySwapBodyItem,
	Unequip,
	SwapBodyItem,
	AddQuantity,
	AddToShop,
	RemoveFromShop,
	SwapInContainer,
	PutInBelt,
	RemoveFromBelt,
	SwapInBelt,
	/// <summary>
	/// Sent for the secondary hand's item going to inventory when changing from a dual item setup to a two handed weapon.
	/// </summary>
	AutoUnequip,
	/// <summary>
	/// Item on cursor when entering game.
	/// <para>Also sent along with a 0x9d type 0x08 packet when unequipping merc item.</para>
	/// </summary>
	ToCursor,
	ItemInSocket,
	UNKNOWNx14,
	/// <summary>
	/// When inserting item in socket, for each potion that drops in belt when lower one is removed, etc.
	/// </summary>
	UpdateStats,
	UNKNOWNx16,
	WeaponSwitch
}
