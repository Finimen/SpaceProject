using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ResourcesSystem
{
    public class ResourcesCollectorUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private LineRenderer _line;

        [SerializeField] private Transform _radius;

        [SerializeField] protected Ease _ease = Ease.InQuad;

        [SerializeField] protected float _duration = .25f;

        private ResourcesCollector _collector;
        
        private float _scaleFactor = 3.14f;

        private void OnEnable()
        {
            _collector = GetComponent<ResourcesCollector>();

            _line = Instantiate(_line, Vector3.zero, Quaternion.identity);
            _line.positionCount = 0;

            _radius.DOScale(Vector3.zero, _duration).SetEase(_ease);
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
            _radius.DOScale(new Vector3(_collector.Radius * _scaleFactor, _collector.Radius * _scaleFactor, 1), _duration).SetEase(_ease);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _radius.DOScale(Vector3.zero, _duration).SetEase(_ease);
        }
    }
}