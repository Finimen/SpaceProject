using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class MouseHoverText : MouseHover
    {
        [SerializeField] private TMP_Text _selectable;

        protected override void OnMouseEntered()
        {
            _selectable.DOFade(1, _duration).SetEase(_ease);
        }

        protected override void OnMouseExited()
        {
            _selectable.DOFade(0, _duration).SetEase(_ease);
        }
    }
}