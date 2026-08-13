using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x5A - Information Message
/// <para>Various player action and status related messages.</para>
/// </summary>
public class InformationMessage : GSPacket
{
	public static readonly int NULL_UInt32 = 0;

	public static readonly int NULL_Int32 = -1;

	protected InformationMessageType type;

	protected byte actionType;

	protected uint objectUID;

	protected string subjectName = null;

	protected string objectName = null;

	protected UnitType slayerType = UnitType.NotApplicable;

	protected CharacterClass charClass = CharacterClass.NotApplicable;

	protected GameObjectClass slayerObject = GameObjectClass.NotApplicable;

	protected NPCClass slayerMonster = NPCClass.NotApplicable;

	protected PlayerInformationActionType informationType = PlayerInformationActionType.None;

	protected PlayerRelationActionType relationType = PlayerRelationActionType.NotApplicable;

	protected int amount = -1;

	public InformationMessageType Type => type;

	public byte ActionType => actionType;

	public uint ObjectUID => objectUID;

	public string SubjectName => subjectName;

	public string ObjectName => objectName;

	public UnitType SlayerType => slayerType;

	public CharacterClass Class => charClass;

	public GameObjectClass SlayerObject => slayerObject;

	public NPCClass SlayerMonster => slayerMonster;

	public PlayerInformationActionType InformationType => informationType;

	public PlayerRelationActionType RelationType => relationType;

	public int Amount => amount;

	public InformationMessage(byte[] data)
		: base(data)
	{
		type = (InformationMessageType)data[1];
		actionType = data[2];
		switch (type)
		{
		case InformationMessageType.DroppedFromGame:
		case InformationMessageType.JoinedGame:
		case InformationMessageType.LeftGame:
			subjectName = ByteConverter.GetNullString(data, 8);
			objectName = ByteConverter.GetNullString(data, 24);
			break;
		case InformationMessageType.NotInGame:
			subjectName = ByteConverter.GetNullString(data, 8);
			break;
		case InformationMessageType.PlayerSlain:
			slayerType = (UnitType)data[7];
			subjectName = ByteConverter.GetNullString(data, 8);
			if (slayerType == UnitType.Player)
			{
				charClass = (CharacterClass)BitConverter.ToUInt32(data, 3);
				objectName = ByteConverter.GetNullString(data, 24);
			}
			else if (slayerType == UnitType.NPC)
			{
				slayerMonster = (NPCClass)BitConverter.ToUInt32(data, 3);
			}
			else if (slayerType == UnitType.GameObject)
			{
				slayerObject = (GameObjectClass)BitConverter.ToUInt32(data, 3);
			}
			break;
		case InformationMessageType.PlayerRelation:
			informationType = (PlayerInformationActionType)actionType;
			objectUID = BitConverter.ToUInt32(data, 3);
			relationType = (PlayerRelationActionType)data[7];
			break;
		case InformationMessageType.SoJsSoldToMerchants:
			amount = BitConverter.ToInt32(data, 3);
			break;
		}
	}
}
