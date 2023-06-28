using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Environment
{
    public abstract class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Ease _ease = Ease.InQuad;

        [SerializeField] protected float _duration = .25f;

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            OnMouseEntered();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            OnMouseExited();
        }

        protected abstract void OnMouseEntered();
        protected abstract void OnMouseExited();
    }
}