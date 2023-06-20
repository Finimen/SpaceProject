using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    internal abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] protected Vector2 _movingVector = Vector2.up;

        protected abstract void Move();
        protected virtual void Destroy()
        {
            Destroy(gameObject);
        }
    }
}