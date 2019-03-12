using Newtonsoft.Json;

namespace Tzup
{
	public class CollisionAlertModel
	{
		[JsonProperty("is_alert")]
		public bool IsAlert { get; set; }

		[JsonProperty("info")]
		public CollisionInfoModel CollisionInfo { get; set; }

		public CollisionAlertModel()
		{
			IsAlert = false;
			CollisionInfo = new CollisionInfoModel();
		}

		public CollisionAlertModel(bool isAlert, CollisionInfoModel collisionInfo)
		{
			IsAlert = isAlert;
			CollisionInfo = collisionInfo;
		}
	}
}
