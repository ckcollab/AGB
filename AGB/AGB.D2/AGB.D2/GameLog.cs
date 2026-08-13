using System.Collections.Generic;
using System.IO;

namespace AGB.D2;

public class GameLog
{
	public List<LoggedGame> LoggedGames = new List<LoggedGame>();

	public GameLog()
	{
	}

	public GameLog(string fileName)
	{
		if (File.Exists(fileName))
		{
			GameLog config = Util.XmlDeserialize<GameLog>(fileName);
			if (config != null)
			{
				LoggedGames = config.LoggedGames;
			}
		}
	}

	public void Save(string fileName)
	{
		Util.XmlSerialize<GameLog>(this, fileName);
	}
}
