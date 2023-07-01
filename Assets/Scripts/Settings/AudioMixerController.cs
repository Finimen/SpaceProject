using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.UI
{
    public class AudioMixerController : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup group;

        [SerializeField] private string label;

        public void UpdateMixer(float value)
        {
            group.audioMixer.SetFloat(label, Mathf.Lerp(-80, 0, value));
        }
    }
}