using Assets.Scripts.SpaceShip;
using Assets.Scripts.CameraSystem;
using Assets.Scripts.GeneratorSystem;
using Assets.Scripts.ResourcesSystem;
using Assets.Scripts.TreadingSystem;
using Assets.Scripts.WeaponInstallationSystem;
using UnityEngine;
using Assets.Scripts.AI;
using Assets.Scripts.PoolSystem;

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

            foreach (var ship in FindObjectsOfType<Ship>(true))
            {
                ship.Initialize();
            }

            foreach (var ship in FindObjectsOfType<ShipAI>(true))
            {
                ship.Initialize();
            }

            foreach (var treadingPoint in FindObjectsOfType<TreadingPoint>(true))
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

            FindObjectOfType<WorldGenerator>().Initialize();

            FindObjectOfType<ObjectPool>().Initialize();

            foreach (var handler in FindObjectsOfType<ResourcesHandlerUI>(true))
            {
                handler.Initialize();
            }
        }
    }
}