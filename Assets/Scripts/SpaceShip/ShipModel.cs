using System;
using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    [Serializable]
    internal class ShipModel
    {
        [SerializeField] private float _health = 100;
        
        [SerializeField] private float _maxHealth = 100;

        public event Action OnDestroyed;

        public float Health => _health;

        public float MaxHealth => _maxHealth;

        public void GetDamage(float amount)
        {
            _health -= amount;

            if(_health < 0)
            {
                OnDestroyed?.Invoke();
            }
        }

        public void Initialize()
        {
            _maxHealth = _health;
        }
    }
}