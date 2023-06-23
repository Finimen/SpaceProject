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

        private static float _playerGCoins;

        public static List<Ore> Ores
        {
            get
            {
                return _allOres;
            }
        }

        public static float PlayerGCoins
        {
            get
            {
                return _playerGCoins;
            }
        }

        public static void Initialize(int startCount = 0)
        {
            _allOres = new List<Ore>(startCount);
        }

        public static void AddOre(Ore ore)
        {
            _allOres.Add(ore);
        }

        public static void RemoveOre(Ore ore)
        {
            _allOres.Remove(ore);
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