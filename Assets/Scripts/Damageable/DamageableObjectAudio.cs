using UnityEngine;

namespace Assets.Scripts.Damageable
{
    [RequireComponent(typeof(DamageableObject))]
    public class DamageableObjectAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _damageAudio;

        [SerializeField] private float _pitchRandomness = .1f;

        private float _lastHealth;

        private void OnEnable()
        {
            _damageAudio = Instantiate(_damageAudio, transform.position, Quaternion.identity, transform);

            GetComponent<DamageableObject>().OnHealthChanged += PlayAudio;
            _lastHealth = GetComponent<DamageableObject>().Health;
        }

        private void PlayAudio(float health)
        {
            if(health < _lastHealth && !_damageAudio.isPlaying)
            {
                _damageAudio.pitch += Random.Range(-_pitchRandomness, _pitchRandomness);
                _damageAudio.Play();
            }

            _lastHealth = health;
        }
    }
}