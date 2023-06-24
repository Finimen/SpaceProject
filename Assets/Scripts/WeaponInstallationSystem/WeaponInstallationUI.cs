using UnityEngine;

namespace Assets.Scripts.WeaponInstallationSystem
{
    public class WeaponInstallationUI : MonoBehaviour
    {
        [SerializeField] private WeaponUI _template;

        [SerializeField] private Transform _parent;

        private WeaponUI[] _lastUI;

        public void ShowUI(WeaponInstallationPoint weaponPoint)
        {
            ClearUI();

            _lastUI = new WeaponUI[weaponPoint.AvailableWeapons.Length];

            for(var i = 0; i < weaponPoint.AvailableWeapons.Length; i++)
            {
                var current = weaponPoint.AvailableWeapons[i];
                var ui = Instantiate(_template, _parent);
                ui.Render(current);
                ui.OnClicked += () => weaponPoint.SpawnNewWeapon(current.Prefab);

                _lastUI[i] = ui;
            }
        }

        public void ClearUI()
        {
            if (_lastUI != null)
            {
                foreach (var lastPart in _lastUI)
                {
                    Destroy(lastPart.gameObject);
                }
            }
        }
    }
}