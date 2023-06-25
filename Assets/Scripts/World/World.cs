using Assets.Scripts.Damageable;
using Assets.Scripts.ResourcesSystem;
using System.Collections.Generic;

namespace Assets.Scripts
{
    /// <summary>
    /// Global date about the world
    /// </summary>
    public static class World
    {
        private static List<Ore> _allOres;
        private static List<DamageableObject> _entities;

        private static float _playerGCoins;

        public static List<Ore> Ores => _allOres;

        public static List<DamageableObject> Entities => _entities;

        public static float PlayerGCoins => _playerGCoins;

        public static void Initialize()
        {
            _allOres = new List<Ore>();
            _entities = new List<DamageableObject>();
        }

        public static void IncreasePlayerCoins(float amount)
        {
            _playerGCoins += amount;
        }

        public static void DecreaseCoins(float amount)
        {
            _playerGCoins -= amount;
        }
    }
}