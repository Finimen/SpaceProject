using Assets.Scripts.Damageable;
using Assets.Scripts.Environment;
using Assets.Scripts.PoolSystem;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Assets.Scripts.SpaceShip
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    public class ShipDamageable : DamageableObject, IInitializable
    {
        [SerializeField] private float _health = 100;

        [SerializeField] private DamageableLevel[] _levels = new DamageableLevel[0];

        private ObjectPool _pool;
        
        private float _maxHealth = 100;

        private int _currentLevel = -1;

        public override event Action OnDestroyed;
        public override event Action<float> OnHealthChanged;

        public override float Health => _health;
        public override float MaxHealth => _maxHealth;

        public override void GetDamage(float amount)
        {
            _health -= amount;

            if (_health <= 0)
            {
                OnDestroyed?.Invoke();

                Dispose();
            }

            OnHealthChanged?.Invoke(Health);

            UpdateDamageLevels();
        }

        public override void Regenerate(float amount)
        {
            _health = Mathf.Clamp(_health + amount, 0, _maxHealth);

            OnHealthChanged?.Invoke(Health);
        }

        public void Initialize()
        {
            _pool = FindObjectOfType<ObjectPool>();

            _maxHealth = _health;

            World.Entities.Add(this);
        }

        private void UpdateDamageLevels()
        {
            for (int i = 0; i < _levels.Length; i++)
            {
                if (Health < _levels[i].MinHealth && _currentLevel < i)
                {
                    _currentLevel = i;

                    var effect = _pool.Get(_levels[i].EffectsTemplate.gameObject).GetComponent<ObjectChaser>();
                    effect.transform.position = transform.position + new Vector3(
                       Random.Range(-_levels[i].RandomSpawnOffset, _levels[i].RandomSpawnOffset),
                       Random.Range(-_levels[i].RandomSpawnOffset, _levels[i].RandomSpawnOffset));
                    effect.transform.rotation = transform.rotation;
                    effect.Initialize(transform, true);
                }
            }
        }

        private void Dispose()
        {
            Destroy(gameObject);

            World.Entities.Remove(this);

            OnDestroyed?.Invoke();
        }
    }
}