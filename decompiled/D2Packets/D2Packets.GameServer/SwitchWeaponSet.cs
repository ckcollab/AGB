namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x97 - Switch Weapon Set
/// <para>Sent when switching weapons to toogle the active weapon set.</para>
/// <para>Also sent when joining game with alternate weapons as active set.</para>
/// </summary>
public class SwitchWeaponSet : GSPacket
{
	public SwitchWeaponSet(byte[] data)
		: base(data)
	{
	}

	public SwitchWeaponSet()
		: base(Build())
	{
	}

	public static byte[] Build()
	{
		return new byte[1] { 151 };
	}
}
