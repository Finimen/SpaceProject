using Assets.Scripts.PoolSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        public abstract event Action OnFired;
        public virtual bool CanShoot { get; protected set; } = true;
        
        protected Collider2D[] _ignoreColliders;

        protected ObjectPool _pool;

        public void Initialize(Collider2D[] ignoreColliders)
        {
            _ignoreColliders = ignoreColliders;

            _pool = FindObjectOfType<ObjectPool>(true);
        }

        public void Shoot()
        {
            if (!CanShoot)
            {
                throw new System.InvalidOperationException();
            }

            ShootInternal();
        }

        protected abstract void ShootInternal();
    }
}