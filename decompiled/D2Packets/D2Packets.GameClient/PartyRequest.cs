using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x5E - Party Request
/// </summary>
public class PartyRequest : GCPacket
{
	protected PartyAction action;

	protected uint playerUID;

	public PartyAction Action => action;

	public uint PlayerUID => playerUID;

	public PartyRequest(byte[] data)
		: base(data)
	{
		action = (PartyAction)data[1];
		playerUID = BitConverter.ToUInt32(data, 2);
	}

	public PartyRequest(PartyAction action, uint playerUID)
		: base(Build(action, playerUID))
	{
		this.action = action;
		this.playerUID = playerUID;
	}

	public static byte[] Build(PartyAction action, uint playerUID)
	{
		return new byte[6]
		{
			94,
			(byte)action,
			(byte)playerUID,
			(byte)(playerUID >> 8),
			(byte)(playerUID >> 16),
			(byte)(playerUID >> 24)
		};
	}
}
