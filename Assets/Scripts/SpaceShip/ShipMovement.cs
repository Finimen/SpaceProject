using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    internal class ShipMovement : MonoBehaviour, IInitializable
    {
        [SerializeField] private Vector3 _target;

        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;

        [SerializeField] private float _rotation;

        [SerializeField] private float _angelBetweenDirection;
        [SerializeField] private float _minAngleToMove = 15;

        [SerializeField] private float _stoppingDistance = 1;

        [SerializeField] private float _offset;

        private Transform _transform;

        private float _currentSpeed;

        private bool _wayPassed = true;

        void IInitializable.Initialize()
        {
            _transform = transform;
        }

        public void SetTargetPoint(Vector3 point)
        {
            _target = new Vector3(point.x, point.y, _transform.position.z);

            _wayPassed = false;
        }

        private void Update()
        {
            Move();

            if (!_wayPassed)
            {
                Rotate();
            }
            else
            {
                _currentSpeed = Mathf.Clamp(_currentSpeed - (_acceleration * Time.deltaTime), 0f, _maxSpeed);
                return;
            }

            if (_angelBetweenDirection < _minAngleToMove)
            {
                UpdateSpeed();
            }
        }

        private void Rotate()
        {
            Vector3 direction = _target - _transform.position;

            _angelBetweenDirection = Vector2.Angle(direction, _transform.right) - Mathf.Abs(_offset);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            _transform.rotation = Quaternion.Lerp(_transform.rotation,
                Quaternion.Euler(0f, 0f, angle + _offset), _rotation * Time.deltaTime);
        }

        private void UpdateSpeed()
        {
            if (Vector2.Distance(_transform.position, _target) > _stoppingDistance)
            {
                _currentSpeed = Mathf.Clamp(_currentSpeed + (_acceleration * Time.deltaTime), 0, _maxSpeed);
            }
            else 
            {
                _wayPassed = true;
            }
        }

        private void Move()
        {
            _transform.position += _transform.up * _currentSpeed * Time.deltaTime;
        }
    }
}