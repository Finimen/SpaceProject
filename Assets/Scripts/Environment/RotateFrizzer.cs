using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class RotateFrizzer : MonoBehaviour
    {
        private Transform _transform;

        private void OnEnable()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.eulerAngles = Vector3.zero;
        }
    }
}