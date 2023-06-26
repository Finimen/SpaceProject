using Assets.Scripts.SpaceShip;
using Assets.Scripts.CameraSystem;
using Assets.Scripts.GeneratorSystem;
using Assets.Scripts.ResourcesSystem;
using Assets.Scripts.TreadingSystem;
using Assets.Scripts.WeaponInstallationSystem;
using UnityEngine;

namespace Assets.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private bool _enabled;

        private void Awake()
        {
            if (!_enabled)
            {
                return;
            }

            World.Initialize();

            FindObjectOfType<PlayerCamera>().Initialize();

            FindObjectOfType<WorldGenerator>().Initialize();

            foreach (var ship in FindObjectsOfType<Ship>(true))
            {
                ship.Initialize();
            }

            foreach(var treadingPoint in FindObjectsOfType<TreadingPoint>(true))
            {
                treadingPoint.Initialize();
            }

            foreach(var weaponInstaller in FindObjectsOfType<WeaponInstaller>(true))
            {
                weaponInstaller.Initialize();
            }

            foreach(var collector in FindObjectsOfType<ResourcesCollector>(true))
            {
                collector.Initialize();
            }



            foreach(var shipUI in FindObjectsOfType<ShipMovementUI>(true))
            {
                shipUI.Initialize();
            }

            foreach (var handler in FindObjectsOfType<ResourcesHandlerUI>(true))
            {
                handler.Initialize();
            }
        }
    }
}