# Drawing text

Drawing text is just rendering a string (hope you knew that ngl) You can do this with the `DrawText` method. There are heaps of overloads for this btw. Heres one example:
```cs
using static Smoke.Graphics;

void Render2D()
{
	float fontSize = 30;
	string text = "Kia Ora, te ao";

	DrawText(text, 10, 10, fontSize, Color.White);
}
```
By default this is gonna use the built in rayLib font since this uses raylib for rendering. If you wanna use a custom font, then load it via `LoadFont()`, then set it as the used font:
```cs
using static Smoke.Graphics;
using static Smoke.AssetManager;

void LoadType()
{
	Fonts["consolas"] = LoadFont("./assets/consolas.ttf");
	FontKey = "consolas";
}

void Render2D()
{
	DrawText($"This text is in {FontKey}.", 10, 10, 25, Color.White);
}
```
Every time you wanna switch fonts then just reassign the `FontKey` variable.

---
[Back to home](../Docs.md)