using Assets.Scripts.Damageable;
using Assets.Scripts.PoolSystem;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    [RequireComponent(typeof(Collider2D))]
    internal class Bullet : Projectile, IInitializable
    {
        [Space(25)]
        [SerializeField] private float _damage;
        [SerializeField] private float _lifeTime = 5;

        private Transform _transform;

        private Collider2D[] _ignoreColliders;

        private ObjectPool _pool;

        public void Initialize()
        {
            _transform = transform;

            _pool = FindObjectOfType<ObjectPool>();

            StartCoroutine(AddToPool());
        }

        public void SetIgnoreColliders(Collider2D[] ignoreColliders)
        {
            _ignoreColliders = ignoreColliders;
        }

        protected override void Move()
        {
            _transform.Translate(_movingVector * _speed);
        }

        private IEnumerator AddToPool()
        {
            yield return new WaitForSeconds(_lifeTime);

            _pool.Add(gameObject);
        }

        private void Update()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_ignoreColliders != null)
            {
                foreach (var ignore in _ignoreColliders)
                {
                    if (ignore == other)
                    {
                        return;
                    }
                }
            }
            
            if(other.GetComponent<IDamageable>() != null)
            {
                other.GetComponent<IDamageable>().GetDamage(_damage);
                _pool.Add(gameObject);
            }
        }
    }
}