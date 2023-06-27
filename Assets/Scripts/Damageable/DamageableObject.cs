using System;
using UnityEngine;

namespace Assets.Scripts.Damageable
{
    public abstract class DamageableObject : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _id;

        public int Id => _id;

        public abstract float Health { get; }

        public abstract float MaxHealth { get; }

        public abstract event Action OnDestroyed;
        public abstract event Action<float> OnHealthChanged;

        public abstract void GetDamage(float amount);

        public abstract void Regenerate(float amount);
    }
}