using Assets.Scripts.ResourcesSystem;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.SpaceShip
{
    [RequireComponent(typeof(ShipMovement))]
    public class Ship : MonoBehaviour, IInitializable, IPointerEnterHandler, IPointerExitHandler
    {
        public enum ShipState
        {
            Gameplay,
            Trading
        }

        public event Action OnSelectedForMoving;
        public event Action OnSelectedForTreading;
        public event Action OnDeselected;

        [SerializeField] private ShipState _state;

        private ShipMovement _movement;
        private ResourcesCollector _collector;
        private ResourcesHandler _handler;
        private PlayerShipInput _playerShipInput;

        private bool _mouseEntered;
        private bool _isSelected;

        public ShipState State => _state;

        public ShipMovement Movement => _movement;
        public ResourcesCollector Collector => _collector;
        public ResourcesHandler Handler => _handler;
        public PlayerShipInput PlayerShipInput => _playerShipInput;

        void IInitializable.Initialize()
        {
            _movement = GetComponent<ShipMovement>();

            ((IInitializable)_movement).Initialize();

            if (GetComponent<ResourcesHandler>() != null)
            {
                _collector = GetComponent<ResourcesCollector>();

                ((IInitializable)_collector).Initialize();
            }
            
            if(GetComponent<PlayerShipInput>() != null)
            {
                _playerShipInput = GetComponent<PlayerShipInput>();

                ((IInitializable)_playerShipInput).Initialize();
            }

            if (GetComponent<ResourcesHandler>() != null)
            {
                _handler = GetComponent<ResourcesHandler>();
            }

            SetState(_state);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            _mouseEntered = true;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _mouseEntered = false;
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

                    OnDeselected?.Invoke();
                    OnSelectedForMoving?.Invoke();
                    break;

                case ShipState.Trading:
                    _movement.enabled = false;

                    if (_collector != null)
                    {
                        _collector.enabled = false;
                    }

                    if (_playerShipInput != null)
                    {
                        _playerShipInput.DisableInput();
                    }

                    OnDeselected?.Invoke();
                    OnSelectedForTreading?.Invoke();
                    break;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null)
            {
                _isSelected = _mouseEntered;

                if (_isSelected)
                {
                    _playerShipInput.EnableInput();

                    switch (_state)
                    {
                        case ShipState.Trading:
                            OnSelectedForTreading?.Invoke();
                            break;
                        
                        case ShipState.Gameplay:
                            OnSelectedForMoving?.Invoke();
                            break;
                    }
                }
                else
                {
                    _playerShipInput.DisableInput();
                    OnDeselected?.Invoke();
                }
            }
        }
    }
}