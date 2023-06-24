using Assets.Scripts.WeaponSystem;
using UnityEngine;

namespace Assets.Scripts.WeaponInstallationSystem
{
    [CreateAssetMenu(menuName = "Weapon/Data")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _price;

        [SerializeField] private BaseWeapon _prefab;

        public BaseWeapon Prefab => _prefab;
        public Sprite Icon => _icon;
        public float Price => _price;
    }
}