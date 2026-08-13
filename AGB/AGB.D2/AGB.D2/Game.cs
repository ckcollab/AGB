using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AGB.D2.Collections;
using AGB.D2.Net;
using AGB.D2.Net.D2.BC;
using AGB.D2.Net.D2.GC;
using AGB.D2.Net.D2.RC;
using D2Data;
using D2Packets.BnetServer;
using D2Packets.D2Packets;
using D2Packets.GameClient;
using D2Packets.GameServer;
using D2Packets.RealmServer;
using MBNCSUtil;

namespace AGB.D2;

public class Game
{
	private readonly TraceLog DebugLog;

	private uint RealmLogonCounter = 1u;

	private string RealmServerIp = "";

	private uint ClientToken;

	private uint ServerToken;

	private RealmInfo RealmInfo;

	public ushort GameCounter = 0;

	public D2Socket Socket;

	public Profile Profile;

	public TaskManager TaskManager;

	public Hero Hero;

	public Mercenary Mercenary;

	public int Seed;

	public GameDifficulty Difficulty;

	public GameRooms ActiveRooms;

	public MapManager MapManager;

	public Items Items;

	public Objects Objects;

	public NPCs NPCs;

	public Players Players;

	public Warps Warps;

	public TownPortals TownPortals;

	public event GameEvent VersionChecked;

	public event GameEvent LobbyEntered;

	public event GameEvent GameEntered;

	public event GameEvent GameExited;

	public event GameEvent SeedReceived;

	public event ItemEvent ItemDropped;

