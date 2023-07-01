using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Settings
{
    public class QualitySettings : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;

        private void OnEnable()
        {
            InitializeDropdown();

            _dropdown.onValueChanged.AddListener(UpdateQuality);
        }

        private void InitializeDropdown()
        {
            _dropdown.ClearOptions();
            
            var options = new List<TMP_Dropdown.OptionData>()
            {
                new TMP_Dropdown.OptionData("Ultra"),
                new TMP_Dropdown.OptionData("Hight"),
                new TMP_Dropdown.OptionData("Medium"),
                new TMP_Dropdown.OptionData("Low"),
            };

            _dropdown.AddOptions(options);

            //0 - Ultra : FullPC;
            //1 - Hight : FullMobile;
            //2 - Medium : PartMobile;
            //3 - Low : Vignitage & Simple bloom;
        }

        private void UpdateQuality(int level)
        {

        }
    }
}