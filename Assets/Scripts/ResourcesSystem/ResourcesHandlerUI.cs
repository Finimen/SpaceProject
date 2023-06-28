using UnityEngine;
using TMPro;
using Assets.Scripts.SpaceShip;

namespace Assets.Scripts.ResourcesSystem
{
    [RequireComponent(typeof(ResourcesHandler), typeof(Ship))]
    public class ResourcesHandlerUI : MonoBehaviour, IInitializable
    {
        [SerializeField] private TMP_Text _defaultOre;
        [SerializeField] private TMP_Text _redOre;
        [SerializeField] private TMP_Text _greenOre;

        private ResourcesHandler _resources;

        private bool _active;

        public void Initialize()
        {
            GetComponent<Ship>().OnSelectedForMoving += () => SetActiveUI(true);
            GetComponent<Ship>().OnDeselected += () => SetActiveUI(false);

            _resources = GetComponent<ResourcesHandler>();

            _resources.OnOreChanged += UpdateOre;

            SetActiveUI(false);
        }

        private void UpdateAllResources()
        {
            _defaultOre.text = $"Default: {_resources.DefaultOre}";
            _redOre.text = $"Red: {_resources.RedOre}";
            _greenOre.text = $"Green: {_resources.GreenOre}";
        }

        private void UpdateOre(OreType type, int amount)
        {
            if (!_active)
            {
                return;
            }

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

        private void SetActiveUI(bool active)
        {
            if(_active != active)
            {
                UpdateAllResources();

                _active = active;

                _defaultOre.gameObject.SetActive(active);
                _redOre.gameObject.SetActive(active);
                _greenOre.gameObject.SetActive(active);
            }
        }
    }
}