namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x19 - Small Gold Add
/// <para>Notifies you that gold has been picked up or transferred from bank to inventory.</para>
/// <para>If amount is larger than 254, a <see cref="T:D2Packets.GameServer.AttributeNotification" /> of type Gold packet will be sent instead.</para>
/// </summary>
public class SmallGoldAdd : GSPacket
{
	protected byte quantity;

	public byte Quantity => quantity;

	public SmallGoldAdd(byte[] data)
		: base(data)
	{
		quantity = data[1];
	}

	public SmallGoldAdd(byte quantity)
		: base(Build(quantity))
	{
		this.quantity = quantity;
	}

	public static byte[] Build(byte quantity)
	{
		return new byte[2] { 25, quantity };
	}
}
