using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    [RequireComponent(typeof(Bullet))]
    internal class BulletAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;

        private void OnEnable()
        {
            GetComponent<Bullet>().OnEnemyHit += PlayAudio;
        }

        private void PlayAudio()
        {
            Instantiate(_audio, transform.position, Quaternion.identity).Play();
        }
    }
}