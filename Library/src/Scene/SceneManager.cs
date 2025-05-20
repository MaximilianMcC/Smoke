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

	public static void SetScene(Guid sceneGuid) => SetScene(Scenes.Find(scene => scene.Guid == sceneGuid));
	public static void SetScene(string sceneDisplayName) => SetScene(Scenes.Find(scene => scene.DisplayName == sceneDisplayName));
	public static void SetScene(Scene newScene)
	{
		Console.WriteLine("Loading scene " +  newScene.DisplayName);

		// Check for if we had a previous scene
		if (CurrentScene != null)
		{
			// Unload everything in the scene
			// TODO: Make a new method that doesn't unload the previous scene? could be helpful fr
			foreach (GameObject gameObject in CurrentScene.Things)
			{
				gameObject.TidyUp();
			}
		}

		// Set our seen to be the new scene
		CurrentScene = newScene;

		// Load everything in that scene
		foreach (GameObject gameObject in CurrentScene.Things)
		{
			gameObject.Start();
		}
	}
}