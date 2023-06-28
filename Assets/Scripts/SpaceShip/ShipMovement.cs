using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    public class ShipMovement : MonoBehaviour, IInitializable
    {
        [SerializeField] private Vector3 _target;

        [Space(25)]
        [SerializeField] private float _maxSpeed = 2;
        [SerializeField] private float _acceleration = .05f;

        [SerializeField] private float _rotationSpeed = 5;

        [Space(25)]
        [SerializeField] private float _minAngleToMove = 15;

        [SerializeField] private float _stoppingDistance = 1;

        [SerializeField] private float _offset = -90;

        private Transform _transform;
        
        private float _angelBetweenDirection;

        private float _currentSpeed;

        private bool _wayPassed = true;

        public float Speed
        {
            get
            {
                return _currentSpeed;
            }
        }

        public Vector3 Target
        {
            get
            {
                return _wayPassed ? transform.position : _target;
            }
        }

        public void Initialize()
        {
            _transform = transform;
            _target = _transform.position;
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

            if (Mathf.Abs(_angelBetweenDirection) < _minAngleToMove)
            {
                UpdateSpeed();
            }
            else
            {
                _currentSpeed = Mathf.Clamp(_currentSpeed - (_acceleration * Time.deltaTime), 0f, _maxSpeed);
            }
        }

        private void OnDisable()
        {
            _currentSpeed = 0;
            _target = _transform.position;
        }

        private void Rotate()
        {
            Vector3 direction = _target - _transform.position;

            _angelBetweenDirection = Vector2.Angle(direction, _transform.right) - Mathf.Abs(_offset);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            float rotateAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, angle + _offset, _rotationSpeed * Time.deltaTime);

            _transform.eulerAngles = new Vector3(0f, 0f, rotateAngle);
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