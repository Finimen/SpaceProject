using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    public class PlayerMovement : ShipMovement
    {
        [SerializeField] private float _maxSpeed = 5;

        [SerializeField] private float _rotationTime = 1.75f;
        [SerializeField] private float _acceleration = 50;

        private Rigidbody2D _rigidbody;

        private Vector2 _inputDirection;
        
        private float _rotation;
        private float _currentSpeed;

        public override float Speed => _currentSpeed;

        public void SetMovementDirection(Vector2 direction)
        {
            _inputDirection = direction;
        }

        public void UpdateRotation()
        {
            _rotation = transform.eulerAngles.z;
        }

        protected override void InitializeInternal()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            UpdateRotation();
        }

        private void FixedUpdate()
        {
            _currentSpeed = _rigidbody.velocity.sqrMagnitude;

            if (_rigidbody.velocity.sqrMagnitude < _maxSpeed)
            {
                _rigidbody.AddForce(transform.up * _inputDirection.y * _acceleration * Time.fixedDeltaTime * (_inputDirection.y > 0 ? 1 : .2f));
            }

            _rotation -= _inputDirection.x;
            _rigidbody.DORotate(_rotation, _rotationTime);
        }
    }
}