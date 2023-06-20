
using System;
using UnityEngine;

namespace Assets.Scripts.Damageable
{
    public abstract class DamageableObject : MonoBehaviour, IDamageable
    {
        public abstract float Health { get; }

        public abstract float MaxHealth { get; }

        public abstract event Action OnDestroyed;

        public abstract void GetDamage(float amount);
    }
}