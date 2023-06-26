using Assets.Scripts.PortSystem;
using Assets.Scripts.SpaceShip;
using UnityEngine;

namespace Assets.Scripts.WeaponInstallationSystem
{
    [RequireComponent(typeof(Port))]
    public class WeaponInstaller : MonoBehaviour, IInitializable
    {
        private WeaponInstallerCanvas _installerCanvas;

        private bool _active;

        public void Initialize()
        {
            _installerCanvas = FindObjectOfType<WeaponInstallerCanvas>(true);

            GetComponent<Port>().SetLeavePortButton(_installerCanvas.LeavePortButton);

            GetComponent<Port>().OnShipEnter += (ship) =>
            {
                _active = true;

                _installerCanvas.gameObject.SetActive(_active);

                ship.SetState(Ship.ShipState.WeaponInstallation);

                ship.OnSelectedForUpgrades += () =>
                {
                    _installerCanvas.gameObject.SetActive(true);
                };

                ship.OnDeselected += () => _installerCanvas.gameObject.SetActive(false);
            };
            GetComponent<Port>().OnShipLeave += (ship) =>
            {
                _active = false;

                _installerCanvas.gameObject.SetActive(_active);

                ship.SetState(Ship.ShipState.Gameplay);
            };

            _installerCanvas.gameObject.SetActive(false);
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
            _installerCanvas.GCoins.text = $"GCoins: {World.PlayerGCoins}";
        }
    }
}