using Assets.Scripts.WeaponSystem;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using static Codice.CM.Common.CmCallContext;

namespace Assets.Scripts.WeaponInstallationSystem
{
    public class WeaponInstallationPoint : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnWeaponEquipped;

        [SerializeField] private BaseWeapon _current;

        [SerializeField, Space(25)] private WeaponData[] _availableWeapons;

        private Collider2D[] _ignoreCollidersForWeapons;
        
        private WeaponInstallationUI _ui;

        private SpriteRenderer _renderer;

        private bool _enabled;

        public BaseWeapon Current => _current;
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

            _renderer = GetComponent<SpriteRenderer>();

            _ignoreCollidersForWeapons = ignoreCollidersForWeapons;

            _current?.Initialize(_ignoreCollidersForWeapons);

            Enable(false);
        }

        public void Enable(bool enabled)
        {
            _enabled = enabled;

            _renderer.enabled = enabled;
        }

        public void SpawnNewWeapon(BaseWeapon template)
        {
            if(_current != null)
            {
                Destroy(_current.gameObject);
            }

            _current = Instantiate(template, transform.position, transform.rotation, transform);
            _current.name = _current.name.Replace("(Clone)", "");
            _current.Initialize(_ignoreCollidersForWeapons);

            OnWeaponEquipped?.Invoke();
        }

        private void Update()
        {
            if(_enabled && Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null)
            {
                _ui.HideUI();
            }
        }
    }
}