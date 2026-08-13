using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x94 - Skill Log
/// <para>Notifies you of your current skills base levels on game join and when they change.</para>
/// </summary>
public class SkillLog : GSPacket
{
	protected BaseSkillLevel[] skills;

	protected uint uid;

	public uint UID => uid;

	public BaseSkillLevel[] Skills => skills;

	public SkillLog(byte[] data)
		: base(data)
	{
		skills = new BaseSkillLevel[data[1]];
		uid = BitConverter.ToUInt32(data, 2);
		for (int i = 0; i < data[1]; i++)
		{
			ref BaseSkillLevel reference = ref skills[i];
			reference = new BaseSkillLevel(BitConverter.ToUInt16(data, 6 + i * 3), data[8 + i * 3]);
		}
	}
}
