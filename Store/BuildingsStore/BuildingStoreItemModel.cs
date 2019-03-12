using SorResources.Models;
using SorResources.Models.Enums;
using SorResources.Models.Inventory;
using SorResources.Models.Static;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.Store
{
    public class BuildingStoreItemModel : StoreItemModel
    {
        private SBuilding _sBuildingModel;

        private List<BuildingModel> _buildings;

        public void InitializeModel(SBuilding sBuilding,
            Dictionary<BuildingType, Sprite> buildingsSprites,
            Dictionary<ResourceType, Sprite> resourcesSprites,
            StoreHelper storeHelper,
            List<BuildingModel> buildings,
            Transform detailWindowParent)
        {
            _sBuildingModel = sBuilding;
            _buildings = buildings;

            InitializeModel(_sBuildingModel.Name,
                buildingsSprites[_sBuildingModel.Type],
                _sBuildingModel.Id,
                detailWindowParent);

            foreach (var price in sBuilding.Price)
                Instantiate(_pricePrefab, _prices).GetComponent<PriceModel>()
                    .InitializeModel(price.Amount, resourcesSprites[price.Type], price.Type);

            GetComponent<Toggle>().onValueChanged.AddListener(delegate
            {
                if (_detailWindowObject != null)
                    Destroy(_detailWindowObject);
                else
                {
                    _detailWindowObject = Instantiate(_detailWindowPrefab, _detailWindowParent);
                    var detailWindowModel = _detailWindowObject.GetComponent<StoreDetailWindowModel>();

                    detailWindowModel.InitializeModel(_sBuildingModel.Name,
                        _sBuildingModel.Description,
                        _sBuildingModel.Price,
                        _pricePrefab,
                        resourcesSprites,
                        () =>
                        {
                            storeHelper.BuyBuilding(_sBuildingModel.Id,
                            (buildingModel, reponseCode) =>
                            {
                                switch (reponseCode)
                                {
                                    case ResponseCode.Success:
                                        buildings.Add(buildingModel);
                                        break;
                                    default:
                                        break;
                                }
                            });
                        });
                }
            });
        }
    }
}
