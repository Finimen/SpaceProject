using Assets.Scripts.PortSystem;
using Assets.Scripts.ResourcesSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.TreadingSystem
{
    [RequireComponent(typeof(Port))]
    internal class TreadingPoint : MonoBehaviour, IInitializable
    {
        [Serializable]
        private struct Resource
        {
            public float Price;

            public Button Sell;
            public Button SellAll;
            public TMP_Text Name;
        }

        [SerializeField] private GameObject _canvas;

        [SerializeField] private TMP_Text _gCoins;

        [SerializeField] private Resource _default;
        [SerializeField] private Resource _red;
        [SerializeField] private Resource _green;

        private ResourcesHandler _currentHandler;

        public void Initialize()
        {
            GetComponent<Port>().OnShipEnter += (ship) =>
            {
                _currentHandler = ship.Handler;

                _canvas.gameObject.SetActive(true);

                UpdateUI();

                if (ship != null)
                {
                    ship.OnSelectedForTreading += () => _canvas.gameObject.SetActive(true);
                    ship.OnDeselected += () => _canvas.gameObject.SetActive(false);
                }
            };

            _default.Sell.onClick.AddListener(() => SellDefault(10));
            _default.SellAll.onClick.AddListener(() => SellDefault(_currentHandler.DefaultOre));

            _red.Sell.onClick.AddListener(() => SellRed(10));
            _red.SellAll.onClick.AddListener(() => SellRed(_currentHandler.RedOre));

            _green.Sell.onClick.AddListener(() => SellGreen(10));
            _green.SellAll.onClick.AddListener(() => SellGreen(_currentHandler.GreenOre));
        }

        public void SetResourcesHandler(ResourcesHandler resourcesHandler)
        {
            _currentHandler = resourcesHandler;
        }

        private void SellDefault(int amount)
        {
            SellResource(amount, _currentHandler.DefaultOre, _default.Price, OreType.Default);
        }

        private void SellRed(int amount)
        {
            SellResource(amount, _currentHandler.RedOre, _red.Price, OreType.Red);
        }

        private void SellGreen(int amount)
        {
            SellResource(amount, _currentHandler.GreenOre, _green.Price, OreType.Green);
        }

        private void SellResource(int amount, int availableAmount, float price, OreType oreType)
        {
            if (availableAmount >= amount)
            {
                _currentHandler.DecreaseOre(amount, oreType);

                World.IncreasePlayerCoins(amount * price);

                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            _gCoins.text = $"GCoins: {World.PlayerGCoins}";

            _default.Name.text = $"Default Ore: x{_currentHandler.DefaultOre} / x1 = {_default.Price} GCoin";
            _red.Name.text = $"Red Ore: x{_currentHandler.RedOre} / x1 = {_red.Price} GCoin";
            _green.Name.text = $"Green Ore: x{_currentHandler.GreenOre} / x1 = {_green.Price} GCoin";
        }
    }
}