using Assets.Scripts.PoolSystem;
using Assets.Scripts.Projectiles;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
    internal class Weapon : BaseWeapon, IInitializable
    {
        [SerializeField] private Bullet _bulletTemplate;
        
        [SerializeField] private Transform _spawnPoint;

        [SerializeField] private float _scatter = 1;
        [SerializeField] private float _delayForNextShoot = .1f;

        private Coroutine _reloading;

        private ObjectPool _pool;

        void IInitializable.Initialize()
        {
            _pool = FindObjectOfType<ObjectPool>();
        }

        protected override void ShootInternal()
        {
            var bullet = _pool.Get(_bulletTemplate.gameObject).GetComponent<Bullet>();
            bullet.transform.position = _spawnPoint.position;
            bullet.transform.rotation = _spawnPoint.rotation 
                * Quaternion.Euler(0, 0, Random.Range(-_scatter, _scatter));

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