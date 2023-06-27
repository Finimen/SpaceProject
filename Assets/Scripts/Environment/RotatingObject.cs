using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class RotatingObject : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotateVector;

        private void Update()
        {
            transform.Rotate(_rotateVector * Time.deltaTime);
        }
    }
}