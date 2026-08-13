using System;
using D2Packets.BnetClient;
using D2Packets.BnetServer;
using D2Packets.GameClient;
using D2Packets.GameServer;
using D2Packets.RealmClient;
using D2Packets.RealmServer;

namespace D2Packets.D2Packets;

public class D2PacketsInfo
{
	public const int BS_PACKET_COUNT = 131;

	public const int BS_PACKET_ID = 2;

	public const int BC_PACKET_COUNT = 131;

	public const int BC_PACKET_ID = 3;

	public const int RS_PACKET_COUNT = 32;

	public const int RS_PACKET_ID = 4;

	public const int RC_PACKET_COUNT = 32;

	public const int RC_PACKET_ID = 5;

	public const int GS_PACKET_COUNT = 177;

	public const int GS_PACKET_ID = 0;

	public const int GC_PACKET_COUNT = 110;

	public const int GC_PACKET_ID = 1;

	public static readonly int[] GSPacketSizeArray;

	public static readonly int[] GCPacketSizeArray;

	public static readonly Type[] BSPacketTypes;

	public static readonly Type[] BCPacketTypes;

	public static readonly Type[] RSPacketTypes;

	public static readonly Type[] RCPacketTypes;

	public static readonly Type[] GSPacketTypes;

	public static readonly Type[] GCPacketTypes;

	public static int GetGSPacketSize(byte[] data)
	{
		return GetGSPacketSize(data, 0, data.Length);
	}

	public static int GetGSPacketSize(byte[] data, int offset, int length)
	{
		if (data[offset] > GSPacketSizeArray.Length)
		{
			return 0;
		}
		int pLen = GSPacketSizeArray[data[offset]];
		if (pLen == -1)
		{
			switch (data[offset])
			{
			case 38:
			{
				if (length < 13)
				{
					break;
				}
				bool have1 = false;
				for (int i = 10; i < length; i++)
				{
					if (data[i + offset] == 0)
					{
						if (have1)
						{
							pLen = i + 1;
							break;
						}
						have1 = true;
					}
				}
				break;
			}
			case 91:
				if (length >= 3)
				{
					pLen = BitConverter.ToUInt16(data, offset + 1);
				}
				break;
			case 148:
				if (length >= 2)
				{
					pLen = 6 + data[offset + 1] * 3;
				}
				break;
			case 156:
			case 157:
				if (length >= 3)
				{
					pLen = data[offset + 2];
				}
				break;
			case 168:
			case 170:
				if (length >= 7)
				{
					pLen = data[offset + 6];
				}
				break;
			case 172:
				if (length >= 13)
				{
					pLen = data[offset + 12];
				}
				break;
			case 174:
				if (length >= 4)
				{
					pLen = 3 + BitConverter.ToUInt16(data, offset + 1);
				}
				break;
			case 62:
				pLen = data[offset + 1];
				break;
			default:
				pLen = 0;
				break;
			}
		}
		return pLen;
	}

	public static int GetGCPacketSize(byte[] data)
	{
		return GetGCPacketSize(data, 0, data.Length);
	}

	public static int GetGCPacketSize(byte[] data, int offset, int length)
	{
		if (data[offset] >= GCPacketSizeArray.Length)
		{
			return 0;
		}
		int pLen = GCPacketSizeArray[data[offset]];
		if (pLen == -1)
		{
			switch (data[offset])
			{
			case 20:
			case 21:
			{
				pLen = 3;
				for (int i = 0; i < 3; i++)
				{
					int j = 0;
					while (true)
					{
						bool flag = true;
						if (length < j + pLen + offset)
						{
							return -1;
						}
						if (data[j + pLen + offset] == 0)
						{
							break;
						}
						j++;
					}
					pLen += j + 1;
				}
				break;
			}
			case 102:
				if (length >= offset + 5)
				{
					pLen = 3 + BitConverter.ToUInt16(data, offset + 1);
				}
				break;
			default:
				pLen = 0;
				break;
			}
		}
		return pLen;
	}

