using UnityEngine;

namespace Assets.Scripts.AudioSystem
{
    public class Music : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _clips;

        private AudioSource _source;

        private void OnEnable()
        {
            _source = GetComponent<AudioSource>();

            _source.clip = _clips[Random.Range(0, _clips.Length)];
        }

        private void FixedUpdate()
        {
            if (!_source.isPlaying)
            {
                _source.Play();
            }
        }
    }
}