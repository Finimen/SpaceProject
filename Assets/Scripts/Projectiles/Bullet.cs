using Assets.Scripts.Damageable;
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

        public void Initialize()
        {
            _transform = transform;

            Destroy(gameObject, _lifeTime);
        }

        public void SetIgnoreColliders(Collider2D[] ignoreColliders)
        {
            _ignoreColliders = ignoreColliders;
        }

        private void Update()
        {
            Move();
        }

        protected override void Move()
        {
            _transform.Translate(_movingVector * _speed);
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
                Destroy(gameObject);
            }
        }
    }
}