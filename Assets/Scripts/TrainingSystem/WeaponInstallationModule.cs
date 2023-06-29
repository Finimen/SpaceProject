using Assets.Scripts.PortSystem;
using Assets.Scripts.TreadingSystem;
using UnityEngine;

namespace Assets.Scripts.TrainingSystem
{
    internal class WeaponInstallationModule : TrainingModule
    {
        [SerializeField] private Port _port;

        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 _offset;

        public override void Complete()
        {
            _port.gameObject.SetActive(false);

            FindObjectOfType<Training>().NextState();
        }

        public override void Enable()
        {
            World.IncreasePlayerCoins(100);

            _port.gameObject.SetActive(true);
            _port.transform.position = _player.position + _offset;

            _port.OnShipLeave += (player) => Complete();
        }
    }
}