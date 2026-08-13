using System;
using System.Threading;
using D2Data;
using D2Packets.D2Packets;
using D2Packets.GameClient;
using D2Packets.GameServer;

namespace AGB.D2;

public class NPC : Unit
{
	private int m_Life;

	private int m_Mana;

	public DateTime LastDrink = DateTime.Now;

	public bool IsMercenary = false;

	public NPCClass Id;

	public int MaxLife = 128;

	public int MaxMana;

	public NPCMode State;

	public Resistances Resistances;

	public bool IsMoving = false;

	private DateTime MoveTime;

	private int MoveRemainingTime;

	private int MovingSpeed;

	private int MovingToX;

	private int MovingToY;

	public bool IsAlive => State != NPCMode.Dead && State != NPCMode.Dying;

	public int MovingX
	{
		get
		{
			if (IsMoving)
			{
				UpdateMovingPosition();
			}
			return X;
		}
	}

	public int MovingY
	{
		get
		{
			if (IsMoving)
			{
				UpdateMovingPosition();
			}
			return Y;
		}
	}

	public int LifeAsPercent => (int)((double)m_Life / 1.28);

	public int Life
	{
		get
		{
			return m_Life;
		}
		set
		{
			if (this.LifeChanged != null)
			{
				this.LifeChanged(this, m_Life, value);
			}
			m_Life = value;
		}
	}

	public int Mana
	{
		get
		{
			return m_Mana;
		}
		set
		{
			if (this.ManaChanged != null)
			{
				this.ManaChanged(this, m_Mana, value);
			}
			m_Mana = value;
		}
	}

	public event NpcPropertyEventHandler LifeChanged;

	public event NpcPropertyEventHandler ManaChanged;

	public NPC(Game game)
		: base(game)
	{
		Game = game;
		Uid = 0u;
		Life = -1;
		Mana = -1;
		MaxLife = -1;
		MaxMana = -1;
		State = NPCMode.Alive;
	}

	public void StartMoving(int speed, int toX, int toY)
	{
		IsMoving = true;
		MoveTime = DateTime.Now;
		MovingSpeed = speed;
		MovingToX = toX;
		MovingToY = toY;
	}

	private void UpdateMovingPosition()
	{
		int walkedTime = (int)Math.Truncate(DateTime.Now.Subtract(MoveTime).TotalMilliseconds) + MoveRemainingTime;
		int walkedTime2 = (int)Math.Truncate((double)walkedTime / 500.0);
		if (walkedTime2 == 0)
		{
			return;
		}
		MoveRemainingTime = walkedTime - walkedTime2 * 500;
		MoveTime = DateTime.Now;
		int xdif = MovingToX - X;
		int ydif = MovingToY - Y;
		int h = (int)Math.Sqrt(ydif * ydif + xdif * xdif);
		int a = walkedTime2 * MovingSpeed / 32;
		if (a > h)
		{
			IsMoving = false;
			X = MovingToX;
			Y = MovingToY;
			return;
		}
		double Xrel = 0.0;
		double Yrel = 0.0;
		int xmov = 0;
		int ymov = 0;
		if (xdif != 0)
		{
			Xrel = h / xdif;
			xmov = a / (int)Xrel;
		}
		if (ydif != 0)
		{
			Yrel = h / ydif;
			ymov = a / (int)Yrel;
		}
		X += xmov;
		Y += ymov;
	}

	public void BuyItem(Item item)
	{
		Game.Socket.Game.Send(new BuyItem(Uid, item.Uid, (uint)item.Action.BaseItem.Cost, BuyFlags.None).Data);
	}

	private bool InteractWithTownFolk(int timeOut)
	{
		bool interacted = false;
		PacketEventHandler npcInfoDelg = delegate
		{
			interacted = true;
		};
		Game.Socket.PacketHandler.AddAsyncListener(GameServerPacket.NPCInfo, npcInfoDelg);
		Game.Socket.Game.Send(new GoToTownFolk(UnitType.NPC, Uid, (uint)X, (uint)Y).Data);
		Thread.Sleep(50);
		Game.Socket.Game.Send(new RunToTarget(UnitType.NPC, Uid).Data);
		Game.Socket.Game.Send(new UnitInteract(UnitType.NPC, Uid).Data);
		DateTime timeStart = DateTime.Now;
		while (DateTime.Now.Subtract(timeStart).TotalMilliseconds < (double)timeOut)
		{
			Thread.Sleep(100);
			if (interacted)
			{
				break;
			}
		}
		Game.Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.NPCInfo, npcInfoDelg);
		if (interacted)
		{
			Game.Socket.Game.Send(new TownFolkInteract(UnitType.NPC, Uid).Data);
		}
		return interacted;
	}

	public bool OpenTrade(int timeOut)
	{
		if (!InteractWithTownFolk(timeOut))
		{
			return false;
		}
		bool receivedValidShopItem = false;
		PacketEventHandler worldItemActionDelg = delegate(D2Packet worlditemactionpacket)
		{
			WorldItemAction worldItemAction = new WorldItemAction(worlditemactionpacket.Data);
			if (worldItemAction.Action == ItemActionType.AddToShop)
			{
				receivedValidShopItem = true;
			}
		};
		Game.Socket.PacketHandler.AddAsyncListener(GameServerPacket.WorldItemAction, worldItemActionDelg);
		Console.WriteLine("Trying to open trade...");
		Game.Socket.Game.Send(new TownFolkMenuSelect(TownFolkMenuItem.Trade, Uid, 0u).Data);
		DateTime watchstart = DateTime.Now;
		while (DateTime.Now.Subtract(watchstart).TotalMilliseconds < (double)timeOut && !receivedValidShopItem)
		{
			Thread.Sleep(1);
		}
		Game.Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.WorldItemAction, worldItemActionDelg);
		return receivedValidShopItem;
	}

	public void CloseTrade()
	{
		Game.Items.ClearShop();
		Game.Socket.Game.Send(new TownFolkCancelInteraction(UnitType.NPC, Uid).Data);
	}

	public bool ReviveMerc(int timeOut)
	{
		if (!InteractWithTownFolk(timeOut))
		{
			return false;
		}
		Game.Socket.Game.Send(new ResurrectMerc(Uid).Data);
		Game.Socket.Game.Send(new TownFolkCancelInteraction(UnitType.NPC, Uid).Data);
		return true;
	}
}
