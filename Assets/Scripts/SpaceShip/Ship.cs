using Assets.Scripts.CameraSystem;
using Assets.Scripts.Players;
using Assets.Scripts.ResourcesSystem;
using Assets.Scripts.WeaponInstallationSystem;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.SpaceShip
{
    /// <summary>
    /// Ship main script and entry point to its components
    /// </summary>
    
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ShipMovement), typeof(ShipDamageDealer))]
    public class Ship : MonoBehaviour, IInitializable, IPointerEnterHandler, IPointerExitHandler
    {
        public enum ShipState
        {
            Gameplay,
            Trading,
            WeaponInstallation
        }

        public event Action OnSelectedForMoving;
        public event Action OnSelectedForTreading;
        public event Action OnSelectedForUpgrades;
        public event Action OnDeselected;

        [SerializeField] private ShipState _state;

        [SerializeField, Space(25)] private Collider2D[] _ignoreCollidersForWeapons;

        private WeaponInstallationPoint[] _weaponInstallationPoints;

        private ShipMovement _movement;
        private ResourcesCollector _collector;
        private ResourcesHandler _handler;
        private PlayerShipInput _playerShipInput;
        private ShipDamageDealer _damageDealer;

        private PlayerCamera _camera;

        private bool _mouseEntered;
        private bool _isSelected;

        public ShipState State => _state;

        public ShipDamageDealer DamageDealer => _damageDealer;
        public ShipMovement Movement => _movement;
        public ResourcesCollector Collector => _collector;
        public ResourcesHandler Handler => _handler;
        public PlayerShipInput PlayerInput => _playerShipInput;

        public Collider2D[] IgnoreCollidersForWeapons => _ignoreCollidersForWeapons;

        public void Initialize()
        {
            _camera = FindObjectOfType<PlayerCamera>();

            InitializeRequiredComponents();

            InitializeAdditionalComponents();

            SetState(_state);

            World.Ships.Add(this);
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

                    _collector.DisableCurrent();
                    _collector.enabled = false;

                    _playerShipInput.DisableInput();

                    OnDeselected?.Invoke();
                    OnSelectedForTreading?.Invoke();
                    break;
                    
                case ShipState.WeaponInstallation:
                    _movement.enabled = false;

                    _collector.DisableCurrent();
                    _collector.enabled = false;

                    _playerShipInput.DisableInput();

                    foreach (var weaponPoint in _weaponInstallationPoints)
                    {
                        weaponPoint.Enable(true);
                    }
                    break;
            }
        }

        public void SetEnableForShooting(bool enabled)
        {
            foreach(var  weaponPoint in _weaponInstallationPoints)
            {
                if (weaponPoint.Current != null)
                {
                    weaponPoint.Current.enabled = enabled;
                }
            }
        }

        public void SetPlayerInput(PlayerShipInput playerInput)
        {
            _playerShipInput = playerInput;
        }

        private void InitializeRequiredComponents()
        {
            _movement = GetComponent<ShipMovement>();
            _movement.Initialize();

            _damageDealer = GetComponent<ShipDamageDealer>();
            _damageDealer.Initialize();
        }

        private void InitializeAdditionalComponents()
        {
            if (GetComponent<ResourcesHandler>() != null)
            {
                _collector = GetComponent<ResourcesCollector>();

                _collector.Initialize();
            }

            if (GetComponent<PlayerShipInput>() != null)
            {
                _playerShipInput = GetComponent<PlayerShipInput>();

                _playerShipInput.Initialize();
            }

            if (GetComponent<ResourcesHandler>() != null)
            {
                _handler = GetComponent<ResourcesHandler>();
            }

            _weaponInstallationPoints = GetComponentsInChildren<WeaponInstallationPoint>();

            foreach (var weaponPoint in _weaponInstallationPoints)
            {
                weaponPoint.Initialize(_ignoreCollidersForWeapons);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null)
            {
                _isSelected = _mouseEntered;

                if (_isSelected)
                {
                    _playerShipInput?.EnableInput();

                    switch (_state)
                    {
                        case ShipState.Trading:
                            OnSelectedForTreading?.Invoke();

                            _camera.SetDestination(transform.position);
                            _camera.EnableInput = false;
                            break;
                        
                        case ShipState.Gameplay:
                            OnSelectedForMoving?.Invoke();
                            break;

                        case ShipState.WeaponInstallation:
                            OnSelectedForUpgrades?.Invoke();

                            _camera.SetDestination(transform.position);
                            _camera.EnableInput = false;
                            break;
                    }
                }
                else
                {
                    _playerShipInput?.DisableInput();
                    OnDeselected?.Invoke();

                    _camera.EnableInput = true;
                }
            }
        }

        private void OnDisable()
        {
            World.Ships.Remove(this);
        }
    }
}