using Assets.Scripts.WeaponSystem;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class WeaponRandomizer : MonoBehaviour
    {
        [SerializeField] private WeaponTrigger[] _weapons;

        [SerializeField, Range(0, 1)] private float _spawnChance = 1;

        public void Initialize(Collider2D[] ignoreColliders)
        {
            if(_spawnChance > Random.Range(0f, 1f))
            {
                var weapon = Instantiate(_weapons[Random.Range(0, _weapons.Length)], transform.position, transform.rotation, transform);
                weapon.Current.Initialize(ignoreColliders);
            }
        }
    }
}