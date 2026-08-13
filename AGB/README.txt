Decompiled C# recovered from the 2008 AGB.MapHack release binaries (ILSpy + original PDBs).

  AGB/            core library (tasks, net, collections)
  AGB.D2/         Diablo 2 game/bot layer (referenced by Maphack and AGBotting)
  AGB.Mapping/    map generation wrapper around native map_eng.dll
  References/     third-party DLLs the old .csproj files expected

These are not the original source files. Control flow and names are recovered from PDBs, but comments, some locals, and original formatting are gone.

D2Data and D2Packets (E.T., 2007) are under ../decompiled/.
map_eng.dll is native Delphi, not C#, so it was not decompiled here.
