using Assets.Scripts.WeaponInstallationSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.WeaponSystem
{
    public class WeaponTriggerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject _attackRadius;
        [SerializeField] private GameObject _selectable;

        private WeaponInstallationPoint _installationPoint;

        private float _scaleFactor = .314f;

        private void OnEnable()
        {
            _installationPoint = GetComponent<WeaponInstallationPoint>();

            SetActiveUI(false);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (_installationPoint.Current != null)
            {
               var weapon = _installationPoint.Current.GetComponent<WeaponTrigger>();

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
            _attackRadius.SetActive(active);
            _selectable.SetActive(active);
        }
    }
}