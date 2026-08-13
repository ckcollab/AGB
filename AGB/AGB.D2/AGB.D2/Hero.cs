using System;
using System.Collections.Generic;
using System.Threading;
using AGB.D2.Collections;
using D2Data;
using D2Packets.D2Packets;
using D2Packets.GameClient;
using D2Packets.GameServer;

namespace AGB.D2;

public class Hero : NPC
{
	public SkillType Left = SkillType.Attack;

	public SkillType Right = SkillType.Attack;

	public Items Items;

	public List<Potion> Belt = new List<Potion>();

	public ItemBuffer Stash = new ItemBuffer(6, 8);

	public ItemBuffer Inventory = new ItemBuffer(10, 4);

	public ItemBuffer Cube = new ItemBuffer(3, 4);

	public Dictionary<SkillType, int> Skills = new Dictionary<SkillType, int>();

	public uint CorpseUID = 0u;

	public int GoldInStash = 0;

	public int GoldInInventory = 0;

	public Player Player;

	public List<WaypointDestination> AvailableWaypoints = new List<WaypointDestination>();

	public Dictionary<QuestType, QuestStanding> Quests = new Dictionary<QuestType, QuestStanding>();

	public bool IsStuck = false;

	public string Name
	{
		get
		{
			if (Player == null)
			{
				return null;
			}
			return Player.Name;
		}
	}

	public CharacterClass Class
	{
		get
		{
			if (Player == null)
			{
				return CharacterClass.Any;
			}
			return Player.Class;
		}
	}

	public int GoldTotal => GoldInInventory + GoldInStash;

	public AreaLevel AreaLevel
	{
		get
		{
			if (Game.ActiveRooms.GetCurrentAreaLevel(X, Y) == AreaLevel.None)
			{
				int alertme = 1;
				alertme++;
			}
			return Game.ActiveRooms.GetCurrentAreaLevel(X, Y);
		}
	}

	public ActLevel Act
	{
		get
		{
			int actNumber = -1;
			if (AreaLevel <= AreaLevel.MooMooFarm)
			{
				return ActLevel.Act1;
			}
			if (AreaLevel <= AreaLevel.ArcaneSanctuary)
			{
				return ActLevel.Act2;
			}
			if (AreaLevel <= AreaLevel.DuranceOfHateLevel3)
			{
				return ActLevel.Act3;
			}
			if (AreaLevel <= AreaLevel.ChaosSanctuary)
			{
				return ActLevel.Act4;
			}
			return ActLevel.Act5;
		}
	}

	public bool IsInTown => AreaLevel == AreaLevel.None || AreaLevel == AreaLevel.RogueEncampment || AreaLevel == AreaLevel.LutGholein || AreaLevel == AreaLevel.KurastDocks || AreaLevel == AreaLevel.ThePandemoniumFortress || AreaLevel == AreaLevel.Harrogath;

	public int LowestDurability
	{
		get
		{
			int lowest = int.MaxValue;
			foreach (Item item in Items.GetFromContainer(ItemLocation.Equipment))
			{
				if (item.Action.BaseItem.DurabilityWarning < lowest)
				{
					lowest = item.Action.BaseItem.DurabilityWarning;
				}
			}
			return lowest;
		}
	}

	public bool HasTeleport => Skills[SkillType.Teleport] > 0;

	public Item ItemOnCursor => Items.Find((Item i) => i.Action.Container == ItemLocation.Cursor || i.Action.Destination == ItemDestination.Cursor);

	public Hero(Game game)
		: base(game)
	{
		foreach (SkillType type2 in Enum.GetValues(typeof(SkillType)))
		{
			if (!Skills.ContainsKey(type2))
			{
				Skills.Add(type2, 0);
			}
		}
		foreach (QuestType type in Enum.GetValues(typeof(QuestType)))
		{
			if (!Quests.ContainsKey(type))
			{
				Quests.Add(type, (QuestStanding)0);
			}
		}
		Items = new Items(game);
	}

	public override void Clear()
	{
		base.Clear();
		Items.Clear();
		Belt.Clear();
	}

