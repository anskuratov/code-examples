using UnityEngine;
using Zenject;

namespace Assets.Sources.Store
{
    public abstract class TableController : MonoBehaviour
    {
        [SerializeField]
        protected TableType _tableType;

        [SerializeField]
        protected Transform _tableContentTransform;

        [SerializeField]
        protected GameObject _tableCellPrefab;

        [SerializeField]
        protected Transform _detailWindowParent;

        protected GameDataPoolBehaviour _gameDataPool;
        protected UiSpritesWrapper _uiSpritesWrapper;

        [Inject]
        protected void ZenjectInit(GameDataPoolBehaviour gameDataPool,
            UiSpritesWrapper uiSpritesWrapper)
        {
            _gameDataPool = gameDataPool;
            _uiSpritesWrapper = uiSpritesWrapper;
        }

        protected void ClearTableContent()
        {
            foreach (Transform child in _tableContentTransform)
                Destroy(child.gameObject);
        }
    }

    public enum TableType
    {
        Buildings,
        Squads
    }
}