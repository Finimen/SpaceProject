using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        public virtual bool CanShoot { get; protected set; } = true;
        
        public Collider2D[] _ignoreColliders;

        public void Initialize(Collider2D[] ignoreColliders)
        {
            _ignoreColliders = ignoreColliders;
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