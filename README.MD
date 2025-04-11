# MarlEngine
A simple game engine to create 3D games with C# and the power of raylib.

# How everything works
basic overview thing idk. Might make a youtube video or something explaining when its all done.

## Actual Engine/running code
The whole game is represented in a single json file. This file has project settings, and all game objects. This file can be manually written to if wanted, but it's recommended to use the actual engine (will do it all for you) All game objects have components, with the most important one being the script component.

The game technically isn't actually a program, it's a library. All of the games code is compiled into an assembly. This dll is then injected into the game *runner*, which is the actual program (window and whatnot) A script component contains the url to a type/class inside the assembly. The runner parses it from the game json file, then loads the type and runs the code.

## ECS (Entity Component System)
Instead of using an object orientated way of representing things, an entity component system is used (btw this doesn't mean I've not written it using oop techniques) Instead of each object having properties and stuff, each entity has components. These components aren't owned by the entity, but used by it. This is mainly for performance reasons (I think) but also to make everything cleaner and more modular.

# Lore
Originally was named MarlEngine but got renamed to Smoke because MarlEngine is a stink name