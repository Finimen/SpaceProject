using Assets.Scripts.Damageable;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
    internal class WeaponTrigger : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _rotateSpeed;

        [SerializeField] private BaseWeapon _currentWeapon;

        [SerializeField] private List<DamageableObject> _enemies;

        private DamageableObject _currentEnemy;

        private void Update()
        {
            if (_currentEnemy == null)
            {
                FindEnemy();
                return;
            }
            else
            {
                Rotate();
            }

            if (Vector2.Distance(transform.position, _currentEnemy.transform.position) < _radius
                && _currentWeapon.CanShoot)
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
            foreach (var enemy in _enemies)
            {
                if (enemy != null && Vector2.Distance(enemy.transform.position, transform.position) < _radius)
                {
                    _currentEnemy = enemy;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}