using Assets.Scripts.PortSystem;
using Assets.Scripts.SpaceShip;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.WeaponInstallationSystem
{
    [RequireComponent(typeof(Port))]
    public class WeaponInstaller : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject _canvas;

        [SerializeField] private TMP_Text _gCoins;

        private bool _active;

        public void Initialize()
        {
            GetComponent<Port>().OnShipEnter += (ship) =>
            {
                _active = true;

                _canvas.SetActive(_active);

                ship.SetState(Ship.ShipState.WeaponInstallation);
            };
            GetComponent<Port>().OnShipLeave += (ship) =>
            {
                _active = false;

                _canvas.SetActive(_active);

                ship.SetState(Ship.ShipState.Gameplay);
            };

            _canvas.SetActive(false);
        }

        private void FixedUpdate()
        {
            if (_active)
            {
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            _gCoins.text = $"GCoins: {World.PlayerGCoins}";
        }
    }
}