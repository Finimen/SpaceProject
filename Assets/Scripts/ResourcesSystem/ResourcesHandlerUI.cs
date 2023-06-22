using UnityEngine;
using TMPro;

using OreType = Assets.Scripts.ResourcesSystem.OreType;

namespace Assets.Scripts.ResourcesSystem
{
    [RequireComponent(typeof(ResourcesHandler))]
    public class ResourcesHandlerUI : MonoBehaviour, IInitializable
    {
        [SerializeField] private TMP_Text _defaultOre;
        [SerializeField] private TMP_Text _redOre;
        [SerializeField] private TMP_Text _greenOre;

        private ResourcesHandler _resources;

        void IInitializable.Initialize()
        {
            _resources = GetComponent<ResourcesHandler>();

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