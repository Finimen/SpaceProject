using Assets.Scripts.SpaceShip;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(Ship))]
    public class ShipAI : MonoBehaviour, IInitializable
    {
        private enum AIType
        {
            Friendly,
            Passive,
        }

        [SerializeField] private AIType _type;

        [SerializeField] private float _distanceToAttack = 5;
        [SerializeField] private float _waypointDistance = 1;

        private Vector3 _target;

        private Ship _ship;
        private ShipDamageDealer _damageable;
        private Transform _transform;
        private BotMovement _movement;

        public void Initialize()
        {
            _ship = GetComponent<Ship>();
            _transform = GetComponent<Transform>();
            _damageable = GetComponent<ShipDamageDealer>();

            _target = transform.position;

            _movement = (BotMovement)_ship.Movement;

            foreach (var weapon in GetComponentsInChildren<WeaponRandomizer>())
            {
                weapon.Initialize(_ship.IgnoreCollidersForWeapons);
            }
        }

        private void FixedUpdate()
        {
            if(_type == AIType.Passive)
            {
                TryFindEnemy();
            }

            UpdateIdleState();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, _target);
        }

        private void UpdateIdleState()
        {
            if (Vector3.Distance(transform.position, _target) < _waypointDistance)
            {
                _target = transform.up * Random.Range(-50, 51) + _transform.right * Random.Range(-5, 6);

                _movement.SetTargetPoint(_target);
            }
        }

        private void TryFindEnemy()
        {
            foreach (var entity in World.Ships)
            {
                if (entity.State == Ship.ShipState.Gameplay && entity.DamageDealer.Id != _damageable.Id && Vector3.Distance(_transform.position, entity.transform.position) < _distanceToAttack)
                {
                    _target = entity.transform.position;

                    _movement.SetTargetPoint(_target);
                }
            }
        }
    }
}