	public Game(Profile profile)
	{
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Expected O, but got Unknown
		Profile = profile;
		Socket = new D2Socket();
		TaskManager = new TaskManager();
		MapManager = new MapManager(this);
		ActiveRooms = new GameRooms();
		Items = new Items(this);
		Objects = new Objects(this);
		NPCs = new NPCs(this);
		Players = new Players(this);
		Warps = new Warps(this);
		TownPortals = new TownPortals(this);
		Hero = new Hero(this);
		Mercenary = new Mercenary(this);
		DebugLog = new TraceLog(Directory.GetCurrentDirectory() + "\\Log\\" + Profile.Character.Name + "\\Game.log", DebugLevel.All);
		DebugLog.AddMessage("#########################################################################");
		DebugLog.AddMessage("Debugging started");
		Socket.PacketHandler.AddAsyncListener(BnetServerPacket.BnetPing, BnetPing);
		Socket.PacketHandler.AddAsyncListener(RealmServerPacket.GameList, GameList);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.GameHandshake, GameHandshake);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.LoadAct, LoadAct);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.SkillLog, SkillsLog);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.UpdateSkill, UpdateSkill);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.UpdateQuestInfo, UpdateQuestInfo);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.AssignPlayer, AssignPlayer);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.AssignNPC, AssignNPC);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.AssignGameObject, AssignGameObject);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.AssignWarp, AssignWarp);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.AssignSkill, AssignSkill);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.AssignPlayerCorpse, AssignPlayerCorpse);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.AttributeByte, AttributeByte);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.AttributeWord, AttributeWord);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.AttributeDWord, AttributeDWord);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.MercAttributeDWord, MercAttributeDWord);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.SetNPCMode, SetNPCMode);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.NPCMove, NPCMove);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.NPCMoveToTarget, NPCMoveToTarget);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.NPCAction, NPCAction);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.MonsterAttack, MonsterAttack);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.NPCStop, NPCStop);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.PlayerReassign, PlayerReassign);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.WorldItemAction, WorldItemAction);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.OwnedItemAction, OwnedItemAction);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.LifeManaChange, LifeManaChange);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.PartyMemberUpdate, PartyMemberUpdate);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.MapAdd, MapAdd);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.MapRemove, MapRemove);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.RemoveGroundUnit, RemoveGroundUnit);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.InformationMessage, InformationMessage);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.OpenWaypoint, OpenWaypoint);
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.PortalOwnership, PortalOwnership);
		Socket.PacketHandler.AddAsyncListener(GameClientPacket.RequestReassign, RequestReassign);
		this.GameExited = (GameEvent)Delegate.Combine(this.GameExited, new GameEvent(Game_GameExited));
		Timer pingTimer = new Timer();
		pingTimer.set_Interval(5000);
		EventHandler eventHandler = delegate
		{
			if (Socket.Game != null && Socket.Game.IsConnected)
			{
				SendGamePing();
			}
		};
		pingTimer.add_Tick(eventHandler);
		pingTimer.Start();
	}

	private void Game_GameExited(Game game)
	{
		Hero.Clear();
		Mercenary.Clear();
		Seed = 0;
		Items.Clear();
		Objects.Clear();
		NPCs.Clear();
		Players.Clear();
		Warps.Clear();
		TownPortals.Clear();
		MapManager.Clear();
		ActiveRooms.Clear();
	}

	private void BnetPing(D2Packet args)
	{
		Socket.BattleNet.Send(args.Data);
	}

	private static void GameList(D2Packet args)
	{
		GameList gameList = new GameList(args.Data);
	}

	private void GameHandshake(D2Packet args)
	{
		GameHandshake gameHandshake = new GameHandshake(args.Data);
		Hero.Uid = gameHandshake.UID;
	}

	private void LoadAct(D2Packet args)
	{
		LoadAct loadAct = new LoadAct(args.Data);
		Seed = (int)loadAct.MapId;
		if (this.SeedReceived != null)
		{
			this.SeedReceived(this);
		}
	}

	private void SkillsLog(D2Packet args)
	{
		SkillLog packet = new SkillLog(args.Data);
		BaseSkillLevel[] skills = packet.Skills;
		for (int i = 0; i < skills.Length; i++)
		{
			BaseSkillLevel skillLevel = skills[i];
			Hero.Skills[skillLevel.Skill] = skillLevel.Level;
		}
	}

	private void UpdateSkill(D2Packet args)
	{
		UpdateSkill packet = new UpdateSkill(args.Data);
		if (Hero.Uid == 0 || Hero.Uid == packet.UID)
		{
			Hero.Skills[packet.Skill]++;
		}
	}

	private void UpdateQuestInfo(D2Packet args)
	{
		UpdateQuestInfo packet = new UpdateQuestInfo(args.Data);
		QuestInfo[] quests = packet.Quests;
		foreach (QuestInfo info in quests)
		{
			Hero.Quests[info.Type] = info.Standing;
		}
	}

	private void AssignPlayer(D2Packet args)
	{
		AssignPlayer packet = new AssignPlayer(args.Data);
		Player player = Players.Update(packet);
		if (Hero.Uid == 0 || Hero.Uid == packet.UID)
		{
			Hero.Player = player;
		}
	}

	private void AssignNPC(D2Packet args)
	{
		try
		{
			NPCs.Update(new AssignNPC(args.Data));
		}
		catch (Exception)
		{
			Util.FileAppend("badpackets.txt", "Bad AssignNPC packet: ");
			Util.FileAppend("badpackets.txt", DataFormatter.Format(args.Data));
		}
	}

	private void AssignGameObject(D2Packet args)
	{
		Objects.Update(new AssignGameObject(args.Data));
	}

	private void AssignWarp(D2Packet args)
	{
		Warps.Update(new AssignWarp(args.Data));
	}

	private void AssignSkill(D2Packet args)
	{
		AssignSkill packet = new AssignSkill(args.Data);
		if (Hero.Uid == 0 || Hero.Uid == packet.UID)
		{
			switch (packet.Hand)
			{
			case SkillHand.Left:
				Hero.Left = packet.Skill;
				break;
			case SkillHand.Right:
				Hero.Right = packet.Skill;
				break;
			}
		}
	}

	private void AssignPlayerCorpse(D2Packet args)
	{
		AssignPlayerCorpse packet = new AssignPlayerCorpse(args.Data);
		if (Hero.Uid == packet.PlayerUID && !packet.Assign)
		{
			Hero.CorpseUID = packet.CorpseUID;
		}
	}

	private void AttributeByte(D2Packet args)
	{
		AttributeByte packet = new AttributeByte(args.Data);
		ProcessAttribute(packet.Stat);
	}

	private void AttributeWord(D2Packet args)
	{
		AttributeWord packet = new AttributeWord(args.Data);
		ProcessAttribute(packet.Stat);
	}

	private void AttributeDWord(D2Packet args)
	{
		AttributeDWord packet = new AttributeDWord(args.Data);
		ProcessAttribute(packet.Stat);
	}

	private void ProcessAttribute(StatBase stat)
	{
		int value = (stat.BaseStat.Signed ? ((SignedStat)stat).Value : ((int)((UnsignedStat)stat).Value));
		switch (stat.BaseStat.Type)
		{
		case StatType.Gold:
			Hero.GoldInInventory = value;
			break;
		case StatType.GoldBank:
			Hero.GoldInStash = value;
			break;
		}
	}

	private void MercAttributeDWord(D2Packet args)
	{
		MercAttributeDWord packet = new MercAttributeDWord(args.Data);
		if (Mercenary.Uid == 0)
		{
			Mercenary.Uid = packet.UID;
		}
		switch (packet.Stat.BaseStat.Type)
		{
		case StatType.MaxLife:
			Mercenary.MaxLife = (int)((UnsignedStat)packet.Stat).Value;
			break;
		case StatType.Life:
			Mercenary.Life = (int)((UnsignedStat)packet.Stat).Value;
			break;
		}
	}

	private void SetNPCMode(D2Packet args)
	{
		NPCs.Update(new SetNPCMode(args.Data));
	}

	private void NPCMove(D2Packet args)
	{
		NPCs.Update(new NPCMove(args.Data));
	}

	private void NPCMoveToTarget(D2Packet args)
	{
		NPCs.Update(new NPCMoveToTarget(args.Data));
	}

	private void NPCAction(D2Packet args)
	{
		NPCs.Update(new NPCAction(args.Data));
	}

	private void MonsterAttack(D2Packet args)
	{
		NPCs.Update(new MonsterAttack(args.Data));
	}

	private void NPCStop(D2Packet args)
	{
		NPCs.Update(new NPCStop(args.Data));
	}

	private void PlayerReassign(D2Packet args)
	{
		PlayerReassign packet = new PlayerReassign(args.Data);
		if (Hero.Uid == packet.UID || Hero.Uid == 0)
		{
			Console.WriteLine("Reassigned: " + packet.X + ", " + packet.Y);
			Hero.X = packet.X;
			Hero.Y = packet.Y;
		}
	}

	private void WorldItemAction(D2Packet args)
	{
		try
		{
			WorldItemAction packet = new WorldItemAction(args.Data);
			Item item = Items.Update(packet);
			switch (packet.Action)
			{
			case ItemActionType.PutInBelt:
				Hero.Belt.Add(new Potion(this, item));
				Hero.Items.Update(packet);
				break;
			case ItemActionType.RemoveFromBelt:
				Hero.Belt.Remove(new Potion(this, item));
				Hero.Items.Remove(item.Uid);
				break;
			case ItemActionType.AddToGround:
			case ItemActionType.DropToGround:
			case ItemActionType.OnGround:
				if (this.ItemDropped != null)
				{
					this.ItemDropped(this, item);
				}
				break;
			case ItemActionType.PutInContainer:
				switch (packet.Container)
				{
				case ItemLocation.Inventory:
					Hero.Inventory.AddItem(item);
					break;
				case ItemLocation.Stash:
					Hero.Stash.AddItem(item);
					break;
				case ItemLocation.Cube:
					Hero.Cube.AddItem(item);
					break;
				}
				break;
			}
			if (item.Action.Container == ItemLocation.Inventory || item.Action.Container == ItemLocation.Stash || item.Action.Container == ItemLocation.Cube || item.Action.Container == ItemLocation.Cursor || item.Action.Destination == ItemDestination.Cursor)
			{
				Hero.Items.Update(packet);
			}
		}
		catch (Exception)
		{
			Util.FileAppend("badpackets.txt", "Bad WorldItemAction packet: ");
			Util.FileAppend("badpackets.txt", DataFormatter.Format(args.Data));
		}
	}

	private void OwnedItemAction(D2Packet args)
	{
		try
		{
			OwnedItemAction packet = new OwnedItemAction(args.Data);
			Item item = null;
			if (packet.OwnerUID == Hero.Uid)
			{
				Hero.Items.Update(packet);
			}
			else
			{
				if (packet.OwnerUID != Mercenary.Uid)
				{
					throw new Exception("Item wasn't appended to either hero or merc, fix this!");
				}
				Mercenary.Items.Update(packet);
			}
			ItemActionType action = packet.Action;
			if (action == ItemActionType.RemoveFromContainer)
			{
				switch (packet.Container)
				{
				case ItemLocation.Inventory:
					Hero.Inventory.RemoveItem(item);
					break;
				case ItemLocation.Stash:
					Hero.Stash.RemoveItem(item);
					break;
				case ItemLocation.Cube:
					Hero.Cube.RemoveItem(item);
					break;
				}
			}
		}
		catch (Exception)
		{
			Util.FileAppend("badpackets.txt", "Bad OwnedItemAction packet: ");
			Util.FileAppend("badpackets.txt", DataFormatter.Format(args.Data));
		}
	}

	private void LifeManaChange(D2Packet args)
	{
		LifeManaChange packet = new LifeManaChange(args.Data);
		Hero.Life = packet.Life;
		Hero.Mana = packet.Mana;
		if (Hero.MaxLife < packet.Life)
		{
			Hero.MaxLife = packet.Life;
		}
		if (Hero.MaxMana < packet.Mana)
		{
			Hero.MaxMana = packet.Mana;
		}
	}

	private void PartyMemberUpdate(D2Packet args)
	{
		PartyMemberUpdate packet = new PartyMemberUpdate(args.Data);
		if (Mercenary.Uid == packet.UID)
		{
			Mercenary.Life *= packet.LifePercent / 100;
		}
	}

	private void MapAdd(D2Packet args)
	{
		MapAdd packet = new MapAdd(args.Data);
		GameRoom gameRoom = new GameRoom(packet.X * 5, packet.Y * 5, packet.Area);
		ActiveRooms.Add(gameRoom);
	}

	private void MapRemove(D2Packet args)
	{
		MapRemove packet = new MapRemove(args.Data);
		ActiveRooms.RemoveRoom(packet.X * 5, packet.Y * 5, packet.Area);
	}

	private void RemoveGroundUnit(D2Packet args)
	{
		RemoveGroundUnit packet = new RemoveGroundUnit(args.Data);
		switch (packet.UnitType)
		{
		case UnitType.Player:
			Players.Remove(packet.UID);
			break;
		case UnitType.NPC:
			NPCs.Remove(packet.UID);
			break;
		case UnitType.Warp:
			Warps.Remove(packet.UID);
			break;
		case UnitType.GameObject:
			Objects.Remove(packet.UID);
			TownPortals.Remove(packet.UID);
			break;
		case UnitType.Missile:
		case UnitType.Item:
			break;
		}
	}

	private void InformationMessage(D2Packet args)
	{
		InformationMessage packet = new InformationMessage(args.Data);
	}

	private void OpenWaypoint(D2Packet args)
	{
		OpenWaypoint packet = new OpenWaypoint(args.Data);
		foreach (WaypointsAvailiable waypoint in Enum.GetValues(typeof(WaypointsAvailiable)))
		{
			if (waypoint != WaypointsAvailiable.None && waypoint != WaypointsAvailiable.HaveList && (waypoint & packet.Waypoints) == waypoint)
			{
				WaypointDestination destination = (WaypointDestination)Enum.Parse(typeof(WaypointDestination), waypoint.ToString());
				if (!Hero.AvailableWaypoints.Contains(destination))
				{
					Hero.AvailableWaypoints.Add(destination);
				}
			}
		}
	}

	private void PortalOwnership(D2Packet args)
	{
		TownPortals.Update(new PortalOwnership(args.Data));
	}

	private void RequestReassign(D2Packet args)
	{
		RequestReassign packet = new RequestReassign(args.Data);
		if (Hero.Uid == 0)
		{
			Hero.Uid = packet.MeUID;
		}
	}

	public ConnectResult Connect(CdKeySetProxyCombo combo)
	{
		Profile.CdKeySetProxyCombo = combo;
		ConnectResult result = new ConnectResult();
		if (File.Exists("PVPGNTEST"))
		{
			if (!Socket.BattleNet.Connect("127.0.0.1", 6112))
			{
				return result;
			}
		}
		else if (!Socket.BattleNet.Connect(Profile.Character.Realm.ToString() + ".battle.net", 6112))
		{
			return result;
		}
		Socket.BattleNet.Send(new byte[1] { 1 });
		Socket.BattleNet.Send(new ConnectionRequest().Data);
		D2Packet bnetConnectionResponsePacket = Socket.PacketHandler.WaitForPacket(BnetServerPacket.BnetConnectionResponse, 15000);
		if (bnetConnectionResponsePacket == null)
		{
			return result;
		}
		BnetConnectionResponse bnetConnectionResponse = (result.ConnectionResponse = new BnetConnectionResponse(bnetConnectionResponsePacket.Data));
		ClientToken = (uint)Environment.TickCount;
		ServerToken = (uint)bnetConnectionResponse.ServerToken;
		if (!File.Exists("Game.exe") || !File.Exists("BNClient.dll") || !File.Exists("D2Client.dll"))
		{
			throw new Exception("Missing the files: Game.exe, BNClient.dll or D2Client.dll; make sure those are in the same directory as the exe!  Those are needed to authenticate with Battle.Net!");
		}
		Socket.BattleNet.Send(new AuthorizationCheck(ClientToken, bnetConnectionResponse.VersionFileName, bnetConnectionResponse.VersionFormulae, new string[3] { "Game.exe", "BNClient.dll", "D2Client.dll" }, new CdKey(Profile.CdKeySetProxyCombo.CdKeySet.Classic), new CdKey(Profile.CdKeySetProxyCombo.CdKeySet.Expansion), ServerToken, Util.RandomString(5, 10, "abcdefghijklmnopqrstuvwxyz")).Data);
		D2Packet bnetAuthResponsePacket = Socket.PacketHandler.WaitForPacket(BnetServerPacket.BnetAuthResponse, 15000);
		BnetAuthResponse bnetAuthResponse = (result.AuthResponse = new BnetAuthResponse(bnetAuthResponsePacket.Data));
		DebugLog.AddMessage("BnetAuthResponse received; Result = " + bnetAuthResponse.Result);
		if (bnetAuthResponse.Result == BnetAuthResult.Success && this.VersionChecked != null)
		{
			this.VersionChecked(this);
		}
		return result;
	}

	public void Disconnect()
	{
		if (Socket.BattleNet != null)
		{
			Socket.BattleNet.Close();
		}
		if (Socket.Realm != null)
		{
			Socket.Realm.Close();
		}
		if (Socket.Game != null)
		{
			Socket.Game.Close();
		}
	}

	public BnetLogonResponse Login()
	{
		Socket.BattleNet.Send(new LogonRequest(Profile.Username, Profile.Password, ClientToken, ServerToken).Data);
		D2Packet packet = Socket.PacketHandler.WaitForPacket(BnetServerPacket.BnetLogonResponse, 15000);
		if (packet == null)
		{
			return null;
		}
		return new BnetLogonResponse(packet.Data);
	}

	public RealmConnectResult RealmConnect()
	{
		return RealmConnect(skipQueries: false);
	}

	public RealmConnectResult RealmConnect(bool skipQueries)
	{
		RealmConnectResult result = new RealmConnectResult();
		if (!skipQueries)
		{
			Socket.BattleNet.Send(new BncsPacket(64).GetData());
			D2Packet queryRealmsResponsePacket = Socket.PacketHandler.WaitForPacket(BnetServerPacket.QueryRealmsResponse, 15000);
			QueryRealmsResponse queryRealmsResponse = new QueryRealmsResponse(queryRealmsResponsePacket.Data);
			result.QueryRealmsResponse = queryRealmsResponse;
			RealmInfo = queryRealmsResponse.Realms[0];
		}
		RealmLogonCounter = 1u;
		Socket.BattleNet.Send(new RealmLogon(RealmInfo.Name, RealmLogonCounter++, ServerToken).Data);
		PacketEventHandler realmLogonResponseDelg = null;
		realmLogonResponseDelg = ((!File.Exists("PVPGNTEST")) ? ((PacketEventHandler)delegate(D2Packet packet)
		{
			RealmLogonResponse realmLogonResponse = new RealmLogonResponse(packet.Data);
			result.RealmLogonResponse = realmLogonResponse;
			if (realmLogonResponse.Result == RealmLogonResult.Success)
			{
				RealmServerIp = realmLogonResponse.RealmServerIP.ToString();
				if (Socket.Realm.IsConnected)
				{
					Console.WriteLine("***** big flaw here: ");
				}
				if (Socket.Realm.Connect(RealmServerIp, 6112) && Socket.Realm.IsConnected)
				{
					try
					{
						Socket.Realm.Send(new byte[1] { 1 });
					}
					catch (NullReferenceException)
					{
						return;
					}
					Socket.Realm.Send(new Logon(realmLogonResponse.Cookie, realmLogonResponse.Status, realmLogonResponse.MCPChunk1, realmLogonResponse.MCPChunk2, realmLogonResponse.Username).Data);
				}
			}
		}) : ((PacketEventHandler)delegate(D2Packet packet)
		{
			Console.WriteLine(DataFormatter.Format(packet.Data).ToString());
			DataReader dataReader = new DataReader(packet.Data);
			uint cookie = 0u;
			uint status = 0u;
			uint[] mcpChunk = null;
			byte[] array = null;
			string ip = "";
			byte[] array2 = null;
			int port = 0;
			uint[] mcpChunk2 = null;
			string bncsUniqueName = "";
			if (dataReader.Length - 4 > 8)
			{
				dataReader.ReadInt32();
				cookie = (uint)dataReader.ReadInt32();
				status = (uint)dataReader.ReadInt32();
				mcpChunk = dataReader.ReadUInt32Array(2);
				array = dataReader.ReadByteArray(4);
				ip = array[0] + "." + array[1] + "." + array[2] + "." + array[3];
				array2 = dataReader.ReadByteArray(4);
				port = array2[0] * 256 + array2[1];
				mcpChunk2 = dataReader.ReadUInt32Array(12);
				bncsUniqueName = dataReader.ReadCString();
			}
			if (Socket.Realm.IsConnected)
			{
				Console.WriteLine("***** big flaw here: ");
			}
			if (Socket.Realm.Connect(ip, port) && Socket.Realm.IsConnected)
			{
				try
				{
					Socket.Realm.Send(new byte[1] { 1 });
				}
				catch (NullReferenceException)
				{
					return;
				}
				Socket.Realm.Send(new Logon(cookie, status, mcpChunk, mcpChunk2, bncsUniqueName).Data);
			}
		}));
		Socket.PacketHandler.AddAsyncListener(BnetServerPacket.RealmLogonResponse, realmLogonResponseDelg);
		PacketEventHandler realmStartupResponseDelg = delegate(D2Packet packet)
		{
			RealmStartupResponse realmStartupResponse = new RealmStartupResponse(packet.Data);
			result.RealmStartupResponse = realmStartupResponse;
			if (realmStartupResponse.Result == RealmStartupResult.Success)
			{
				Socket.Realm.Send(new CharacterLogon(Profile.Character.Name).Data);
			}
		};
		Socket.PacketHandler.AddAsyncListener(RealmServerPacket.RealmStartupResponse, realmStartupResponseDelg);
		PacketEventHandler characterLogonResponseDelg = delegate(D2Packet packet)
		{
			CharacterLogonResponse characterLogonResponse = new CharacterLogonResponse(packet.Data);
			result.CharacterLogonResponse = characterLogonResponse;
			if (characterLogonResponse.Result == RealmCharacterActionResult.Success)
			{
				Socket.BattleNet.Send(new ChannelListRequest(Profile.Client).Data);
				Socket.BattleNet.Send(new EnterChatRequest(Profile.Character).Data);
			}
		};
		Socket.PacketHandler.AddAsyncListener(RealmServerPacket.CharacterLogonResponse, characterLogonResponseDelg);
		DateTime startWatching = DateTime.Now;
		while (DateTime.Now.Subtract(startWatching).TotalMilliseconds < 20000.0 && !result.HasCompletedSuccessfully && !result.HasFailed)
		{
			Thread.Sleep(10);
		}
		if (!result.HasFailed && this.LobbyEntered != null)
		{
			this.LobbyEntered(this);
		}
		Socket.PacketHandler.RemoveAsyncListener(BnetServerPacket.RealmLogonResponse, realmLogonResponseDelg);
		Socket.PacketHandler.RemoveAsyncListener(RealmServerPacket.RealmStartupResponse, realmStartupResponseDelg);
		Socket.PacketHandler.RemoveAsyncListener(RealmServerPacket.CharacterLogonResponse, characterLogonResponseDelg);
		return result;
	}

	public EnterGameResult CreateGame(GameDifficulty difficulty)
	{
		return CreateGame(difficulty, Util.RandomString(5, 15), Util.RandomString(3, 8));
	}

	public EnterGameResult CreateGame(GameDifficulty difficulty, string name, string pass)
	{
		if (pass == null)
		{
			pass = "";
		}
		EnterGameResult result = new EnterGameResult();
		if (!Socket.Realm.IsConnected)
		{
			return result;
		}
		name = name.ToUpper();
		pass = pass.ToUpper();
		GameCounter += 2;
		Socket.Realm.Send(new CreateGame(GameCounter, difficulty, name, pass, "").Data);
		D2Packet packet = Socket.PacketHandler.WaitForPacket(RealmServerPacket.CreateGameResponse, 15000);
		if (packet == null)
		{
			BotManager.Instance.EnterGame(Profile.Character.Realm, Profile.CdKeySetProxyCombo);
			return result;
		}
		if ((result.CreateGameResponse = new CreateGameResponse(packet.Data)).Result != 0)
		{
			BotManager.Instance.EnterGame(Profile.Character.Realm, Profile.CdKeySetProxyCombo);
			return result;
		}
		Socket.BattleNet.Send(new StartGame(name, pass).Data);
		EnterGameResult joinGameResult = JoinGame(name, pass);
		joinGameResult.CreateGameResponse = result.CreateGameResponse;
		return joinGameResult;
	}

	public EnterGameResult JoinGame(string name)
	{
		return JoinGame(name, "");
	}

	public EnterGameResult JoinGame(string name, string pass)
	{
		BotManager.Instance.EnterGame(Profile.Character.Realm, Profile.CdKeySetProxyCombo);
		EnterGameResult result = new EnterGameResult();
		if (Socket.Game.IsConnected)
		{
			Socket.Game.Close();
		}
		Socket.Realm.Send(new JoinGame((ushort)(GameCounter + 1), name, pass).Data);
		PacketEventHandler joinGameResponseDelg = delegate(D2Packet packet)
		{
			JoinGameResponse joinGameResponse = new JoinGameResponse(packet.Data);
			result.JoinGameResponse = joinGameResponse;
			if (joinGameResponse.Result != 0)
			{
				return;
			}
			Socket.BattleNet.Send(new NotifyJoin(name, pass).Data);
			Socket.BattleNet.Send(new LeaveChat().Data);
			Socket.Realm.Close();
			try
			{
				Socket.Game.Connect(joinGameResponse.GameServerIP, 4000);
			}
			catch (Exception)
			{
				Console.WriteLine("fasdafsasdfsdf socket game connect failed");
			}
		};
		Socket.PacketHandler.AddAsyncListener(RealmServerPacket.JoinGameResponse, joinGameResponseDelg);
		PacketEventHandler requestLogonInfoDelg = delegate(D2Packet packet)
		{
			RequestLogonInfo requestLogonInfo = new RequestLogonInfo(packet.Data);
			result.RequestLogonInfo = requestLogonInfo;
			Socket.Game.Send(new GameLogon(result.JoinGameResponse.GameHash, result.JoinGameResponse.GameToken, Profile.Character.Class, Profile.Character.Name).Data);
		};
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.RequestLogonInfo, requestLogonInfoDelg);
		PacketEventHandler gamelogonReceiptDelg = delegate(D2Packet packet)
		{
			GameLogonReceipt gameLogonReceipt = new GameLogonReceipt(packet.Data);
			result.GameLogonReceipt = gameLogonReceipt;
		};
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.GameLogonReceipt, gamelogonReceiptDelg);
		PacketEventHandler gameLogonSuccessDelg = delegate(D2Packet packet)
		{
			GameLogonSuccess gameLogonSuccess = new GameLogonSuccess(packet.Data);
			result.GameLogonSuccess = gameLogonSuccess;
			Socket.Game.Send(new byte[1] { 107 });
		};
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.GameLogonSuccess, gameLogonSuccessDelg);
		DateTime startWatching = DateTime.Now;
		while (DateTime.Now.Subtract(startWatching).TotalMilliseconds < 20000.0 && !result.HasCompletedSuccessfully && !result.HasFailed)
		{
			Thread.Sleep(10);
		}
		if (result.HasCompletedSuccessfully)
		{
			Difficulty = result.GameLogonReceipt.Difficulty;
			AsyncHelper.FireAsync(this.GameEntered, this);
		}
		else
		{
			Console.WriteLine("HasCompletedSuccessfully == false -----------------------------------------------");
		}
		Socket.PacketHandler.RemoveAsyncListener(RealmServerPacket.JoinGameResponse, joinGameResponseDelg);
		Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.RequestLogonInfo, requestLogonInfoDelg);
		Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.GameLogonReceipt, gamelogonReceiptDelg);
		Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.GameLogonSuccess, gameLogonSuccessDelg);
		return result;
	}

	public bool JoinTCPGame(string GameServerIP, string D2SFileName, string charName, D2Data.CharacterClass charClass)
	{
		EnterGameResult result = new EnterGameResult();
		if (Socket.Game.IsConnected)
		{
			Socket.Game.Close();
		}
		Console.WriteLine("Connecting to game server: " + GameServerIP);
		if (!Socket.Game.Connect(GameServerIP, 4000))
		{
			Console.WriteLine("Couldn't connect to game server!");
			return false;
		}
		PacketEventHandler requestLogonInfoDelg = delegate(D2Packet packet)
		{
			RequestLogonInfo requestLogonInfo = new RequestLogonInfo(packet.Data);
			result.RequestLogonInfo = requestLogonInfo;
			Socket.Game.Send(new GameLogon(0u, 1, charClass, charName).Data);
		};
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.RequestLogonInfo, requestLogonInfoDelg);
		PacketEventHandler gamelogonReceiptDelg = delegate(D2Packet packet)
		{
			GameLogonReceipt gameLogonReceipt = new GameLogonReceipt(packet.Data);
			result.GameLogonReceipt = gameLogonReceipt;
		};
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.GameLogonReceipt, gamelogonReceiptDelg);
		PacketEventHandler gameLogonSuccessDelg = delegate(D2Packet packet)
		{
			GameLogonSuccess gameLogonSuccess = new GameLogonSuccess(packet.Data);
			FileStream fileStream = File.Open(D2SFileName, FileMode.OpenOrCreate, FileAccess.Read);
			byte[] array = new byte[fileStream.Length];
			int num = (int)fileStream.Length;
			fileStream.Read(array, 0, (int)fileStream.Length);
			fileStream.Close();
			int num2 = 0;
			do
			{
				int num3 = num - num2;
				if (num3 > 255)
				{
					num3 = 255;
				}
				byte[] array2 = new byte[7 + num3];
				array2[0] = 108;
				array2[1] = (byte)num3;
				array2[2] = (byte)num;
				array2[3] = (byte)(num >> 8);
				array2[4] = (byte)(num >> 16);
				array2[5] = (byte)(num >> 24);
				Buffer.BlockCopy(array, num2, array2, 6, num3);
				array2[5 + num3 + 1] = 0;
				Socket.Game.Send(array2);
				Thread.Sleep(250);
				num2 += num3;
			}
			while (num2 < num);
			Socket.Game.Send(new byte[1] { 107 });
			result.GameLogonSuccess = gameLogonSuccess;
		};
		Socket.PacketHandler.AddAsyncListener(GameServerPacket.GameLogonSuccess, gameLogonSuccessDelg);
		DateTime startWatching = DateTime.Now;
		while (DateTime.Now.Subtract(startWatching).TotalMilliseconds < 60000.0 && !result.HasCompletedSuccessfully && !result.HasFailed)
		{
			Thread.Sleep(10);
		}
		if (!result.HasFailed)
		{
			Difficulty = result.GameLogonReceipt.Difficulty;
			AsyncHelper.FireAsync(this.GameEntered, this);
		}
		Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.RequestLogonInfo, requestLogonInfoDelg);
		Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.GameLogonReceipt, gamelogonReceiptDelg);
		Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.GameLogonSuccess, gameLogonSuccessDelg);
		return result.GameLogonSuccess != null;
	}

	public void LeaveGame()
	{
		if (Socket.Game.IsConnected)
		{
			Socket.Game.Send(new byte[1] { 105 });
		}
		Socket.Game.Close();
		Socket.BattleNet.Send(new byte[4] { 255, 31, 4, 0 });
		DebugLog.AddMessage("Left game!");
		TaskManager.Clear();
		if (this.GameExited != null)
		{
			this.GameExited(this);
		}
	}

	public void SendGamePing()
	{
		Socket.Game.Send(new Ping((uint)DateTime.Now.Ticks, 0L).Data);
	}

	public void SendGameMessage(string text)
	{
		Socket.Game.Send(new SendMessage(GameMessageType.GameMessage, text).Data);
	}

	public bool IdentifyWait(Item item, int timeout)
	{
		if (item.Action.Container != ItemLocation.Inventory)
		{
			return false;
		}
		uint IdentifyScroll = 0u;
		uint ItemToID = item.Uid;
		foreach (Item theItem in Items.GetCopy())
		{
			if (theItem.Action.BaseItem.Class == ItemClass.TomeOfIdentify)
			{
				IdentifyScroll = theItem.Uid;
			}
		}
		if (IdentifyScroll == 0)
		{
			return false;
		}
		Socket.Game.Send(new UseContainerItem(IdentifyScroll, Hero.X, Hero.Y).Data);
		D2Packet packet = Socket.PacketHandler.WaitForPacket(GameServerPacket.UseStackableItem, timeout);
		if (packet == null)
		{
			return false;
		}
		UseStackableItem it = new UseStackableItem(packet.Data);
		if (it.UID != IdentifyScroll)
		{
			return false;
		}
		Socket.Game.Send(new IdentifyItem(ItemToID, IdentifyScroll).Data);
		packet = Socket.PacketHandler.WaitForPacket(GameServerPacket.OwnedItemAction, timeout);
		if (packet == null)
		{
			return false;
		}
		OwnedItemAction action = new OwnedItemAction(packet.Data);
		return action.OwnerUID == Hero.Uid;
	}
}