	static D2PacketsInfo()
	{
		GSPacketSizeArray = new int[182]
		{
			1, 8, 1, 12, 1, 1, 1, 6, 6, 11,
			6, 6, 9, 13, 12, 16, 16, 8, 26, 14,
			18, 11, -1, 0, 15, 2, 2, 3, 5, 3,
			4, 6, 10, 12, 12, 13, 90, 90, -1, 40,
			103, 97, 15, 0, 8, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, -1, 8, 13, 0, 6, 0, 0, 13,
			0, 11, 11, 0, 0, 0, 16, 17, 7, 1,
			15, 14, 42, 10, 3, 0, 0, 14, 7, 26,
			40, -1, 5, 6, 38, 5, 7, 2, 7, 21,
			0, 7, 7, 16, 21, 12, 12, 16, 16, 10,
			1, 1, 1, 1, 1, 32, 10, 13, 6, 2,
			21, 6, 13, 8, 6, 18, 5, 10, 0, 20,
			29, 0, 0, 0, 0, 0, 0, 2, 6, 6,
			11, 7, 10, 33, 13, 26, 6, 8, -1, 13,
			9, 1, 7, 16, 17, 7, -1, -1, 7, 8,
			10, 7, 8, 24, 3, 8, -1, 7, -1, 7,
			-1, 7, -1, 0, -1, -1, 1, 0, 53, -1,
			5, 0
		};
		GCPacketSizeArray = new int[112]
		{
			0, 5, 9, 5, 9, 5, 9, 9, 5, 9,
			9, 1, 5, 9, 9, 5, 9, 9, 1, 9,
			-1, -1, 13, 5, 17, 5, 9, 9, 3, 9,
			9, 17, 13, 9, 5, 9, 5, 9, 13, 9,
			9, 9, 9, 0, 0, 1, 3, 9, 9, 9,
			17, 17, 5, 17, 9, 5, 13, 5, 3, 3,
			9, 5, 5, 3, 1, 1, 1, 1, 17, 9,
			13, 13, 1, 9, 0, 9, 5, 3, 0, 7,
			9, 9, 5, 1, 1, 0, 0, 0, 3, 17,
			0, 0, 0, 7, 6, 5, 1, 3, 5, 5,
			9, 17, -1, 0, 37, 1, 1, 1, 1, 13,
			0, 1
		};
		BSPacketTypes = new Type[131];
		BCPacketTypes = new Type[131];
		RSPacketTypes = new Type[32];
		RCPacketTypes = new Type[32];
		GSPacketTypes = new Type[177];
		GCPacketTypes = new Type[110];
		BSPacketTypes[0] = typeof(global::D2Packets.BnetServer.KeepAlive);
		BSPacketTypes[10] = typeof(EnterChatResponse);
		BSPacketTypes[11] = typeof(ChannelList);
		BSPacketTypes[15] = typeof(ChatEvent);
		BSPacketTypes[21] = typeof(AdInfo);
		BSPacketTypes[37] = typeof(BnetPing);
		BSPacketTypes[51] = typeof(FileTimeInfo);
		BSPacketTypes[58] = typeof(BnetLogonResponse);
		BSPacketTypes[62] = typeof(RealmLogonResponse);
		BSPacketTypes[64] = typeof(QueryRealmsResponse);
		BSPacketTypes[70] = typeof(NewsInfo);
		BSPacketTypes[74] = typeof(ExtraWorkInfo);
		BSPacketTypes[76] = typeof(RequiredExtraWorkInfo);
		BSPacketTypes[80] = typeof(BnetConnectionResponse);
		BSPacketTypes[81] = typeof(BnetAuthResponse);
		BCPacketTypes[0] = typeof(global::D2Packets.BnetClient.KeepAlive);
		BCPacketTypes[10] = typeof(EnterChatRequest);
		BCPacketTypes[11] = typeof(ChannelListRequest);
		BCPacketTypes[12] = typeof(JoinChannel);
		BCPacketTypes[14] = typeof(ChatCommand);
		BCPacketTypes[16] = typeof(LeaveChat);
		BCPacketTypes[21] = typeof(AdInfoRequest);
		BCPacketTypes[28] = typeof(StartGame);
		BCPacketTypes[31] = typeof(LeaveGame);
		BCPacketTypes[33] = typeof(DisplayAd);
		BCPacketTypes[34] = typeof(NotifyJoin);
		BCPacketTypes[37] = typeof(BnetPong);
		BCPacketTypes[51] = typeof(FileTimeRequest);
		BCPacketTypes[58] = typeof(BnetLogonRequest);
		BCPacketTypes[62] = typeof(RealmLogonRequest);
		BCPacketTypes[64] = typeof(QueryRealms);
		BCPacketTypes[70] = typeof(NewsInfoRequest);
		BCPacketTypes[75] = typeof(ExtraWorkResponse);
		BCPacketTypes[80] = typeof(BnetConnectionRequest);
		BCPacketTypes[81] = typeof(BnetAuthRequest);
		RSPacketTypes[1] = typeof(RealmStartupResponse);
		RSPacketTypes[2] = typeof(CharacterCreationResponse);
		RSPacketTypes[3] = typeof(CreateGameResponse);
		RSPacketTypes[4] = typeof(JoinGameResponse);
		RSPacketTypes[5] = typeof(GameList);
		RSPacketTypes[6] = typeof(GameInfo);
		RSPacketTypes[7] = typeof(CharacterLogonResponse);
		RSPacketTypes[10] = typeof(CharacterDeletionResponse);
		RSPacketTypes[18] = typeof(MessageOfTheDay);
		RSPacketTypes[20] = typeof(GameCreationQueue);
		RSPacketTypes[24] = typeof(CharacterUpgradeResponse);
		RSPacketTypes[25] = typeof(CharacterList);
		RCPacketTypes[1] = typeof(RealmStartupRequest);
		RCPacketTypes[2] = typeof(CharacterCreationRequest);
		RCPacketTypes[3] = typeof(CreateGameRequest);
		RCPacketTypes[4] = typeof(JoinGameRequest);
		RCPacketTypes[5] = typeof(GameListRequest);
		RCPacketTypes[6] = typeof(GameInfoRequest);
		RCPacketTypes[7] = typeof(CharacterLogonRequest);
		RCPacketTypes[10] = typeof(CharacterDeletionRequest);
		RCPacketTypes[18] = typeof(MessageOfTheDayRequest);
		RCPacketTypes[19] = typeof(CancelGameCreation);
		RCPacketTypes[24] = typeof(CharacterUpgradeRequest);
		RCPacketTypes[25] = typeof(CharacterListRequest);
		GSPacketTypes[0] = typeof(GameLoading);
		GSPacketTypes[1] = typeof(GameLogonReceipt);
		GSPacketTypes[2] = typeof(GameLogonSuccess);
		GSPacketTypes[3] = typeof(LoadAct);
		GSPacketTypes[4] = typeof(LoadDone);
		GSPacketTypes[5] = typeof(UnloadDone);
		GSPacketTypes[6] = typeof(GameLogoutSuccess);
		GSPacketTypes[7] = typeof(MapAdd);
		GSPacketTypes[8] = typeof(MapRemove);
		GSPacketTypes[9] = typeof(AssignWarp);
		GSPacketTypes[10] = typeof(RemoveGroundUnit);
		GSPacketTypes[11] = typeof(GameHandshake);
		GSPacketTypes[12] = typeof(NPCGetHit);
		GSPacketTypes[13] = typeof(PlayerStop);
		GSPacketTypes[14] = typeof(SetGameObjectMode);
		GSPacketTypes[15] = typeof(PlayerMove);
		GSPacketTypes[16] = typeof(PlayerMoveToTarget);
		GSPacketTypes[17] = typeof(ReportKill);
		GSPacketTypes[21] = typeof(PlayerReassign);
		GSPacketTypes[25] = typeof(SmallGoldAdd);
		GSPacketTypes[26] = typeof(ByteToExperience);
		GSPacketTypes[27] = typeof(WordToExperience);
		GSPacketTypes[28] = typeof(DWordToExperience);
		GSPacketTypes[29] = typeof(AttributeByte);
		GSPacketTypes[30] = typeof(AttributeWord);
		GSPacketTypes[31] = typeof(AttributeDWord);
		GSPacketTypes[32] = typeof(PlayerAttributeNotification);
		GSPacketTypes[33] = typeof(UpdateSkill);
		GSPacketTypes[34] = typeof(UpdatePlayerItemSkill);
		GSPacketTypes[35] = typeof(AssignSkill);
		GSPacketTypes[38] = typeof(GameMessage);
		GSPacketTypes[39] = typeof(NPCInfo);
		GSPacketTypes[40] = typeof(UpdateQuestInfo);
		GSPacketTypes[41] = typeof(UpdateGameQuestLog);
		GSPacketTypes[42] = typeof(TransactionComplete);
		GSPacketTypes[44] = typeof(PlaySound);
		GSPacketTypes[62] = typeof(UpdateItemStats);
		GSPacketTypes[63] = typeof(UseStackableItem);
		GSPacketTypes[66] = typeof(PlayerClearCursor);
		GSPacketTypes[71] = typeof(Relator1);
		GSPacketTypes[72] = typeof(Relator2);
		GSPacketTypes[76] = typeof(UnitUseSkillOnTarget);
		GSPacketTypes[77] = typeof(UnitUseSkill);
		GSPacketTypes[78] = typeof(MercForHire);
		GSPacketTypes[79] = typeof(MercForHireListStart);
		GSPacketTypes[81] = typeof(AssignGameObject);
		GSPacketTypes[82] = typeof(UpdateQuestLog);
		GSPacketTypes[83] = typeof(PartyRefresh);
		GSPacketTypes[89] = typeof(AssignPlayer);
		GSPacketTypes[90] = typeof(InformationMessage);
		GSPacketTypes[91] = typeof(PlayerInGame);
		GSPacketTypes[92] = typeof(PlayerLeaveGame);
		GSPacketTypes[93] = typeof(QuestItemState);
		GSPacketTypes[96] = typeof(PortalInfo);
		GSPacketTypes[99] = typeof(OpenWaypoint);
		GSPacketTypes[101] = typeof(PlayerKillCount);
		GSPacketTypes[103] = typeof(NPCMove);
		GSPacketTypes[104] = typeof(NPCMoveToTarget);
		GSPacketTypes[105] = typeof(SetNPCMode);
		GSPacketTypes[107] = typeof(NPCAction);
		GSPacketTypes[108] = typeof(MonsterAttack);
		GSPacketTypes[109] = typeof(NPCStop);
		GSPacketTypes[116] = typeof(PlayerCorpseVisible);
		GSPacketTypes[117] = typeof(AboutPlayer);
		GSPacketTypes[118] = typeof(PlayerInSight);
		GSPacketTypes[119] = typeof(UpdateItemUI);
		GSPacketTypes[120] = typeof(AcceptTrade);
		GSPacketTypes[121] = typeof(GoldTrade);
		GSPacketTypes[122] = typeof(SummonAction);
		GSPacketTypes[123] = typeof(AssignSkillHotkey);
		GSPacketTypes[124] = typeof(UseSpecialItem);
		GSPacketTypes[125] = typeof(SetItemState);
		GSPacketTypes[127] = typeof(PartyMemberUpdate);
		GSPacketTypes[129] = typeof(AssignMerc);
		GSPacketTypes[130] = typeof(PortalOwnership);
		GSPacketTypes[138] = typeof(NPCWantsInteract);
		GSPacketTypes[139] = typeof(PlayerPartyRelationship);
		GSPacketTypes[140] = typeof(PlayerRelationship);
		GSPacketTypes[141] = typeof(AssignPlayerToParty);
		GSPacketTypes[142] = typeof(AssignPlayerCorpse);
		GSPacketTypes[143] = typeof(Pong);
		GSPacketTypes[144] = typeof(PartyMemberPulse);
		GSPacketTypes[148] = typeof(SkillLog);
		GSPacketTypes[149] = typeof(LifeManaChange);
		GSPacketTypes[150] = typeof(WalkVerify);
		GSPacketTypes[151] = typeof(SwitchWeaponSet);
		GSPacketTypes[153] = typeof(ItemTriggerSkill);
		GSPacketTypes[156] = typeof(WorldItemAction);
		GSPacketTypes[157] = typeof(OwnedItemAction);
		GSPacketTypes[158] = typeof(MercAttributeByte);
		GSPacketTypes[159] = typeof(MercAttributeWord);
		GSPacketTypes[160] = typeof(MercAttributeDWord);
		GSPacketTypes[161] = typeof(MercByteToExperience);
		GSPacketTypes[162] = typeof(MercWordToExperience);
		GSPacketTypes[167] = typeof(DelayedState);
		GSPacketTypes[168] = typeof(SetState);
		GSPacketTypes[169] = typeof(EndState);
		GSPacketTypes[170] = typeof(AddUnit);
		GSPacketTypes[171] = typeof(NPCHeal);
		GSPacketTypes[172] = typeof(AssignNPC);
		GSPacketTypes[174] = typeof(WardenCheck);
		GSPacketTypes[175] = typeof(RequestLogonInfo);
		GSPacketTypes[176] = typeof(GameOver);
		GCPacketTypes[1] = typeof(WalkToLocation);
		GCPacketTypes[2] = typeof(WalkToTarget);
		GCPacketTypes[3] = typeof(RunToLocation);
		GCPacketTypes[4] = typeof(RunToTarget);
		GCPacketTypes[5] = typeof(CastLeftSkill);
		GCPacketTypes[6] = typeof(CastLeftSkillOnTarget);
		GCPacketTypes[7] = typeof(CastLeftSkillOnTargetStopped);
		GCPacketTypes[8] = typeof(RecastLeftSkill);
		GCPacketTypes[9] = typeof(RecastLeftSkillOnTarget);
		GCPacketTypes[10] = typeof(RecastLeftSkillOnTargetStopped);
		GCPacketTypes[12] = typeof(CastRightSkill);
		GCPacketTypes[13] = typeof(CastRightSkillOnTarget);
		GCPacketTypes[14] = typeof(CastRightSkillOnTargetStopped);
		GCPacketTypes[15] = typeof(RecastRightSkill);
		GCPacketTypes[16] = typeof(RecastRightSkillOnTarget);
		GCPacketTypes[17] = typeof(RecastRightSkillOnTargetStopped);
		GCPacketTypes[19] = typeof(UnitInteract);
		GCPacketTypes[20] = typeof(SendOverheadMessage);
		GCPacketTypes[21] = typeof(SendMessage);
		GCPacketTypes[22] = typeof(PickItem);
		GCPacketTypes[23] = typeof(DropItem);
		GCPacketTypes[24] = typeof(DropItemToContainer);
		GCPacketTypes[25] = typeof(PickItemFromContainer);
		GCPacketTypes[26] = typeof(EquipItem);
		GCPacketTypes[29] = typeof(SwapEquippedItem);
		GCPacketTypes[28] = typeof(UnequipItem);
		GCPacketTypes[31] = typeof(SwapContainerItem);
		GCPacketTypes[32] = typeof(UseContainerItem);
		GCPacketTypes[33] = typeof(StackItems);
		GCPacketTypes[35] = typeof(AddBeltItem);
		GCPacketTypes[36] = typeof(RemoveBeltItem);
		GCPacketTypes[37] = typeof(SwapBeltItem);
		GCPacketTypes[38] = typeof(UseBeltItem);
		GCPacketTypes[39] = typeof(IdentifyItem);
		GCPacketTypes[41] = typeof(EmbedItem);
		GCPacketTypes[42] = typeof(ItemToCube);
		GCPacketTypes[47] = typeof(TownFolkInteract);
		GCPacketTypes[48] = typeof(TownFolkCancelInteraction);
		GCPacketTypes[49] = typeof(DisplayQuestMessage);
		GCPacketTypes[50] = typeof(BuyItem);
		GCPacketTypes[51] = typeof(SellItem);
		GCPacketTypes[52] = typeof(CainIdentifyItems);
		GCPacketTypes[53] = typeof(TownFolkRepair);
		GCPacketTypes[54] = typeof(HireMercenary);
		GCPacketTypes[55] = typeof(IdentifyGambleItem);
		GCPacketTypes[56] = typeof(TownFolkMenuSelect);
		GCPacketTypes[58] = typeof(IncrementAttribute);
		GCPacketTypes[59] = typeof(IncrementSkill);
		GCPacketTypes[60] = typeof(SelectSkill);
		GCPacketTypes[61] = typeof(HoverUnit);
		GCPacketTypes[63] = typeof(SendCharacterSpeech);
		GCPacketTypes[64] = typeof(RequestQuestLog);
		GCPacketTypes[65] = typeof(Respawn);
		GCPacketTypes[73] = typeof(WaypointInteract);
		GCPacketTypes[75] = typeof(RequestReassign);
		GCPacketTypes[79] = typeof(ClickButton);
		GCPacketTypes[80] = typeof(DropGold);
		GCPacketTypes[81] = typeof(SetSkillHotkey);
		GCPacketTypes[88] = typeof(CloseQuest);
		GCPacketTypes[89] = typeof(GoToTownFolk);
		GCPacketTypes[93] = typeof(SetPlayerRelation);
		GCPacketTypes[94] = typeof(PartyRequest);
		GCPacketTypes[95] = typeof(UpdatePosition);
		GCPacketTypes[96] = typeof(SwitchWeapons);
		GCPacketTypes[97] = typeof(ChangeMercEquipment);
		GCPacketTypes[98] = typeof(ResurrectMerc);
		GCPacketTypes[99] = typeof(InventoryItemToBelt);
		GCPacketTypes[102] = typeof(WardenResponse);
		GCPacketTypes[104] = typeof(GameLogonRequest);
		GCPacketTypes[105] = typeof(ExitGame);
		GCPacketTypes[107] = typeof(EnterGame);
		GCPacketTypes[109] = typeof(Ping);
	}
}
