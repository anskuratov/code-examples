using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Sources.Store
{
    [RequireComponent(typeof(GridLayoutGroup))]
    [RequireComponent(typeof(ToggleGroup))]
    public sealed class StoreTableController : TableController
    {
        private StoreHelper _storeHelper;

        [Inject]
        private void ZenjectInit(StoreHelper storeHelper)
        {
            _storeHelper = storeHelper;
        }

        private void OnEnable()
        {
            ClearTableContent();
            FillTheTable();
        }

        private void FillTheTable()
        {
            switch (_tableType)
            {
                case TableType.Buildings:
                    var data = _gameDataPool.StartData.SBuilding;
                    foreach (var buildingData in data)
                    {
                        var currentCell = Instantiate(_tableCellPrefab, _tableContentTransform);
                        currentCell.GetComponent<BuildingStoreItemModel>()
                            .InitializeModel(buildingData,
                            _uiSpritesWrapper.BuildingImages, 
                            _uiSpritesWrapper.ResourcesImages, 
                            _storeHelper,
                            _gameDataPool.Buildings,
                            _detailWindowParent);
                        currentCell.GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
                    }
                    break;

                case TableType.Squads:
                    break;
            }
        }
    }
}