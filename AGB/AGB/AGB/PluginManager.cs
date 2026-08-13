using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace AGB;

public class PluginManager<T>
{
	private AppDomain PluginDomain;

	public PluginManager(string domainName, string path)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		PermissionSet permSet = new PermissionSet(PermissionState.None);
		permSet.AddPermission((IPermission?)new SecurityPermission(SecurityPermissionFlag.Execution));
		permSet.AddPermission((IPermission?)new SecurityPermission(SecurityPermissionFlag.ControlAppDomain));
		permSet.AddPermission((IPermission?)new SecurityPermission(SecurityPermissionFlag.SerializationFormatter));
		permSet.AddPermission((IPermission?)new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
		permSet.AddPermission((IPermission?)new ReflectionPermission((ReflectionPermissionFlag)4));
		permSet.AddPermission((IPermission?)new EnvironmentPermission(PermissionState.None));
		permSet.AddPermission((IPermission?)new FileDialogPermission((FileDialogPermissionAccess)3));
		permSet.AddPermission((IPermission?)new RegistryPermission(PermissionState.None));
		permSet.AddPermission((IPermission?)new UIPermission(PermissionState.Unrestricted));
		permSet.AddPermission((IPermission?)new FileIOPermission((FileIOPermissionAccess)15, path));
		AppDomainSetup setup = new AppDomainSetup();
		setup.set_ApplicationBase(path);
		setup.set_DisallowApplicationBaseProbing(false);
		PluginDomain = AppDomain.CreateDomain(domainName, (Evidence)null, setup, permSet, (StrongName[])null);
	}

	public List<T> GetPlugins(string path)
	{
		List<T> plugins = new List<T>();
		if (!Directory.Exists(path))
		{
			return null;
		}
		DirectoryInfo dir = new DirectoryInfo(path);
		List<FileInfo> possiblePlugins = new List<FileInfo>(dir.GetFiles("*.dll"));
		foreach (FileInfo plugin in possiblePlugins)
		{
			Type ObjType = null;
			try
			{
				Assembly assembly = Assembly.Load(Path.GetFileNameWithoutExtension(plugin.FullName));
				if ((object)assembly != null)
				{
					Type[] types = assembly.GetTypes();
					Type[] array = types;
					foreach (Type type in array)
					{
						if ((object)type.BaseType == typeof(T))
						{
							ObjType = type;
							break;
						}
					}
				}
			}
			catch (FileLoadException)
			{
			}
			catch (BadImageFormatException)
			{
			}
			if ((object)ObjType != null)
			{
				plugins.Add(Util.LoadAssembly<T>(Path.GetFileNameWithoutExtension(plugin.FullName)));
			}
		}
		return plugins;
	}
}
