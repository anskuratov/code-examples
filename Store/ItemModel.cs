using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.Storage
{
    public abstract class ItemModel : MonoBehaviour
    {
        [SerializeField]
        protected Image _itemImage;

        [SerializeField]
        protected Text _itemNameText;
        
        [SerializeField]
        protected GameObject _detailWindowPrefab;
        protected GameObject _detailWindowObject;
        protected Transform _detailWindowParent;

        protected string _itemId;

        protected void InitializeModel(string itemName,
            Sprite itemSprite,
            string itemId,
            Transform detailWindowParent)
        {
            _itemNameText.text = itemName;
            _itemImage.sprite = itemSprite;
            _itemId = itemId;
            _detailWindowParent = detailWindowParent;
        }
    }
}