	private bool IsSkillSelected(SkillHand hand, SkillType type)
	{
		switch (hand)
		{
		case SkillHand.Left:
			if (Left == type)
			{
				return true;
			}
			break;
		case SkillHand.Right:
			if (Right == type)
			{
				return true;
			}
			break;
		}
		return false;
	}

	public SkillType GetBestMaxedSkill()
	{
		if (Skills[SkillType.BlessedHammer] >= 20)
		{
			return SkillType.BlessedHammer;
		}
		if (Skills[SkillType.Lightning] >= 20)
		{
			return SkillType.Lightning;
		}
		if (Skills[SkillType.Blizzard] >= 20)
		{
			return SkillType.Blizzard;
		}
		if (Skills[SkillType.FrozenOrb] >= 20)
		{
			return SkillType.FrozenOrb;
		}
		if (Skills[SkillType.Meteor] >= 20)
		{
			return SkillType.Meteor;
		}
		return SkillType.None;
	}

	public Item GetItemAt(EquipmentLocation location)
	{
		foreach (Item item in Items.GetFromContainer(ItemLocation.Equipment))
		{
			if (item.Action.EquipmentLocation == location)
			{
				return item;
			}
		}
		return null;
	}

	public int GetDistanceFrom(Unit unit)
	{
		return GetDistanceFrom(unit.X, unit.Y);
	}

	public int GetDistanceFrom(int x, int y)
	{
		return Math.Abs((int)Math.Sqrt(Math.Pow(x - X, 2.0) + Math.Pow(y - Y, 2.0)));
	}

	public bool SelectSkill(SkillHand hand, SkillType type)
	{
		if (!IsSkillSelected(hand, type))
		{
			Game.Socket.Game.Send(new SelectSkill(type, hand).Data);
			return Game.Socket.PacketHandler.WaitForPacket(GameServerPacket.AssignSkill, 5000) == null;
		}
		return true;
	}

	public void Attack(NPC npc, SkillType skill)
	{
		Attack(npc, skill, SkillHand.Left);
	}

	public void Attack(NPC npc, SkillType skill, SkillHand hand)
	{
		SelectSkill(hand, skill);
		switch (hand)
		{
		case SkillHand.Left:
			CastLeftSkill(npc);
			break;
		case SkillHand.Right:
			CastRightSkill(npc);
			break;
		}
	}

	public void Attack(int x, int y, SkillType skill, SkillHand hand)
	{
		SelectSkill(hand, skill);
		switch (hand)
		{
		case SkillHand.Left:
			CastLeftSkill(x, y);
			break;
		case SkillHand.Right:
			CastRightSkill(x, y);
			break;
		}
	}

	public bool TeleportWait(int x, int y, int timeout)
	{
		SelectSkill(SkillHand.Right, SkillType.Teleport);
		CastRightSkill(x, y);
		return WaitForReassign(timeout);
	}

	public bool TelekinesisItemWait(Item item, int timeout)
	{
		SelectSkill(SkillHand.Right, SkillType.Telekinesis);
		Game.Socket.Game.Send(new CastRightSkillOnTarget(UnitType.Item, item.Uid).Data);
		DateTime start = DateTime.Now;
		RemoveGroundUnit rgu;
		do
		{
			bool flag = true;
			if (DateTime.Now.Subtract(start).TotalMilliseconds > (double)timeout)
			{
				return false;
			}
			D2Packet packet = Game.Socket.PacketHandler.WaitForPacket(GameServerPacket.RemoveGroundUnit, timeout);
			if (packet == null)
			{
				return false;
			}
			rgu = new RemoveGroundUnit(packet.Data);
		}
		while (rgu.UnitType != UnitType.Item || rgu.UID != item.Uid);
		return true;
	}

	public void Move(int x, int y)
	{
		X = x;
		Y = y;
		Game.Socket.Game.Send(new RunToLocation((ushort)x, (ushort)y).Data);
	}

	public void Move(Unit unit)
	{
		if (unit == null)
		{
			throw new ArgumentException("Move(unit) -- unit was null!  I can't move to a unit that doesn't exist, check if it exists first!");
		}
		X = unit.X;
		Y = unit.Y;
		Game.Socket.Game.Send(new RunToLocation((ushort)unit.X, (ushort)unit.Y).Data);
		Game.Socket.Game.Send(new RunToTarget(UnitType.NPC, unit.Uid).Data);
	}

