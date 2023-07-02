using Assets.Scripts.Players;
using Assets.Scripts.ResourcesSystem;
using Assets.Scripts.WeaponInstallationSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    /// <summary>
    /// Ship main script and entry point to its components
    /// </summary>
    
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ShipDamageDealer))]
    public class Ship : MonoBehaviour, IInitializable
    {
        public enum ShipState
        {
            Gameplay,
            Trading,
            WeaponInstallation
        }

        public event Action<ShipState> OnStateUpdated;

        [SerializeField] private ShipState _state;

        [SerializeField, Space(25)] private Collider2D[] _ignoreCollidersForWeapons;

        private WeaponInstallationPoint[] _weaponInstallationPoints;

        private ShipMovement _movement;
        private ResourcesCollector _collector;
        private ResourcesHandler _handler;
        private PlayerShipInput _playerShipInput;
        private ShipDamageDealer _damageDealer;

        public ShipState State => _state;

        public ShipDamageDealer DamageDealer => _damageDealer;
        public ShipMovement Movement => _movement;
        public ResourcesCollector Collector => _collector;
        public ResourcesHandler Handler => _handler;
        public PlayerShipInput PlayerInput => _playerShipInput;

        public Collider2D[] IgnoreCollidersForWeapons => _ignoreCollidersForWeapons;

        public void Initialize()
        {
            InitializeRequiredComponents();

            InitializeAdditionalComponents();

            SetState(_state);

            World.Ships.Add(this);
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
                    
                    break;

                case ShipState.Trading:
                    _movement.enabled = false;

                    _collector.DisableCurrent();
                    _collector.enabled = false;

                    break;
                    
                case ShipState.WeaponInstallation:
                    _movement.enabled = false;

                    _collector.DisableCurrent();
                    _collector.enabled = false;

                    foreach (var weaponPoint in _weaponInstallationPoints)
                    {
                        weaponPoint.Enable(true);
                    }
                    break;
            }

            OnStateUpdated?.Invoke(_state);
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

        private void OnDisable()
        {
            World.Ships.Remove(this);
        }
    }
}