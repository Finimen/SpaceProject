using UnityEngine;

namespace Assets.Scripts.ResourcesSystem
{
    public class ResourcesCollectorUI : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;

        private ResourcesCollector _collector;

        private void OnEnable()
        {
            _collector = GetComponent<ResourcesCollector>();

            _line = Instantiate(_line, Vector3.zero, transform.rotation);
            _line.positionCount = 0;
        }

        private void FixedUpdate()
        {
            if(_collector.Current == null)
            {
                _line.positionCount = 0;

                return;
            }
            else
            {
                _line.positionCount = 2;
            }

            var start = transform.position;
            var end = _collector.Current.transform.position;

            _line.SetPosition(0, start);
            _line.SetPosition(1, end);
        }
    }
}