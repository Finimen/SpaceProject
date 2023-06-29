using Assets.Scripts.PortSystem;
using Assets.Scripts.TreadingSystem;
using UnityEngine;

namespace Assets.Scripts.TrainingSystem
{
    public class TreadingModule : TrainingModule
    {
        [SerializeField] private Transform _player;

        [SerializeField] private Vector3 _spawnOffset;
        [SerializeField] private Port _port;

        public override void Complete()
        {
            _port.gameObject.SetActive(false);

            FindObjectOfType<Training>().NextState();
        }

        public override void Enable()
        {
            _port.gameObject.SetActive(true);
            _port.transform.position = _player.position + _spawnOffset;

            _port.OnShipLeave += (player) => Complete();
        }
    }
}