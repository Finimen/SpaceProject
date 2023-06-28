using Assets.Scripts.Damageable;
using Assets.Scripts.SpaceShip;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(Ship))]
    public class ShipAI : MonoBehaviour
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
        private ShipDamageable _damageable;
        private Transform _transform;

        private void OnEnable()
        {
            _ship = GetComponent<Ship>();
            _transform = GetComponent<Transform>();
            _damageable = GetComponent<ShipDamageable>();

            _target = transform.position;

            foreach(var weapon in GetComponentsInChildren<WeaponRandomizer>())
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
                _target = transform.up * Random.Range(5, 50) + _transform.right * Random.Range(-5, 5);

                _ship.Movement.SetTargetPoint(_target);
            }
        }

        private void TryFindEnemy()
        {
            foreach (var entity in World.Entities)
            {
                if (entity.Id != _damageable.Id && Vector3.Distance(_transform.position, entity.transform.position) < _distanceToAttack)
                {
                    _target = entity.transform.position;

                    _ship.Movement.SetTargetPoint(_target);
                }
            }
        }
    }
}