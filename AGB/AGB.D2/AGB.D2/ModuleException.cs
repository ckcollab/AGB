using System;

namespace AGB.D2;

public class ModuleException : Exception
{
	public Module Module;

	public ModuleException(Module module, string message)
		: base(message)
	{
		Module = module;
	}
}
