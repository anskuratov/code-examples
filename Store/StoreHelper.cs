using Assets.Sources.Utils;
using SorResources.Models;
using SorResources.Models.Inventory;
using SorResources.Models.Static;
using System;
using Zenject;

namespace Assets.Sources.Store
{
    public class StoreHelper
    {
        private WebManagerBehaviour _webManager;
        private ApiRoutes _apiRoutes;

        [Inject]
        public void ZenjectInit(WebManagerBehaviour webManager,
            ApiRoutes apiRoutes)
        {
            _webManager = webManager;
            _apiRoutes = apiRoutes;
        }

        public void GetBuildingsStoreCatalog(Action<StaticModel[], ResponseCode> responseHandler)
            => _webManager.SendWithValue<SBuilding[]>(_apiRoutes.Building.Static(), responseHandler);

        public void BuyBuilding(string buildingId, Action<BuildingModel, ResponseCode> responseHandler)
            => _webManager.SendWithValue<BuildingModel>(_apiRoutes.Building.Buy(buildingId), responseHandler);

        public void SellBuilding(string buildingId, Action<ResponseCode> responseHandler)
            => _webManager.Send(_apiRoutes.Building.Sell(buildingId), responseHandler);

        public void GetSquadsStoreCatalog(Action<StaticModel[], ResponseCode> responseHandler)
            => _webManager.SendWithValue<SSquad[]>(_apiRoutes.Squad.Static(), responseHandler);
    }
}
