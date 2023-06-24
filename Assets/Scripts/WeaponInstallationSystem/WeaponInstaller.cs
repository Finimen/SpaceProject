using Assets.Scripts.PortSystem;
using Assets.Scripts.SpaceShip;
using UnityEngine;

namespace Assets.Scripts.WeaponInstallationSystem
{
    [RequireComponent(typeof(Port))]
    public class WeaponInstaller : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject _canvas;

        void IInitializable.Initialize()
        {
            GetComponent<Port>().OnShipEnter += (ship) =>
            {
                _canvas.SetActive(true);

                ship.SetState(Ship.ShipState.WeaponInstallation);
            };
            GetComponent<Port>().OnShipLeave += (ship) =>
            {
                _canvas.SetActive(false);

                ship.SetState(Ship.ShipState.Gameplay);
            };
        }
    }
}