using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x03 - Load Act
/// <para>Sent on game join or act change before map and game objects data.</para>
/// </summary>
public class LoadAct : GSPacket
{
	protected byte act;

	protected uint mapId;

	protected AreaLevel townArea;

	public string Unknown8 => ByteConverter.ToHexString(data, 8, 4);

	public byte Act => act;

	public AreaLevel TownArea => townArea;

	public uint MapId => mapId;

	public LoadAct(byte[] data)
		: base(data)
	{
		act = data[1];
		mapId = BitConverter.ToUInt32(data, 2);
		townArea = (AreaLevel)BitConverter.ToUInt16(data, 6);
	}
}
