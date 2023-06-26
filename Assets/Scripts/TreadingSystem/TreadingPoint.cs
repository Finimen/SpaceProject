using Assets.Scripts.PortSystem;
using Assets.Scripts.ResourcesSystem;
using Assets.Scripts.SpaceShip;
using UnityEngine;

namespace Assets.Scripts.TreadingSystem
{
    [RequireComponent(typeof(Port))]
    internal class TreadingPoint : MonoBehaviour, IInitializable
    {
        [SerializeField] private float _defaultPrice;
        [SerializeField] private float _redPrice;
        [SerializeField] private float _greenPrice;

        private ResourcesHandler _currentHandler;
        private TreadingPointCanvas _treadingCanvas;

        public void Initialize()
        {
            _treadingCanvas = FindObjectOfType<TreadingPointCanvas>(true);

            GetComponent<Port>().SetLeavePortButton(_treadingCanvas.LeavePortButton);

            GetComponent<Port>().OnShipEnter += (ship) =>
            {
                ship.SetState(Ship.ShipState.Trading);

                _currentHandler = ship.Handler;

                UpdateUI();

                if (ship != null)
                {
                    ship.OnSelectedForTreading += () => _treadingCanvas.gameObject.SetActive(true);
                    ship.OnDeselected += () => _treadingCanvas.gameObject.SetActive(false);
                }
            };

            _treadingCanvas.Default.Sell.onClick.AddListener(() => SellDefault(10));
            _treadingCanvas.Default.SellAll.onClick.AddListener(() => SellDefault(_currentHandler.DefaultOre));

            _treadingCanvas.Red.Sell.onClick.AddListener(() => SellRed(10));
            _treadingCanvas.Red.SellAll.onClick.AddListener(() => SellRed(_currentHandler.RedOre));

            _treadingCanvas.Green.Sell.onClick.AddListener(() => SellGreen(10));
            _treadingCanvas.Green.SellAll.onClick.AddListener(() => SellGreen(_currentHandler.GreenOre));
        }

        public void SetResourcesHandler(ResourcesHandler resourcesHandler)
        {
            _currentHandler = resourcesHandler;
        }

        private void SellDefault(int amount)
        {
            SellResource(amount, _currentHandler.DefaultOre, _defaultPrice, OreType.Default);
        }

        private void SellRed(int amount)
        {
            SellResource(amount, _currentHandler.RedOre, _redPrice, OreType.Red);
        }

        private void SellGreen(int amount)
        {
            SellResource(amount, _currentHandler.GreenOre, _greenPrice, OreType.Green);
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
            _treadingCanvas.GCoins.text = $"GCoins: {World.PlayerGCoins}";

            _treadingCanvas.Default.Name.text = $"Default Ore: x{_currentHandler.DefaultOre} / x1 = {_defaultPrice} GCoin";
            _treadingCanvas.Red.Name.text = $"Red Ore: x{_currentHandler.RedOre} / x1 = {_redPrice} GCoin";
            _treadingCanvas.Green.Name.text = $"Green Ore: x{_currentHandler.GreenOre} / x1 = {_greenPrice} GCoin";
        }
    }
}