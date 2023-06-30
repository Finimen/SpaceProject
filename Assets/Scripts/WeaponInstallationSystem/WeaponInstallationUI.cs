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
            HideUI();

            _lastUI = new WeaponUI[weaponPoint.AvailableWeapons.Length];

            for(var i = 0; i < weaponPoint.AvailableWeapons.Length; i++)
            {
                var current = weaponPoint.AvailableWeapons[i];
                var ui = Instantiate(_template, _parent);
                ui.Render(current);

                ui.OnClicked += () =>
                {
                    if(weaponPoint.Current == null || weaponPoint.Current.name != current.Prefab.name)
                    {
                        weaponPoint.SpawnNewWeapon(current.Prefab);
                        World.DecreaseCoins(current.Price);
                    }
                };

                _lastUI[i] = ui;
            }
        }

        public void HideUI()
        {
            if (_lastUI != null)
            {
                foreach (var lastPart in _lastUI)
                {
                    Destroy(lastPart.gameObject);
                }

                _lastUI = null;
            }
        }
    }
}