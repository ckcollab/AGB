using System;
using System.Collections.Generic;
using AGB.Collections;
using AGB.Net;

namespace AGB.D2;

public class BotManager
{
	private object PadLock = new object();

	public static readonly BotManager Instance = new BotManager();

	private ThreadSafeList<Bot> Bots = new ThreadSafeList<Bot>();

	private ThreadSafeList<CdKeySetInstance>[] CdKeys = new ThreadSafeList<CdKeySetInstance>[3];

	private ThreadSafeList<ProxyInstance>[] Proxies = new ThreadSafeList<ProxyInstance>[3];

	private ThreadSafeList<CdKeySetProxyComboInstance>[] Combos = new ThreadSafeList<CdKeySetProxyComboInstance>[3];

	private GameLog GameLog;

	private BotManager()
	{
		GameLog = new GameLog("gamelog.xml");
		for (int i = 0; i < 3; i++)
		{
			CdKeys[i] = new ThreadSafeList<CdKeySetInstance>();
			Proxies[i] = new ThreadSafeList<ProxyInstance>();
			Combos[i] = new ThreadSafeList<CdKeySetProxyComboInstance>();
			Proxies[i].Add(new ProxyInstance("", 0));
		}
	}

	public void AddBot(Bot bot)
	{
		Bots.Add(bot);
	}

	public List<Bot> GetBots()
	{
		return Bots.FindAll((Bot b) => b != null);
	}

	public void AddCdKeySet(string classic, string expansion)
	{
		AddCdKeySet(new CdKeySet(classic, expansion));
	}

	public void AddCdKeySet(CdKeySet cdKeySet)
	{
		for (int i = 0; i < 3; i++)
		{
			CdKeySetInstance cdKeySetInstance = new CdKeySetInstance(cdKeySet);
			CdKeys[i].Add(cdKeySetInstance);
			for (int j = 0; j < Proxies[i].Count; j++)
			{
				Combos[i].Add(new CdKeySetProxyComboInstance(cdKeySetInstance, Proxies[i].GetAt(j)));
			}
		}
	}

	public void AddProxy(Proxy proxy)
	{
		AddProxy(proxy.IP, proxy.Port, proxy.Username, proxy.Password);
	}

	public void AddProxy(string proxy, int port)
	{
		AddProxy(proxy, port, "", "");
	}

	public void AddProxy(string proxy, int port, string username, string password)
	{
		for (int i = 0; i < 3; i++)
		{
			ProxyInstance proxyInstance = new ProxyInstance(proxy, port);
			Proxies[i].Add(proxyInstance);
			for (int j = 0; j < CdKeys[i].Count; j++)
			{
				Combos[i].Add(new CdKeySetProxyComboInstance(CdKeys[i].GetAt(j), proxyInstance));
			}
		}
	}

	public void LoadGameLog()
	{
		lock (PadLock)
		{
			LoggedGame game;
			foreach (LoggedGame loggedGame2 in GameLog.LoggedGames)
			{
				game = loggedGame2;
				for (int realm = 0; realm < 3; realm++)
				{
					CdKeySetProxyComboInstance comboInstance = Combos[realm].Find((CdKeySetProxyComboInstance instance) => instance.CdKeySet.Classic == game.Combo.CdKeySet.Classic && instance.CdKeySet.Expansion == game.Combo.CdKeySet.Expansion && instance.Proxy.IP == game.Combo.Proxy.IP && instance.Proxy.Port == game.Combo.Proxy.Port);
					if (comboInstance != null)
					{
						LoggedGame loggedGame = new LoggedGame();
						loggedGame.Combo = game.Combo;
						loggedGame.JoinedAt = game.JoinedAt;
						LoggedGame oldGame = loggedGame;
						comboInstance.Games.Add(oldGame);
					}
				}
			}
		}
	}

