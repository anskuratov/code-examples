using SorResources.Models.Enums;
using SorResources.Models.Types;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.Store
{
    public class StoreDetailWindowModel : DetailWindowModel
    {
        [SerializeField]
        private Transform _price;

        [SerializeField]
        private Button _buyButton;

        public void InitializeModel(string name,
            string description,
            List<ResourseModel> prices,
            GameObject pricePrefab,
            Dictionary<ResourceType, Sprite> resourcesSprites,
            Action buyAction)
        {
            _name.text = name;

            _description.text = description;

            foreach (var price in prices)
                Instantiate(pricePrefab, _price).GetComponent<PriceModel>()
                    .InitializeModel(price.Amount, resourcesSprites[price.Type], price.Type);

            _buyButton.onClick.AddListener(delegate { buyAction(); });
        }
    }
}