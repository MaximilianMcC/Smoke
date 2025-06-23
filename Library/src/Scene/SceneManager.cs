using Force.DeepCloner;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Smoke;

public static class SceneManager
{
	public static List<Scene> Scenes = [];
	public static Scene CurrentScene { get; private set; }

	public static void DeserializeScenes(JArray rawScenes)
	{
		// Loop over every scene
		foreach (JObject rawScene in rawScenes)
		{
			// Make the new scene and populate from the JSON
			// Unique given is given per 'instance' of a game
			// TODO: Don't do this manually
			Scene scene = new Scene();
			scene.DisplayName = (string)rawScene["DisplayName"];
			scene.Guid = Guid.NewGuid();

			// Populate the scene with its game objects
			JArray rawGameObjects = (JArray)rawScene["Things"];
			scene.Things = ObjectManager.DeserializeGameObjects(rawGameObjects);

			// Add the scene to the scenes list
			Scenes.Add(scene);
		}
	}

	public static void LoadScene(Guid sceneGuid) => LoadScene(Scenes.Find(scene => scene.Guid == sceneGuid));
	public static void LoadScene(string sceneDisplayName) => LoadScene(Scenes.Find(scene => scene.DisplayName == sceneDisplayName));
	public static void LoadScene(Scene newScene)
	{
		Console.WriteLine("Loading scene " + newScene.DisplayName);

		// Create a copy of the 'clean' scene
		Scene sceneCopy = newScene.DeepClone();

		// Unload the previous scene then load the new scene
		UnloadScene(CurrentScene);
		CurrentScene = sceneCopy;

		// Load everything in that scene
		foreach (GameObject gameObject in CurrentScene.Things)
		{
			gameObject.Start();
		}
	}

	private static void UnloadScene(Scene scene)
	{
		if (scene == null) return;

		// Unload everything in the scene
		// TODO: Make a new method that doesn't unload the previous scene? could be helpful fr
		foreach (GameObject gameObject in CurrentScene.Things)
		{
			gameObject.TidyUp();
		}
	}

	public static void RestartCurrentScene()
	{
		if (CurrentScene == null)
		{
			// TODO: Proper onscreen (and in terminal) logging system
			Console.WriteLine("Can't restart the current scene if there is no scene");
			return;
		}

		// TODO: Figure out what scene the current scene inherits off

	}
}