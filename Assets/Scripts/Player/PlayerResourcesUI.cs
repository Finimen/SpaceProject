using UnityEngine;
using TMPro;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerResources))]
    public class PlayerResourcesUI : MonoBehaviour, IInitializable
    {
        [SerializeField] private TMP_Text _defaultOre;
        [SerializeField] private TMP_Text _redOre;
        [SerializeField] private TMP_Text _greenOre;

        private PlayerResources _resources;

        void IInitializable.Initialize()
        {
            _resources = GetComponent<PlayerResources>();

            _resources.OnOreChanged += UpdateOre;

            UpdateAllResources();
        }

        private void OnDisable()
        {
            _resources.OnOreChanged -= UpdateOre;
        }

        private void UpdateAllResources()
        {
            _defaultOre.text = $"Default: {_resources.DefaultOre}";
            _redOre.text = $"Red: {_resources.RedOre}";
            _greenOre.text = $"Green: {_resources.GreenOre}";
        }

        private void UpdateOre(OreType type, int amount)
        {
            switch (type)
            {
                case OreType.Default:
                    _defaultOre.text = $"Default: {amount}";
                    break;
                case OreType.Red:
                    _redOre.text = $"Red: {amount}";
                    break;
                case OreType.Green:
                    _greenOre.text = $"Green: {amount}";
                    break;
            }
        }
    }
}