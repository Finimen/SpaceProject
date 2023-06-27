using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.TreadingSystem
{
    internal class ShipPurchasePointCanvas : MonoBehaviour
    {
        [SerializeField] private Button _byShipButton;
        [SerializeField] private Button _leavePortButton;

        [SerializeField] private TMP_Text _gCoins;
        [SerializeField] private TMP_Text _price;

        public Button ByShipButton => _byShipButton;
        public Button LeavePortButton => _leavePortButton;

        public TMP_Text GCoins => _gCoins;
        public TMP_Text Price => _price;
    }
}