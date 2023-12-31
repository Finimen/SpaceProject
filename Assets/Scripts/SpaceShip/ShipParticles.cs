using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    public class ShipParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _engine;

        [SerializeField] private float _speedMultiplayer = 100;
        [SerializeField] private float _minSpeed = .1f;

        private ShipMovement _shipMovement;

        private ParticleSystem.EmissionModule _emission;

        private void OnEnable()
        {
            _shipMovement = GetComponent<ShipMovement>();
            _emission = _engine.emission;
        }

        private void FixedUpdate()
        {
            if(_shipMovement.Speed >  _minSpeed)
            {
                _emission.rateOverTime = _shipMovement.Speed * _speedMultiplayer;
            }
            else
            {
                _emission.rateOverTime = 0;
            }
        }
    }
}