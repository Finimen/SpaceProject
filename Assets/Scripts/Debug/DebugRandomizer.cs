using UnityEngine;

namespace Assets.Scripts.Debug
{
    [ExecuteInEditMode]
    public class DebugRandomizer : MonoBehaviour
    {
        [SerializeField] private int _minInclusive;
        [SerializeField] private int _maxInclusive;

        [SerializeField] private int _result;

        [SerializeField] private bool _enabled;

        private void Update()
        {
            if (_enabled)
            {
                _enabled = false;

                _result = Random.Range(_minInclusive, _maxInclusive);
                UnityEngine.Debug.Log(_result);
            }
        }
    }
}