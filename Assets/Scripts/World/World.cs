using Assets.Scripts.ResourcesSystem;
using System.Collections.Generic;

namespace Assets.Scripts
{
    /// <summary>
    /// Global date about the world
    /// </summary>
    public static class World
    {
        private static List<Ore> allOres;

        public static List<Ore> Ores
        {
            get
            {
                return allOres;
            }
        }

        public static void Initialize(int startCount = 0)
        {
            allOres = new List<Ore>(startCount);
        }

        public static void AddOre(Ore ore)
        {
            allOres.Add(ore);
        }

        public static void RemoveOre(Ore ore)
        {
            allOres.Remove(ore);
        }
    }
}