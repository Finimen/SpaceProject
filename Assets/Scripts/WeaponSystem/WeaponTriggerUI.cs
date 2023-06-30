using Assets.Scripts.WeaponInstallationSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.WeaponSystem
{
    public class WeaponTriggerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform _attackRadius;
        [SerializeField] private Transform _selectable;

        [SerializeField] protected Ease _ease = Ease.InQuad;

        [SerializeField] protected float _duration = .25f;

        private WeaponInstallationPoint _installationPoint;

        private WeaponTrigger weapon;

        private Vector3 _selectableScale;

        private float _scaleFactor = .314f;

        private void OnEnable()
        {
            _installationPoint = GetComponent<WeaponInstallationPoint>();

            _selectableScale = _selectable.localScale;

            SetActiveUI(false);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (_installationPoint.Current != null)
            {
                weapon = _installationPoint.Current.GetComponent<WeaponTrigger>();

                _attackRadius.transform.localScale = new Vector3(weapon.Radius / _scaleFactor, weapon.Radius / _scaleFactor, 1);

                SetActiveUI(true);
            }
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            SetActiveUI(false);
        }

        private void SetActiveUI(bool active)
        {
            if (active)
            {
                _attackRadius.DOScale(new Vector3(weapon.Radius / _scaleFactor, weapon.Radius / _scaleFactor, 1), _duration).SetEase(_ease);
                _selectable.DOScale(_selectableScale, _duration).SetEase(_ease);
            }
            else
            {
                _attackRadius.DOScale(Vector3.zero, _duration).SetEase(_ease);
                _selectable.DOScale(Vector3.zero, _duration).SetEase(_ease);
            }
        }
    }
}