using SorResources.Models.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.Store
{
    public class PriceModel : MonoBehaviour
    {
        private static readonly Color _moneyColor = new Color(224f, 197f, 88f);
        private static readonly Color _premiumMoneyColor = new Color(255f, 255f, 255f);
        private static readonly Color _oilColor = new Color(187f, 56f, 192f);
        private static readonly Color _refinedOilColor = new Color(139f, 76f, 255f);
        private static readonly Color _benzineColor = new Color(255f, 23f, 23f);
        private static readonly Color _dieselColor = new Color(94f, 255f, 189f);
        private static readonly Color _energyColor = new Color(255f, 138f, 0f);

        private readonly Color[] _resourceColors =
        {
        _moneyColor,
        _premiumMoneyColor,
        _oilColor,
        _refinedOilColor,
        _benzineColor,
        _dieselColor,
        _energyColor
    };

        [SerializeField]
        private Text _priceCost;

        [SerializeField]
        private Image _resourceType;

        [SerializeField]
        private Image _priceBackground;

        public void InitializeModel(long price, Sprite resourceTexture, ResourceType resourceType)
        {
            _priceCost.text = price.ToString();
            _resourceType.sprite = resourceTexture;
            _priceBackground.color = _resourceColors[(int)resourceType];
        }
    }
}