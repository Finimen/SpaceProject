using Assets.Scripts.PoolSystem;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticlesDestroyer : MonoBehaviour
    {
        private ParticleSystem _particles;

        private ObjectPool _pool;

        private void OnEnable()
        {
            _particles = GetComponent<ParticleSystem>();
            _pool = FindObjectOfType<ObjectPool>();
        }

        private void FixedUpdate()
        {
            if (!_particles.isPlaying)
            {
                _pool.Add(gameObject);
            }
        }
    }
}