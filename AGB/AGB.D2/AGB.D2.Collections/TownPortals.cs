using System;
using System.Collections.Generic;
using System.Threading;
using AGB.Collections;
using D2Packets.GameServer;

namespace AGB.D2.Collections;

public class TownPortals : ThreadSafeList<TownPortal>
{
	private Game Game;

	public TownPortals(Game game)
	{
		Game = game;
	}

	public void Remove(uint portalLocalUid)
	{
		lock (PadLock)
		{
			TownPortal townPortal = ItemList.Find((TownPortal p) => p.PortalLocalUid == portalLocalUid);
			if (townPortal != null)
			{
				ItemList.Remove(townPortal);
			}
		}
	}

	public void Update(PortalOwnership portalOwnership)
	{
		lock (PadLock)
		{
			TownPortal townPortal = ItemList.Find((TownPortal p) => p.PortalLocalUid == portalOwnership.PortalLocalUID);
			if (townPortal == null)
			{
				townPortal = new TownPortal(Game);
				ItemList.Add(townPortal);
			}
			townPortal.PortalLocalUid = portalOwnership.PortalLocalUID;
			townPortal.OwnerName = portalOwnership.OwnerName;
			townPortal.OwnerUid = portalOwnership.OwnerUID;
			townPortal.PortalRemoteUid = portalOwnership.PortalRemoteUID;
		}
	}

	public TownPortal Find(uint portalLocalUid, int timeOut)
	{
		DateTime start = DateTime.Now;
		while (DateTime.Now.Subtract(start).TotalMilliseconds < (double)timeOut)
		{
			TownPortal townPortal = Find(portalLocalUid);
			if (townPortal != null)
			{
				return townPortal;
			}
			Thread.Sleep(100);
		}
		return null;
	}

	public TownPortal Find(uint portalLocalUid)
	{
		lock (PadLock)
		{
			return Find((TownPortal p) => p.PortalLocalUid == portalLocalUid);
		}
	}

	public TownPortal Find(string ownerName, int timeOut)
	{
		DateTime start = DateTime.Now;
		while (DateTime.Now.Subtract(start).TotalMilliseconds < (double)timeOut)
		{
			TownPortal townPortal = Find(ownerName);
			if (townPortal != null)
			{
				return townPortal;
			}
			Thread.Sleep(100);
		}
		return null;
	}

	public TownPortal Find(string ownerName)
	{
		lock (PadLock)
		{
			return Find((TownPortal p) => p.OwnerName == ownerName);
		}
	}

	public List<TownPortal> FindAll(uint portalLocalUid, int timeOut)
	{
		DateTime start = DateTime.Now;
		while (DateTime.Now.Subtract(start).TotalMilliseconds < (double)timeOut)
		{
			List<TownPortal> townPortals = FindAll(portalLocalUid);
			if (townPortals != null && townPortals.Count != 0)
			{
				return townPortals;
			}
			Thread.Sleep(100);
		}
		return null;
	}

	public List<TownPortal> FindAll(uint portalLocalUid)
	{
		lock (PadLock)
		{
			return FindAll((TownPortal p) => p.PortalLocalUid == portalLocalUid);
		}
	}
}
