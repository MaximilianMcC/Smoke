using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Smoke;
public class SceneManager
{
	public static Scene CurrentScene = null;
	public static List<Scene> Scenes = null;

	//? This only needs to happen once at the start of the program
	public static void DeserializeAllScenes()
	{
		// Get the project config json, and parse it
		// so that we can manually extract the scenes
		JObject json = JObject.Parse(SmokeProject.ConfigJson);
		JArray rawScenes = (JArray)json["Scenes"];

		// Use the custom deserializer to parse the scenes
		Scenes = rawScenes.ToObject<List<Scene>>(SmokeProject.JsonDeserializerSettings);
	}

	// TODO: don't use strings like this
	public static void Load(string name)
	{
		// Find the scene we want
		Scene newScene = Scenes.Where(scene => scene.Name == name).FirstOrDefault();
		if (newScene == default)
		{
			Console.Error.WriteLine($"Cannot find a scene by the name of \"{name}\" (check spelling)");
			return;
		}

		// Unload the current scene and
		// set this scene to be the new one
		UnloadCurrentScene();
		CurrentScene = newScene;

		// Call all start methods on all components
		CurrentScene.Start();
	}

	internal static void UnloadCurrentScene()
	{
		// If we had a previous scene, then unload it
		if (CurrentScene != null)
		{
			CurrentScene.Unload();
			CurrentScene = null;
		}
	}
}