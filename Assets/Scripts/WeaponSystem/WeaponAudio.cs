using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
    [RequireComponent(typeof(BaseWeapon))]
    internal class WeaponAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;

        private void OnEnable()
        {
            GetComponent<BaseWeapon>().OnFired += PlaySound;

            _audio = Instantiate(_audio, transform.position, Quaternion.identity, transform);
        }

        private void PlaySound()
        {
            if(!_audio.isPlaying)
            {
                _audio.Play();
            }
        }
    }
}