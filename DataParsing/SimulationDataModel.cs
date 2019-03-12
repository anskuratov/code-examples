using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Tzup
{
	public class SimulationDataModel
	{
		private static SimulationDataModel _instance;

		private const string scenariosPath = @"Assets/Sources/Simulation/Scenarios/";

		public static readonly string[] scenariosPaths =
		{
			@"scenario1/", @"scenario2/", @"scenario3/", @"scenario4/", @"scenario5/"
        };

		public static readonly string[] scenarioFilesWithManeuvers =
		{
			@"default.json", @"in-track.json", @"in-plane.json", @"out-plane.json"
		};

		public Dictionary<int, SimulationStampModel>[] SimulationStampsData { get; private set; }

		private Dictionary<int, SimulationStampModel>[][] _allSimulationStampsData;

		private SimulationDataModel()
		{
			InitializeAllSimulations();
		}

		public static SimulationDataModel Instance()
		{
			if (_instance == null)
				_instance = new SimulationDataModel();

			return _instance;
		}

		public void SetSimulationStampsData(int index)
		{
			SimulationStampsData = _allSimulationStampsData[index];
		}

		private void InitializeAllSimulations()
		{
			_allSimulationStampsData = new Dictionary<int, SimulationStampModel>[scenariosPaths.Length][];

			for (int i = 0; i < _allSimulationStampsData.Length; ++i)
			{
				_allSimulationStampsData[i] = new Dictionary<int, SimulationStampModel>[scenarioFilesWithManeuvers.Length];
			}

			for (int i = 0; i < scenariosPaths.Length; ++i)
			{
				string[] scenarioData = GetJsonStringFromScenarioFile(i);

				for (int j = 0; j < scenarioFilesWithManeuvers.Length; ++j)
					_allSimulationStampsData[i][j] = GetSimulationObjectFromJson(scenarioData[j]);
			}
		}

		private string[] GetJsonStringFromScenarioFile(int scenarioIndex)
		{
			var maneuversData = new string[scenarioFilesWithManeuvers.Length];
			var scenarioFullPath = scenariosPath + scenariosPaths[scenarioIndex];

			for (int i = 0; i < maneuversData.Length; ++i)
			{
				using (var reader = new StreamReader(scenarioFullPath + scenarioFilesWithManeuvers[i]))
				{
					maneuversData[i] = reader.ReadToEnd();
				}
			}

			return maneuversData;
		}

		private Dictionary<int, SimulationStampModel> GetSimulationObjectFromJson(string scenarioData)
		=> JsonConvert.DeserializeObject<Dictionary<int, SimulationStampModel>>(scenarioData);
	}
}