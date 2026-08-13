using System;
using System.Diagnostics;
using System.IO;

namespace AGB;

public sealed class TraceLog
{
	private string file = "";

	private DebugLevel level;

	private readonly object SyncObj = new object();

	public string FileName
	{
		get
		{
			return file;
		}
		set
		{
			file = value;
		}
	}

	public DebugLevel TraceLevel
	{
		get
		{
			return level;
		}
		set
		{
			level = value;
		}
	}

	public event LogEvent Written;

	public TraceLog(string file, DebugLevel level)
	{
		this.file = file;
		this.level = level;
	}

	public void AddMessage(string message)
	{
		AddMessage(message, DebugLevel.Info);
	}

	public void AddMessage(string message, DebugLevel lvl)
	{
		if (lvl >= level)
		{
			return;
		}
		lock (SyncObj)
		{
			StackFrame frame = new StackTrace(fNeedFileInfo: false).GetFrame(1);
			string formattedMessage = string.Format("[{0}] {1}: {2}", DateTime.Now.ToUniversalTime(), Enum.Format(typeof(DebugLevel), lvl, "G"), message);
			if (this.Written != null)
			{
				this.Written(formattedMessage, lvl);
			}
			string rootDir = Path.GetDirectoryName(file) + "\\";
			if (!Directory.Exists(rootDir))
			{
				Directory.CreateDirectory(rootDir);
			}
			using StreamWriter log = new StreamWriter(File.Open(file, FileMode.Append));
			log.WriteLine(formattedMessage);
		}
	}
}
