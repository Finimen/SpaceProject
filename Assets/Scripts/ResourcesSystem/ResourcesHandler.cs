using System;
using UnityEngine;

namespace Assets.Scripts.ResourcesSystem
{
    /// <summary>
    /// Ores
    /// </summary>
    public class ResourcesHandler : MonoBehaviour
    {
        [SerializeField] private int _defaultOre;
        [SerializeField] private int _redOre;
        [SerializeField] private int _greenOre;

        public event Action<OreType, int> OnOreChanged;

        public int DefaultOre => _defaultOre;
        public int RedOre => _redOre;
        public int GreenOre => _greenOre;

        public void IncreaseOre(int amount, OreType oreType)
        {
            switch (oreType)
            {
                case OreType.Default: 
                    _defaultOre += amount;
                    OnOreChanged?.Invoke(oreType, _defaultOre);
                    break;

                case OreType.Red:
                    _redOre += amount; 
                    OnOreChanged?.Invoke(oreType, _redOre);
                    break;

                case OreType.Green:
                    _greenOre += amount;
                    OnOreChanged?.Invoke(oreType, _greenOre);
                    break;
            }
        }

        public void DecreaseOre(int amount, OreType oreType)
        {
            IncreaseOre(-amount, oreType);
        }
    }
}