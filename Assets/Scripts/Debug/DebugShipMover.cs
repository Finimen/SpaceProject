using UnityEngine;

namespace Assets.Scripts.Debug
{
    public class DebugShipMover : MonoBehaviour
    {
        [SerializeField] private Vector3 _worldMouse;

        [SerializeField] private float _movement;
        [SerializeField] private float _rotation;

        private void Update()
        {
            UpdateTarget();
            MoveToTarget();
        }

        private void UpdateTarget()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;

                return;
                _worldMouse = Camera.main.ScreenToWorldPoint(mousePosition);
                _worldMouse.z = 0f;
            }
        }

        private void MoveToTarget()
        {
            Vector3 direction = _worldMouse - transform.position;

            // ¬ычисл€ем угол поворота в радианах от направлени€
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0f, 0f, angle), _rotation * Time.deltaTime);
        }
    }
}