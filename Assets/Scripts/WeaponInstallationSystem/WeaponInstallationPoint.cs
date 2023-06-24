using Assets.Scripts.WeaponSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.WeaponInstallationSystem
{
    public class WeaponInstallationPoint : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private BaseWeapon _current;

        [SerializeField, Space(25)] private WeaponData[] _availableWeapons;

        private Collider2D[] _ignoreCollidersForWeapons;
        private WeaponInstallationUI _ui;

        private bool _enabled;

        public WeaponData[] AvailableWeapons => _availableWeapons;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if(_enabled)
            {
                _ui.ShowUI(this);
            }
        }

        public void Initialize(Collider2D[] ignoreCollidersForWeapons)
        {
            _ui = FindObjectOfType<WeaponInstallationUI>(true);

            _ignoreCollidersForWeapons = ignoreCollidersForWeapons;

            _current?.Initialize(_ignoreCollidersForWeapons);
        }

        public void Enable(bool enabled)
        {
            _enabled = enabled;
        }

        public void SpawnNewWeapon(BaseWeapon template)
        {
            if(_current != null)
            {
                Destroy(_current.gameObject);
            }

            _current = Instantiate(template, transform.position, transform.rotation, transform);
            _current.Initialize(_ignoreCollidersForWeapons);
        }
    }
}