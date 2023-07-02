using UnityEngine;

namespace Assets.Scripts.CameraSystem
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [SerializeField] private float _speed;

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, _speed * Time.deltaTime);
        }
    }
}