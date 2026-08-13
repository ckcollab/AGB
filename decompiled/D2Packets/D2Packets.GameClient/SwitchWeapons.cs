namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x60 - Switch Weapons
/// <para>Toogle the active weapon tab.</para>
/// </summary>
public class SwitchWeapons : GCPacket
{
	public SwitchWeapons(byte[] data)
		: base(data)
	{
	}

	public SwitchWeapons()
		: base(Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1] { 96 };
	}
}
