using Assets.Scripts.SpaceShip;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Ship))]
    public class ShipAI : MonoBehaviour
    {
        [SerializeField] private float _distanceToAttack = 5;
        [SerializeField] private float _waypointDistance = 1;

        private Vector3 _target;

        private Ship _ship;
        private Transform _transform;

        private void OnEnable()
        {
            _ship = GetComponent<Ship>();
            _transform = GetComponent<Transform>();

            _target = transform.position;
        }

        private void FixedUpdate()
        {
            TryFindEnemy();

            UpdateIdleState();
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
                if (Vector3.Distance(_transform.position, entity.transform.position) < _distanceToAttack)
                {
                    _target = entity.transform.position;

                    _ship.Movement.SetTargetPoint(_target);
                }
            }
        }
    }
}