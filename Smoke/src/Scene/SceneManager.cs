using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Smoke;
public class SceneManager
{
	public static Scene CurrentScene = null;

	// TODO: don't use strings like this
	public static void Load(string name)
	{
		// Get the project config json, and parse it
		// so that we can manually extract the scenes
		JObject json = JObject.Parse(SmokeProject.ConfigJson);
		JArray rawScenes = (JArray)json["Scenes"];

		// Find the scene we want
		JObject rawScene = (JObject)rawScenes.Where(scene => (string)scene["Name"] == name).FirstOrDefault();
		if (rawScene == default)
		{
			Console.Error.WriteLine($"Cannot find a scene with the name \"{name}\"");
			return;
		}

		// Make the actual scene
		Scene newScene = new Scene()
		{
			Name = (string)rawScene["Name"],
			GameObjects = new List<GameObject>()
		};

		// Make a custom deserializer that lets us handle custom types in JSON
		JsonSerializer deserializer = JsonSerializer.Create(new JsonSerializerSettings()
		{
			// Stuff for assemblies
			TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
			SerializationBinder = new DefaultSerializationBinder(),

			// Defines the datatype in a "$type" property
			TypeNameHandling = TypeNameHandling.Auto,
		});

		// Loop over each game object in the scene and deserialize it
		// TODO: Get newtonsoft to do this all for us
		foreach (JObject rawGameObject in rawScene["GameObjects"])
		{
			// Deserialize the game object then add it to the scene
			GameObject currentGameObject = rawGameObject.ToObject<GameObject>(deserializer);
			newScene.GameObjects.Add(currentGameObject);
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