# Architecture

## History
This is the third version. Version one used an entity-component system, version two used a weird hybrid mix of an ecs and normal oop stuff, and this version uses the same weird mix. I have chosen to do this third version since the second version was so messy. The biggest issue I had was not sticking to a specific style of writing things.

## Plan

## Project Structure
Smoke is made from three 'sub projects' All of these work together. [Smoke](../Smoke/) is the api/library used by you to actually write the game and interface with the engine. [Engine](../Engine/) is the program that you use to put assign components to gameobjects and whatnot. It also contains the CLI stuff for compiling and building. [Runner](../Runner/) Takes the code you've written and makes it into an executable.

The most important bit of a smoke project is the json file. This contains all of the games information (maps, components, settings, etc)