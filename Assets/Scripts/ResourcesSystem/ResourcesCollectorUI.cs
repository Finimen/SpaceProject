using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ResourcesSystem
{
    public class ResourcesCollectorUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private LineRenderer _line;

        [SerializeField] private GameObject _radius;

        private ResourcesCollector _collector;
        
        private float _scaleFactor = 3.14f;

        private void OnEnable()
        {
            _collector = GetComponent<ResourcesCollector>();

            _line = Instantiate(_line, Vector3.zero, Quaternion.identity);
            _line.positionCount = 0;

            _radius.SetActive(false);
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

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            _radius.transform.localScale = new Vector3(_collector.Radius * _scaleFactor, _collector.Radius * _scaleFactor, 1);
            _radius.SetActive(true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _radius.SetActive(false);
        }
    }
}