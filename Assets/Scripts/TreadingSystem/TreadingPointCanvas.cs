using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.TreadingSystem
{
    public class TreadingPointCanvas : MonoBehaviour
    {
        [Serializable]
        public struct Resource
        {
            public Button Sell;
            public Button SellAll;
            public TMP_Text Name;
        }

        [SerializeField] private TMP_Text _gCoins;

        [SerializeField] private Button _leavePortButton;

        [SerializeField] private Resource _default;
        [SerializeField] private Resource _red;
        [SerializeField] private Resource _green;

        public TMP_Text GCoins => _gCoins;

        public Button LeavePortButton => _leavePortButton;

        public Resource Default => _default;
        public Resource Red => _red;
        public Resource Green => _green;
    }
}