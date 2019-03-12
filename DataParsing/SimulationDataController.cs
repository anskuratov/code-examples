using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tzup
{
	public class SimulationDataController
	{
		private int _scenariosNumber;
		private Dictionary<int, List<Vector3>> _debrisWayPoints;
		private Dictionary<int, List<Vector3>>.Enumerator _debrisWayPointsEnumerator;
		private int _maneuverIndex;

		public int ScenarioIndex { get; private set; }
		public SimulationDataModel SimulationData { get; private set; }

		public List<Vector3> ProtectedWayPoints;
		public int NumberOfDebrisObjects { get; private set; }

		public SimulationDataController()
		{
			Initialize();

			_maneuverIndex = 0;

			var rnd = new System.Random();
			ScenarioIndex = rnd.Next(0, _scenariosNumber);

			SimulationData = SimulationDataModel.Instance();
			SimulationData.SetSimulationStampsData(ScenarioIndex);

			InitializeObjectsWayPoints();
		}

		public SimulationDataController(int scenarioIndex, int maneuverIndex)
		{
			Initialize();

			if (scenarioIndex >= _scenariosNumber || scenarioIndex < 0)
			{
				throw new ArgumentException("Incorrect constructor argument!");
			}

			ScenarioIndex = scenarioIndex;
			_maneuverIndex = maneuverIndex;

			SimulationData = SimulationDataModel.Instance();
			SimulationData.SetSimulationStampsData(ScenarioIndex);

			InitializeObjectsWayPoints();
		}

		public List<Vector3> GetNextDebrisWayPoints()
		{
			if (_debrisWayPointsEnumerator.MoveNext())
				return _debrisWayPointsEnumerator.Current.Value;
			else
				throw new IndexOutOfRangeException("Can't return next DEBRIS object points!");
		}

		private void Initialize() => _scenariosNumber = SimulationDataModel.scenariosPaths.Length;

		private void InitializeObjectsWayPoints()
		{
			ProtectedWayPoints = new List<Vector3>();
			_debrisWayPoints = new Dictionary<int, List<Vector3>>();
			for (int i = 0; i < SimulationData.SimulationStampsData[_maneuverIndex][0].Debris_pos.Count; ++i)
				_debrisWayPoints.Add(i, new List<Vector3>());

			foreach (var stampData in SimulationData.SimulationStampsData[_maneuverIndex].Values)
			{
				var protectedPosition = stampData.Protected_pos;
				ProtectedWayPoints.Add(new Vector3(protectedPosition[0], protectedPosition[1], protectedPosition[2]));

				var debrisPosition = stampData.Debris_pos;
				for (int i = 0; i < debrisPosition.Count; ++i)
					_debrisWayPoints[i].Add(new Vector3(debrisPosition[i][0], debrisPosition[i][1], debrisPosition[i][2]));
			}

			_debrisWayPointsEnumerator = _debrisWayPoints.GetEnumerator();

			NumberOfDebrisObjects = _debrisWayPoints.Keys.Count;
		}
	}
}
