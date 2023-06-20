using Assets.Scripts.Damageable;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    [RequireComponent(typeof(Collider2D))]
    internal class Bullet : Projectile, IInitializable
    {
        [Space(25)]
        [SerializeField] private float _damage;

        private Transform _transform;

        public void Initialize()
        {
            _transform = transform;
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
            if(other.GetComponent<IDamageable>() != null)
            {
                other.GetComponent<IDamageable>().GetDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}