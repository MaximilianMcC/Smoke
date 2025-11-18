using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Smoke;
public class GameObjectHandler
{
	public string Deserialize(GameObject gameObject)
	{
		return JsonConvert.SerializeObject(gameObject);
	}
}