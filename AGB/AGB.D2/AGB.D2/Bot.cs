using System;
using System.Collections.Generic;
using System.IO;

namespace AGB.D2;

public class Bot
{
	private readonly Profile Profile;

	private PluginManager<Module> PluginManager;

	public Game Game;

	public List<Module> Modules;

	public Bot(Profile profile)
	{
		Profile = profile;
		PluginManager = new PluginManager<Module>("AGB for " + profile.Character.Name + "@" + profile.Character.Realm, Environment.CurrentDirectory + Path.DirectorySeparatorChar + "characters" + Path.DirectorySeparatorChar + profile.Character.Name + "@" + profile.Character.Realm);
		Modules = new List<Module>();
		Game = new Game(Profile);
	}

	public void AddModules(string directory)
	{
		List<Module> modules = PluginManager.GetPlugins(directory);
		if (modules == null)
		{
			return;
		}
		foreach (Module module in modules)
		{
			AddModule(module);
		}
	}

	public void AddModule(Module module)
	{
		module.Bot = this;
		module.Game = Game;
		module.BaseDirectory = string.Concat(Environment.CurrentDirectory, Path.DirectorySeparatorChar, "characters", Path.DirectorySeparatorChar, Profile.Character.Name, "@", Profile.Character.Realm, Path.DirectorySeparatorChar, module.Name, Path.DirectorySeparatorChar);
		Game.VersionChecked += module.VersionChecked;
		Game.LobbyEntered += module.LobbyEntered;
		Game.GameEntered += module.GameEntered;
		Game.GameExited += module.GameExited;
		Modules.Add(module);
	}

	public Module GetModule(string name)
	{
		return Modules.Find((Module module) => module.Name == name);
	}

	public bool HasModule(string name)
	{
		return Modules.Find((Module module) => module.Name == name) != null;
	}

	public bool LoadModules()
	{
		try
		{
			foreach (Module module in Modules)
			{
				module.Load();
			}
		}
		catch (ModuleException)
		{
			return false;
		}
		return true;
	}

	public void Start()
	{
		foreach (Module module in Modules)
		{
			module.Start(Game);
		}
	}

	public void UnloadModules()
	{
		foreach (Module module in Modules)
		{
			module.Unload();
		}
		Modules.Clear();
	}

	public void RemoveModule(Module module)
	{
		Modules.Remove(module);
		module.Unload();
	}
}