	public CdKeySetProxyCombo GetCombo(Realm realm)
	{
		lock (PadLock)
		{
			for (int i = 0; i < Combos[(int)realm].Count; i++)
			{
				if (Combos[(int)realm].GetAt(i).CdKeySetInstance.Instances >= Combos[(int)realm].GetAt(i).CdKeySetInstance.AllowedInstances)
				{
					int alertme = 1;
					alertme++;
				}
				if (Combos[(int)realm].GetAt(i).ProxyInstance.Instances >= Combos[(int)realm].GetAt(i).ProxyInstance.AllowedInstances)
				{
					int alertme = 1;
					alertme++;
				}
				if (!(DateTime.Now.Subtract(Combos[(int)realm].GetAt(i).Released) >= Combos[(int)realm].GetAt(i).IgnoreLength))
				{
					int alertme = 1;
					alertme++;
				}
				if (GetGameCountInLastHour(realm, Combos[(int)realm].GetAt(i)) >= 19)
				{
					int alertme = 1;
					alertme++;
				}
				if (Combos[(int)realm].GetAt(i).CdKeySetInstance.Instances < Combos[(int)realm].GetAt(i).CdKeySetInstance.AllowedInstances && Combos[(int)realm].GetAt(i).ProxyInstance.Instances < Combos[(int)realm].GetAt(i).ProxyInstance.AllowedInstances && DateTime.Now.Subtract(Combos[(int)realm].GetAt(i).Released) >= Combos[(int)realm].GetAt(i).IgnoreLength && GetGameCountInLastHour(realm, Combos[(int)realm].GetAt(i)) < 19)
				{
					Combos[(int)realm].GetAt(i).CdKeySetInstance.Instances++;
					Combos[(int)realm].GetAt(i).ProxyInstance.Instances++;
					return new CdKeySetProxyCombo(new CdKeySet(Combos[(int)realm].GetAt(i).CdKeySet.Classic, Combos[(int)realm].GetAt(i).CdKeySet.Expansion), new Proxy
					{
						Username = Combos[(int)realm].GetAt(i).Proxy.Username,
						Password = Combos[(int)realm].GetAt(i).Proxy.Password,
						IP = Combos[(int)realm].GetAt(i).Proxy.IP,
						Port = Combos[(int)realm].GetAt(i).Proxy.Port
					});
				}
			}
			return null;
		}
	}

	public void ReleaseCombo(Realm realm, CdKeySetProxyCombo combo)
	{
		ReleaseCombo(realm, combo, TimeSpan.FromMilliseconds(0.0));
	}

	public void ReleaseCombo(Realm realm, CdKeySetProxyCombo combo, TimeSpan ignore)
	{
		CdKeySetProxyComboInstance comboInstance = Combos[(int)realm].Find((CdKeySetProxyComboInstance instance) => instance.CdKeySet.Classic == combo.CdKeySet.Classic && instance.CdKeySet.Expansion == combo.CdKeySet.Expansion && instance.Proxy.IP == combo.Proxy.IP && instance.Proxy.Port == combo.Proxy.Port);
		if (comboInstance != null)
		{
			comboInstance.CdKeySetInstance.Instances--;
			comboInstance.ProxyInstance.Instances--;
			comboInstance.Released = DateTime.Now;
			comboInstance.IgnoreLength = ignore;
		}
	}

	public void EnterGame(Realm realm, CdKeySetProxyCombo combo)
	{
		lock (PadLock)
		{
			CdKeySetProxyComboInstance comboInstance = Combos[(int)realm].Find((CdKeySetProxyComboInstance instance) => instance.CdKeySet.Classic == combo.CdKeySet.Classic && instance.CdKeySet.Expansion == combo.CdKeySet.Expansion && instance.Proxy.IP == combo.Proxy.IP && instance.Proxy.Port == combo.Proxy.Port);
			if (comboInstance != null)
			{
				LoggedGame loggedGame = new LoggedGame();
				loggedGame.Combo = combo;
				loggedGame.JoinedAt = DateTime.Now;
				LoggedGame game = loggedGame;
				GameLog.LoggedGames.Add(game);
				GameLog.Save("gamelog.xml");
				comboInstance.Games.Add(game);
			}
		}
	}

	public int GetGameCountInLastHour(Realm realm, CdKeySetProxyCombo combo)
	{
		CdKeySetProxyComboInstance comboInstance = Combos[(int)realm].Find((CdKeySetProxyComboInstance instance) => instance.CdKeySet.Classic == combo.CdKeySet.Classic && instance.CdKeySet.Expansion == combo.CdKeySet.Expansion && instance.Proxy.IP == combo.Proxy.IP && instance.Proxy.Port == combo.Proxy.Port);
		if (comboInstance == null)
		{
			return 0;
		}
		List<LoggedGame> gamesInLastHour = comboInstance.Games.FindAll((LoggedGame game) => DateTime.Now.Subtract(game.JoinedAt).TotalHours < 1.0);
		return gamesInLastHour.Count;
	}
}
