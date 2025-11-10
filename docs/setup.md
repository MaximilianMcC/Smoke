# Smoke Setup

## Downloading

## Making A New Project
Open the terminal, and use the following command:
```sh
smoke new "ProjectName"
```
This will create all nessearly folders/files in a new directory. To open the game in the editor, run this:
```sh
smoke ProjectName
```

If you wanna add vscode support then add this
```json
// Launch.json
{
	"version": "0.2.0",
	"configurations": [
		{
			"name": "Debug Smoke Project",
			"type": "coreclr",
			"request": "launch",
			"preLaunchTask": "Build Smoke Project",
			"program": "D:/code/c#/raylib/Smoke/Engine/bin/Debug/net8.0/Engine.exe",
			"cwd": "${workspaceFolder}",
			"console": "internalConsole",

			"args": ["run", "./"]
		}
	]
}
```
```json
// Tasks.json
{
	"version": "2.0.0",
	"tasks": [
		{
			"label": "Build Smoke Project",
			"command": "D:/code/c#/raylib/Smoke/Engine/bin/Debug/net8.0/Engine.exe",
			"type": "process",
			"args": ["build", "./", "debug"],
			"problemMatcher": "$msCompile"
		}
	]
}
```