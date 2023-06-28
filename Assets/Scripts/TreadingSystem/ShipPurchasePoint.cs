using Assets.Scripts.PortSystem;
using Assets.Scripts.ResourcesSystem;
using Assets.Scripts.SpaceShip;
using Assets.Scripts.WeaponSystem;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Assets.Scripts.TreadingSystem
{
    [RequireComponent(typeof(Port))]
    public class ShipPurchasePoint : MonoBehaviour, IInitializable
    {
        [Serializable]
        private struct ShipData
        {
            public Ship Ship;

            public float MinPrice;
            public float MaxPrice;
        }

        [SerializeField] private ShipData[] _shipsForSale;

        [Space(25)]
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _entitiesParent;

        private ShipPurchasePointCanvas _canvas;
        private Ship _currentShip;

        private int _price;

        public void Initialize()
        {
            _canvas = FindObjectOfType<ShipPurchasePointCanvas>(true);

            SpawnShip();

            SubscribeToEvents();
        }

        private void SpawnShip()
        {
            var index = Random.Range(0, _shipsForSale.Length);

            _currentShip = Instantiate(_shipsForSale[index].Ship, _spawnPoint.transform.position, _spawnPoint.transform.rotation, transform);
            _currentShip.Initialize();
            _currentShip.SetEnableForShooting(false);

            _currentShip.Collector.enabled = false;
            _currentShip.GetComponent<ResourcesCollectorUI>().enabled = false;

            _price = (int)Random.Range(_shipsForSale[index].MinPrice, _shipsForSale[index].MaxPrice);

            foreach (var triggerUI in _currentShip.GetComponentsInChildren<WeaponTriggerUI>())
            {
                triggerUI.enabled = false;
            }
        }

        private void SubscribeToEvents()
        {
            GetComponent<Port>().SetLeavePortButton(_canvas.LeavePortButton);
            GetComponent<Port>().OnShipEnter += (ship) =>
            {
                ship.SetState(Ship.ShipState.Trading);

                ship.OnSelectedForTreading += EnableCanvas;
                ship.OnDeselected += DisableCanvas;

                UpdateUI();
            };
            GetComponent<Port>().OnShipLeave += (ship) =>
            {
                ship.OnSelectedForTreading -= EnableCanvas;
                ship.OnDeselected -= DisableCanvas;

                _canvas.gameObject.SetActive(false);
            };
        }

        private void EnableCanvas()
        {
            _canvas.gameObject.SetActive(true);
        }

        private void DisableCanvas()
        {
            _canvas.gameObject.SetActive(false);
        }

        private void UpdateUI()
        {
            _canvas.GCoins.text = $"GCoins: {World.PlayerGCoins}";
            _canvas.Price.text = $"Price: {_price} GCoins";

            _canvas.ByShipButton.gameObject.SetActive(true);
            _canvas.ByShipButton.onClick.RemoveAllListeners();
            _canvas.ByShipButton.onClick.AddListener(ByShip);
        }

        private void ByShip()
        {
            var playerInput = _currentShip.gameObject.AddComponent<PlayerShipInput>();
            playerInput.Initialize();

            _currentShip.SetPlayerInput(playerInput);
            _currentShip.transform.parent = _entitiesParent;
            
            _canvas.ByShipButton.gameObject.SetActive(false);

            _currentShip.Collector.enabled = true;
            _currentShip.GetComponent<ResourcesCollectorUI>().enabled = true;

            foreach (var triggerUI in _currentShip.GetComponentsInChildren<WeaponTriggerUI>(true))
            {
                triggerUI.enabled = true;
            }
        }
    }
}