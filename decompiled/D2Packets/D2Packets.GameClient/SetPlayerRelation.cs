using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x5D - Set Player Relation
/// </summary>
public class SetPlayerRelation : GCPacket
{
	protected PlayerRelationType relation;

	protected bool value;

	protected uint uid;

	public uint UID => uid;

	public PlayerRelationType Relation => relation;

	public bool Value => value;

	public SetPlayerRelation(byte[] data)
		: base(data)
	{
		relation = (PlayerRelationType)data[1];
		value = BitConverter.ToBoolean(data, 2);
		uid = BitConverter.ToUInt32(data, 3);
	}

	public SetPlayerRelation(uint uid, PlayerRelationType relation, bool value)
		: base(Build(uid, relation, value))
	{
		this.uid = uid;
		this.relation = relation;
		this.value = value;
	}

	public static byte[] Build(uint uid, PlayerRelationType relation, bool value)
	{
		return new byte[7]
		{
			93,
			(byte)relation,
			(byte)(value ? 1u : 0u),
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
