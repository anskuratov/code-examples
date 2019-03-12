using Newtonsoft.Json;

namespace Tzup
{
	public class CollisionInfoModel
	{
		[JsonProperty("probability")]
		public float Probability { get; set; }

		[JsonProperty("distance")]
		public float DistanceTo { get; set; }

		[JsonProperty("epoch")]
		public float Epoch { get; set; }

		[JsonProperty("debris_name")]
		public string DebrisName { get; set; }

		[JsonProperty("debris_id")]
		public string DebrisId { get; set; }

		[JsonProperty("sec_before_collision")]
		public float SecondsTo { get; set; }

		public CollisionInfoModel()
		{
			Probability = 0;
			DistanceTo = 0;
			Epoch = 0;
			DebrisName = string.Empty;
			DebrisId = string.Empty;
			SecondsTo = 0;
		}

		public CollisionInfoModel(float probability,
								  float distanceTo,
								  float epoch,
								  string debrisName,
								  string debrisId,
								  float secondsTo)
		{
			Probability = probability;
			DistanceTo = distanceTo;
			Epoch = epoch;
			DebrisName = debrisName;
			DebrisId = debrisId;
			SecondsTo = secondsTo;
		}
	}
}
