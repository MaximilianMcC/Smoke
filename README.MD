# MarlEngine
A simple game engine to create 3D games using C# with the power of raylib.

## How everything works
There is a pre built `game.exe`. This is set up to dynamically load C# scripts and use them. Scripts can be added to different game objects via the editor. This will also provide a visual way to create levels and whatnot. These scripts interface with the game executable via the library.

## Compiling the game 'runner'
Still not sure what the full thing should be so idk. Command is here though
```sh
dotnet publish -r win-x64 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true --self-contained /p:DebugType=None
```
Maybe just point to the exe that `dotnet run` makes ngl (quicker (not in the mood to wait for it to publish))