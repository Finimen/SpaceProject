using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class ObjectChaser : MonoBehaviour
    {
        private Transform _target;
        private Transform _transform;

        private Vector3 _offset;

        public void Initialize(Transform target, bool zeroOffset)
        {
            _transform = transform;

            _target = target;
            _offset = zeroOffset? Vector3.zero : transform.position - target.position;
        }

        private void Update()
        {
            if (_target != null)
            {
                _transform.position = _target.position + _offset;
            }
        }
    }
}