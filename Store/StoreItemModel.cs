using Assets.Sources.Storage;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.Store
{
    [RequireComponent(typeof(Toggle))]
    public abstract class StoreItemModel : ItemModel
    {
        [SerializeField]
        protected Transform _prices;

        [SerializeField]
        protected GameObject _pricePrefab;
    }
}
