using Assets.Scripts.Damageable;
using System;
using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
    public class WeaponTrigger : MonoBehaviour
    {
        public event Action OnEnemyDetected;

        [SerializeField] private int _id;

        [SerializeField] private float _radius = 5;
        [SerializeField] private float _rotateSpeed = 25;

        [SerializeField] private BaseWeapon _currentWeapon;
        
        private DamageableObject _currentEnemy;

        private float _offset = -90;
        
        private float _angelBetweenDirection;

        public BaseWeapon Current => _currentWeapon;

        public float Radius => _radius;

        private void Update()
        {
            FindEnemy();

            if (_currentEnemy == null)
            {
                return;
            }
            else
            {
                Rotate();
            }

            Vector3 direction = _currentEnemy.transform.position - transform.position;

            _angelBetweenDirection = Vector2.Angle(direction, transform.right) - Mathf.Abs(_offset);

            if (Vector2.Distance(transform.position, _currentEnemy.transform.position) < _radius
                && _currentWeapon.CanShoot && Mathf.Abs(_angelBetweenDirection) < .5f)
            {
                _currentWeapon.Shoot();
            }
        }

        private void Rotate()
        {
            Vector3 direction = _currentEnemy.transform.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            float rotateAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, angle - 90,
                _rotateSpeed * Time.deltaTime);

            transform.eulerAngles = new Vector3(0f, 0f, rotateAngle);
        }

        private void FindEnemy()
        {
            DamageableObject nearestEnemy = null;

            foreach (var entity in World.Ships)
            {
                if (nearestEnemy == null && entity.DamageDealer.Id != _id || entity.DamageDealer.Id != _id && entity != null && nearestEnemy != null 
                    && Vector3.Distance(entity.transform.position, transform.position) < Vector3.Distance(nearestEnemy.transform.position, transform.position))
                {
                    nearestEnemy = entity.DamageDealer;
                }
            }

            if (nearestEnemy != null && Vector3.Distance(nearestEnemy.transform.position, transform.position) < _radius && nearestEnemy != _currentEnemy)
            {
                _currentEnemy = nearestEnemy;

                OnEnemyDetected?.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}