using Assets.Scripts.ResourcesSystem;
using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    [RequireComponent(typeof(ShipMovement))]
    public class Ship : MonoBehaviour, IInitializable
    {
        public enum ShipState
        {
            Gameplay,
            Trading
        }

        [SerializeField] private ShipState _state;

        private ShipMovement _movement;
        private ResourcesCollector _collector;
        private PlayerShipInput _playerShipInput;

        public ShipState State => _state;

        void IInitializable.Initialize()
        {
            _movement = GetComponent<ShipMovement>();

            if (GetComponent<ResourcesCollector>() != null)
            {
                _collector = GetComponent<ResourcesCollector>();
            }
            
            if(GetComponent<PlayerShipInput>() != null)
            {
                _playerShipInput = GetComponent<PlayerShipInput>();
            }

            SetState(_state);
        }

        public void SetState(ShipState state)
        {
            _state = state;

            switch (_state)
            {
                case ShipState.Gameplay:
                    _movement.enabled = true;

                    if(_collector != null)
                    {
                        _collector.enabled = true;
                    }

                    if(_playerShipInput != null)
                    {
                        _playerShipInput.enabled = true;
                    }
                    break;

                case ShipState.Trading:
                    _movement.enabled = false;

                    if (_collector != null)
                    {
                        _collector.enabled = false;
                    }

                    if (_playerShipInput != null)
                    {
                        _playerShipInput.enabled = false;
                    }
                    break;
            }
        }
    }
}