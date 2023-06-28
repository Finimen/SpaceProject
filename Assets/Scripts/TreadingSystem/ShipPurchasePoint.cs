using Assets.Scripts.PortSystem;
using Assets.Scripts.ResourcesSystem;
using Assets.Scripts.SpaceShip;
using Assets.Scripts.WeaponSystem;
using UnityEngine;

namespace Assets.Scripts.TreadingSystem
{
    [RequireComponent(typeof(Port))]
    public class ShipPurchasePoint : MonoBehaviour, IInitializable
    {
        [SerializeField] private Ship _shipForSale;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _entitiesParent;

        [SerializeField] private float _price;

        private ShipPurchasePointCanvas _canvas;

        public void Initialize()
        {
            _canvas = FindObjectOfType<ShipPurchasePointCanvas>(true);

            SpawnShip();

            SubscribeToEvents();
        }

        private void SpawnShip()
        {
            _shipForSale = Instantiate(_shipForSale, _spawnPoint.transform.position, _spawnPoint.transform.rotation, transform);
            _shipForSale.Initialize();
            _shipForSale.SetEnableForShooting(false);

            _shipForSale.Collector.enabled = false;
            _shipForSale.GetComponent<ResourcesCollectorUI>().enabled = false;

            foreach (var triggerUI in _shipForSale.GetComponentsInChildren<WeaponTriggerUI>())
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

                ship.OnSelectedForTreading += () => _canvas.gameObject.SetActive(true);
                ship.OnDeselected += () => _canvas.gameObject.SetActive(false);

                UpdateUI();
            };
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
            var playerInput = _shipForSale.gameObject.AddComponent<PlayerShipInput>();
            playerInput.Initialize();

            _shipForSale.SetPlayerInput(playerInput);
            _shipForSale.transform.parent = _entitiesParent;
            
            _canvas.ByShipButton.gameObject.SetActive(false);

            _shipForSale.Collector.enabled = true;
            _shipForSale.GetComponent<ResourcesCollectorUI>().enabled = true;

            foreach (var triggerUI in _shipForSale.GetComponentsInChildren<WeaponTriggerUI>(true))
            {
                triggerUI.enabled = true;
            }
        }
    }
}