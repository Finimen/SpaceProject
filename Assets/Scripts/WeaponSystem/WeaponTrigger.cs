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
            }
            else
            {
                Rotate();
            }

            if (_currentWeapon.CanShoot)
            {
                _currentWeapon.Shoot();
            }
        }

        private void Rotate()
        {
            Vector3 direction = _currentEnemy.transform.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0f, 0f, angle), _rotateSpeed * Time.deltaTime);
        }

        private void FindEnemy()
        {
            foreach (var enemy in _enemies)
            {
                if (Vector2.Distance(enemy.transform.position, transform.position) < _radius)
                {
                    _currentEnemy = enemy;
                }
            }
        }
    }
}