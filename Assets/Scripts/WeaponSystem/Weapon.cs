using Assets.Scripts.Projectiles;
using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Assets.Scripts.WeaponSystem
{
    public class Weapon : BaseWeapon
    {
        public override event Action OnFired;

        [SerializeField] private Bullet _bulletTemplate;
        [SerializeField] private GameObject _shootParticles;
        
        [SerializeField] private Transform[] _spawnPoints;

        [SerializeField] private float _scatter = 1;
        [SerializeField] private float _delayForNextShoot = .1f;

        private Coroutine _reloading;

        private int _currentSpawn;

        protected override void ShootInternal()
        {
            var bullet = CreateBullet();

            _currentSpawn++;
            if(_currentSpawn >= _spawnPoints.Length)
            {
                _currentSpawn = 0;
            }

            if (_shootParticles != null)
            {
                var particles = _pool.Get(_shootParticles);
                particles.transform.position = transform.position;
                particles.transform.rotation = transform.rotation;
            }

            OnFired?.Invoke();

            if(_reloading != null)
            {
                StopCoroutine(_reloading);
            }

            _reloading = StartCoroutine(Reload());
        }

        private Bullet CreateBullet()
        {
            var bullet = _pool.Get(_bulletTemplate.gameObject).GetComponent<Bullet>();
            bullet.transform.position = _spawnPoints[_currentSpawn].position;
            bullet.transform.rotation = _spawnPoints[_currentSpawn].rotation
                * Quaternion.Euler(0, 0, Random.Range(-_scatter, _scatter));

            bullet.Initialize();
            bullet.SetIgnoreColliders(_ignoreColliders);

            return bullet;
        }

        private IEnumerator Reload()
        {
            CanShoot = false;

            yield return new WaitForSeconds(_delayForNextShoot);

            CanShoot = true;
        }
    }
}