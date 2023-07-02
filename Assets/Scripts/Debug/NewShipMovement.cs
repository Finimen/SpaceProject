using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Debug
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class NewShipMovement : MonoBehaviour
    {
        [SerializeField] private float _currentSpeed;
        [SerializeField] private float _maxSpeed;

        [SerializeField] private float _duration;
        [SerializeField] private float _acceleration;

        private Rigidbody2D _rigidbody;

        private Vector2 _inputDirection;
        private float _rotation;

        public void SetMovementDirection(Vector2 direction)
        {
            _inputDirection = direction;
        }

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _currentSpeed = _rigidbody.velocity.sqrMagnitude;

            if (_rigidbody.velocity.sqrMagnitude < _maxSpeed)
            {
                _rigidbody.AddForce(transform.up * _inputDirection.y * _acceleration * Time.fixedDeltaTime * (_inputDirection.y > 0? 1: .2f));
            }

            _rotation -= _inputDirection.x;
            _rigidbody.DORotate(_rotation, _duration);
        }
    }
}