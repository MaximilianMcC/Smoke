# MarlEngine
A simple game engine to create 3D games using C# with the power of raylib.

## How everything works
There is a pre built `game.exe`. This is set up to dynamically load C# scripts and use them. Scripts can be added to different game objects via the editor. This will also provide a visual way to create levels and whatnot. These scripts interface with the game executable via the library.

All together there are four total C# projects:
| Project | What it does | Link |
|---------|--------------|------|
| Engine  | Lets you add scripts to things visually, and create maps | [Engine](./Engine/) |
| Game    | Runs scripts from the developed game, and uses maps from the engine | [Game](./Game/) |
| Library | Lets you write code to interface with the Game project | [Library](./Library/) |
| Developed Game | Where you write code with the library in conjunction with the engine to be used by game | N/A |

## Compiling the developed game
To allow individual hot-reloading, the game is compiled into separate scripts. This uses `csc`. If you don't have it you can install it with `dotnet tool install -g csc`

## Compiling the game
Still not sure what the full thing should be so idk. Command is here though
```sh
dotnet publish -r win-x64 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true --self-contained /p:DebugType=None
```
Maybe just point to the exe that `dotnet run` makes ngl (quicker (not in the mood to wait for it to publish))