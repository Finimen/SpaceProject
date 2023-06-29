using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
    [RequireComponent(typeof(WeaponTrigger))]
    public class WeaponTriggerAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;

        private void OnEnable()
        {
            _audio = Instantiate(_audio, transform.position, Quaternion.identity, transform);

            GetComponent<WeaponTrigger>().OnEnemyDetected += PlayAudio;
        }

        private void PlayAudio()
        {
            _audio.Play();
        }
    }
}