	public bool MoveWait(int x, int y, int timeout)
	{
		Move(x, y);
		DateTime start = DateTime.Now;
		WalkVerify verify;
		do
		{
			bool flag = true;
			if (DateTime.Now.Subtract(start).TotalMilliseconds > (double)timeout)
			{
				return false;
			}
			D2Packet packet = Game.Socket.PacketHandler.WaitForPacket(GameServerPacket.WalkVerify, timeout);
			if (packet == null)
			{
				return false;
			}
			verify = new WalkVerify(packet.Data);
		}
		while (verify.X != x || verify.Y != y);
		return true;
	}

	public bool MoveWaitStatic(int x2, int y2, int delay)
	{
		double distance = Math.Sqrt(Math.Pow(x2 - X, 2.0) + Math.Pow(y2 - Y, 2.0));
		if (distance > 25.0)
		{
			return false;
		}
		Map map = Game.MapManager.GetMap(AreaLevel);
		if (map == null)
		{
			return false;
		}
		List<PathNode> walkpath = map.GetWalkPath(X, Y, x2, y2, 1);
		Move(x2, y2);
		Thread.Sleep(delay * walkpath.Count);
		return true;
	}

	public void CastLeftSkill(int x, int y)
	{
		Game.Socket.Game.Send(new CastLeftSkill((ushort)x, (ushort)y).Data);
	}

	public void CastLeftSkill(NPC unit)
	{
		Game.Socket.Game.Send(new CastLeftSkillOnTarget(UnitType.NPC, unit.Uid).Data);
	}

	public void CastRightSkill(int x, int y)
	{
		Game.Socket.Game.Send(new CastRightSkill((ushort)x, (ushort)y).Data);
	}

	public void CastRightSkill(NPC unit)
	{
		Game.Socket.Game.Send(new CastRightSkillOnTarget(UnitType.NPC, unit.Uid).Data);
	}

	public bool WaitForReassign(int timeOut)
	{
		bool receivedValidReassign = false;
		PacketEventHandler playerReassignDelg = delegate(D2Packet packet)
		{
			PlayerReassign playerReassign = new PlayerReassign(packet.Data);
			if (playerReassign.UID == Game.Hero.Uid)
			{
				receivedValidReassign = true;
			}
		};
		Game.Socket.PacketHandler.AddAsyncListener(GameServerPacket.PlayerReassign, playerReassignDelg);
		DateTime watchstart = DateTime.Now;
		while (DateTime.Now.Subtract(watchstart).TotalMilliseconds < (double)timeOut && !receivedValidReassign)
		{
			Thread.Sleep(1);
		}
		Game.Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.PlayerReassign, playerReassignDelg);
		return receivedValidReassign;
	}

	public bool WaitForReassign(AreaLevel level, int timeOut)
	{
		if (!WaitForReassign(timeOut))
		{
			return false;
		}
		DateTime watchstart = DateTime.Now;
		while (DateTime.Now.Subtract(watchstart).TotalMilliseconds < (double)timeOut)
		{
			if (AreaLevel == level)
			{
				return true;
			}
			Thread.Sleep(1);
		}
		return false;
	}

	public bool DrinkPotion(PotionType type, int timeOut)
	{
		return DrinkPotion(toMerc: false, type, timeOut);
	}

	public bool DrinkPotion(bool toMerc, PotionType type, int timeOut)
	{
		Belt.Sort(Potion.Compare);
		Belt.Reverse();
		return Belt.Find((Potion p) => p.Type == type)?.Drink(toMerc, timeOut) ?? false;
	}

	public override string ToString()
	{
		return string.Concat("Life: ", base.Life, "/", MaxLife, Environment.NewLine, "Mana: ", base.Mana, "/", MaxMana, Environment.NewLine, "UID: ", Uid, Environment.NewLine, "Coords: ", X, ", ", Y, Environment.NewLine, Environment.NewLine, "Skill: ", Left, " | ", Right, Environment.NewLine);
	}
}
