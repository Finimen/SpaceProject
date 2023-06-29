using UnityEngine;

namespace Assets.Scripts.WeaponInstallationSystem
{
    [RequireComponent(typeof(WeaponInstallationPoint))]
    internal class WeaponInstallationPointAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;

        private void OnEnable()
        {
            _audio = Instantiate(_audio, transform.position, Quaternion.identity, transform);

            GetComponent<WeaponInstallationPoint>().OnWeaponEquipped += PlayAudio;
        }

        private void PlayAudio()
        {
            _audio.Play();
        }
    }
}