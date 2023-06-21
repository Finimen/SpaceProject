using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        public virtual bool CanShoot { get; protected set; } = true;
        
        public List<Collider2D> _ignoreColliders;

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