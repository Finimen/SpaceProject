using TMPro;
using UnityEngine;

namespace Assets.Scripts.ResourcesSystem
{
    public class ResourcesCanvas : MonoBehaviour
    {
        [SerializeField] private TMP_Text _defaultOre;
        [SerializeField] private TMP_Text _redOre;
        [SerializeField] private TMP_Text _greenOre;
        [SerializeField] private TMP_Text _gCoins;

        public TMP_Text DefaultOre => _defaultOre;
        public TMP_Text RedOre => _redOre;
        public TMP_Text GreenOre => _greenOre;
        public TMP_Text GCoins => _gCoins;
    }
}