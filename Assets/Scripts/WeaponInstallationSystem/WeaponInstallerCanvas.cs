using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WeaponInstallationSystem
{
    public class WeaponInstallerCanvas : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gCoins;

        [SerializeField] private Button _leavePortButton;

        public TMP_Text GCoins => _gCoins;

        public Button LeavePortButton => _leavePortButton;
    }
}