using System;
using MBNCSUtil;

namespace AGB.D2.Net.Packets;

public class SetNewGameInfoResult : AGBPacket
{
	public int TotalRuns;

	public int TotalRunsAllocated;

	public SetNewGameInfoResultValue Result;

	public override PacketType Type => PacketType.SetNewGameInfoResult;

	public override byte[] Data
	{
		get
		{
			DataBuffer buffer = new DataBuffer();
			buffer.InsertInt32(TotalRuns);
			buffer.InsertInt32(TotalRunsAllocated);
			buffer.InsertByte((byte)Result);
			return buffer.GetData();
		}
	}

	public static SetNewGameInfoResult Parse(byte[] packetData, int offset)
	{
		if (packetData.Length == 5)
		{
			return null;
		}
		byte[] data = new byte[packetData.Length - offset];
		Array.Copy(packetData, offset, data, 0, packetData.Length - offset);
		SetNewGameInfoResult result = new SetNewGameInfoResult();
		DataReader reader = new DataReader(data);
		result.TotalRuns = reader.ReadInt32();
		result.TotalRunsAllocated = reader.ReadInt32();
		result.Result = (SetNewGameInfoResultValue)reader.ReadByte();
		return result;
	}
}
