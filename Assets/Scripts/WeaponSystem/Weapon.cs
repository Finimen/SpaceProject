using Assets.Scripts.Projectiles;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
    internal class Weapon : BaseWeapon
    {
        [SerializeField] private Bullet _bulletTemplate;

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _delayForNextShoot = .1f;

        private Coroutine _reloading;

        protected override void ShootInternal()
        {
            var bullet = Instantiate(_bulletTemplate, _spawnPoint.position, _spawnPoint.rotation);
            bullet.Initialize();
            bullet.SetIgnoreColliders(_ignoreColliders.ToArray());

            if(_reloading != null)
            {
                StopCoroutine(_reloading);
            }

            _reloading = StartCoroutine(Reload());
        }

        private IEnumerator Reload()
        {
            CanShoot = false;

            yield return new WaitForSeconds(_delayForNextShoot);

            CanShoot = true;
        }
    }
}