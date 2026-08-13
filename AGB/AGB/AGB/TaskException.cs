using System;

namespace AGB;

public class TaskException : Exception
{
	public TaskException(string message)
		: base(message)
	{
	}
}
