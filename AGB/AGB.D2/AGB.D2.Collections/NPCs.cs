using System;
using System.Collections.Generic;
using System.Threading;
using AGB.Collections;
using D2Data;
using D2Packets.GameServer;

namespace AGB.D2.Collections;

public class NPCs : ThreadSafeList<NPC>
{
	private Game Game;

	public NPCs(Game game)
	{
		Game = game;
	}

	public void Remove(uint uid)
	{
		lock (PadLock)
		{
			NPC npc = ItemList.Find((NPC n) => n.Uid == uid);
			if (npc != null)
			{
				ItemList.Remove(npc);
			}
		}
	}

	public void Update(AssignNPC assignNpc)
	{
		lock (PadLock)
		{
			NPC npc = ItemList.Find((NPC n) => n.Uid == assignNpc.UID);
			if (npc == null)
			{
				npc = new NPC(Game);
				ItemList.Add(npc);
			}
			npc.Uid = assignNpc.UID;
			npc.Id = assignNpc.ID;
			npc.Life = assignNpc.Life;
			npc.X = assignNpc.X;
			npc.Y = assignNpc.Y;
		}
	}

	public void Update(NPCStop npcStop)
	{
		lock (PadLock)
		{
			NPC npc = ItemList.Find((NPC n) => n.Uid == npcStop.UID);
			if (npc == null)
			{
				npc = new NPC(Game);
				ItemList.Add(npc);
			}
			npc.Uid = npcStop.UID;
			npc.X = npcStop.X;
			npc.Y = npcStop.Y;
			npc.Life = npcStop.Life;
		}
	}

	public void Update(MonsterAttack monsterAttack)
	{
		lock (PadLock)
		{
			NPC npc = ItemList.Find((NPC n) => n.Uid == monsterAttack.UID);
			if (npc == null)
			{
				npc = new NPC(Game);
				ItemList.Add(npc);
			}
			npc.Uid = monsterAttack.UID;
			npc.X = monsterAttack.X;
			npc.Y = monsterAttack.Y;
		}
	}

	public void Update(NPCMoveToTarget npcMoveToTarget)
	{
		lock (PadLock)
		{
			NPC npc = ItemList.Find((NPC n) => n.Uid == npcMoveToTarget.UID);
			if (npc == null)
			{
				npc = new NPC(Game);
				ItemList.Add(npc);
			}
			npc.Uid = npcMoveToTarget.UID;
			npc.X = npcMoveToTarget.CurrentX;
			npc.Y = npcMoveToTarget.CurrentY;
		}
	}

	public void Update(NPCAction npcAction)
	{
		lock (PadLock)
		{
			NPC npc = ItemList.Find((NPC n) => n.Uid == npcAction.UID);
			if (npc == null)
			{
				npc = new NPC(Game);
				ItemList.Add(npc);
			}
			npc.Uid = npcAction.UID;
			npc.X = npcAction.X;
			npc.Y = npcAction.Y;
		}
	}

	public void Update(NPCMove npcMove)
	{
		lock (PadLock)
		{
			NPC npc = ItemList.Find((NPC n) => n.Uid == npcMove.UID);
			if (npc == null)
			{
				npc = new NPC(Game);
				ItemList.Add(npc);
			}
			npc.Uid = npcMove.UID;
			npc.X = npcMove.X;
			npc.Y = npcMove.Y;
		}
	}

	public void Update(SetNPCMode setNPCMode)
	{
		lock (PadLock)
		{
			NPC npc = ItemList.Find((NPC n) => n.Uid == setNPCMode.UID);
			if (npc == null)
			{
				npc = new NPC(Game);
				ItemList.Add(npc);
			}
			npc.Uid = setNPCMode.UID;
			npc.X = setNPCMode.X;
			npc.Y = setNPCMode.Y;
			npc.State = setNPCMode.Mode;
			npc.Life = setNPCMode.Life;
		}
	}

	public NPC Find(NPCClass id, int timeOut)
	{
		DateTime start = DateTime.Now;
		while (DateTime.Now.Subtract(start).TotalMilliseconds < (double)timeOut)
		{
			NPC npc = Find(id);
			if (npc != null)
			{
				return npc;
			}
			Thread.Sleep(100);
		}
		return null;
	}

	public NPC Find(NPCClass id)
	{
		lock (PadLock)
		{
			return Find((NPC n) => n.Id == id);
		}
	}

	public List<NPC> FindAll(NPCClass id, int timeOut)
	{
		DateTime start = DateTime.Now;
		while (DateTime.Now.Subtract(start).TotalMilliseconds < (double)timeOut)
		{
			List<NPC> npcs = FindAll(id);
			if (npcs != null && npcs.Count != 0)
			{
				return npcs;
			}
			Thread.Sleep(100);
		}
		return null;
	}

	public List<NPC> FindAll(NPCClass id)
	{
		lock (PadLock)
		{
			return FindAll((NPC n) => n.Id == id);
		}
	}
}
