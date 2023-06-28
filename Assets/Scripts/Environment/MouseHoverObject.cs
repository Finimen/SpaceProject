using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class MouseHoverObject : MouseHover
    {
        [SerializeField] private Transform _selectable;

        private Vector3 _startScale;

        protected override void OnMouseEntered()
        {
            _selectable.DOScale(_startScale, _duration).SetEase(_ease);
        }

        protected override void OnMouseExited()
        {
            _selectable.DOScale(Vector3.zero, _duration).SetEase(_ease);
        }

        private void OnEnable()
        {
            _startScale = transform.localScale;

            _selectable.DOScale(Vector3.zero, _duration).SetEase(_ease);
        }
    }
}