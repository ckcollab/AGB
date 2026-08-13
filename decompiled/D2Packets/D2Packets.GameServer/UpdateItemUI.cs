using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x77 - Update Item UI
/// <para>Notifies of item related UI changes (mostly trade related but also stash and cube).</para>
/// </summary>
public class UpdateItemUI : GSPacket
{
	protected ItemUIAction action;

	public ItemUIAction Action => action;

	public UpdateItemUI(byte[] data)
		: base(data)
	{
		action = (ItemUIAction)data[1];
	}

	public UpdateItemUI(ItemUIAction action)
		: base(Build(action))
	{
		this.action = action;
	}

	public static byte[] Build(ItemUIAction action)
	{
		return new byte[2]
		{
			119,
			(byte)action
		};
	}
}
