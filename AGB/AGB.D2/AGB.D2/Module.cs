using System.Threading;

namespace AGB.D2;

public abstract class Module
{
	public Game Game;

	public Bot Bot;

	public string BaseDirectory;

	public string Name;

	public string Author;

	public string Version;

	public event ModuleEventArgs StatusUpdated;

	public event ModuleEventArgs Warning;

	public event ModuleExceptionEventArgs ExceptionThrown;

	public virtual void Load()
	{
	}

	public virtual void Unload()
	{
	}

	public virtual void Start(Game game)
	{
	}

	public virtual void VersionChecked(Game game)
	{
	}

	public virtual void LobbyEntered(Game game)
	{
	}

	public virtual void GameEntered(Game game)
	{
	}

	public virtual void GameExited(Game game)
	{
	}

	protected void RaiseUpdate(string message)
	{
		if (this.StatusUpdated != null)
		{
			this.StatusUpdated(this, message);
		}
	}

	protected void RaiseWarning(string message)
	{
		if (this.Warning != null)
		{
			this.Warning(this, message);
		}
	}

	protected void ThrowModuleException(ModuleException e)
	{
		Task resetTask = new Task(int.MaxValue, "Throwing Module Exception", delegate
		{
			if (this.ExceptionThrown == null)
			{
				throw e;
			}
			this.ExceptionThrown(e);
			if (Game.Seed != 0)
			{
				Game.LeaveGame();
			}
		});
		Game.TaskManager.Reset(resetTask);
		Thread.Sleep(5000);
	}
}
