using UnityEngine;
using Assets.Scripts.SpaceShip;

namespace Assets.Scripts.ResourcesSystem
{
    [RequireComponent(typeof(ResourcesHandler), typeof(Ship))]
    public class ResourcesHandlerUI : MonoBehaviour, IInitializable
    {
        private ResourcesCanvas _canvas;
        private ResourcesHandler _resources;

        private bool _active;

        public void Initialize()
        {
            _canvas = FindObjectOfType<ResourcesCanvas>(true);

            GetComponent<Ship>().OnSelectedForMoving += () => SetActiveUI(true);
            GetComponent<Ship>().OnDeselected += () => SetActiveUI(false);

            _resources = GetComponent<ResourcesHandler>();

            _resources.OnOreChanged += UpdateOre;

            SetActiveUI(false);
        }

        private void UpdateAllResources()
        {
            _canvas.DefaultOre.text = $"Default: {_resources.DefaultOre}";
            _canvas.RedOre.text = $"Red: {_resources.RedOre}";
            _canvas.GreenOre.text = $"Green: {_resources.GreenOre}";
            _canvas.GCoins.text = $"GCoins: {World.PlayerGCoins}";
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
                    _canvas.DefaultOre.text = $"Default: {amount}";
                    break;
                case OreType.Red:
                    _canvas.RedOre.text = $"Red: {amount}";
                    break;
                case OreType.Green:
                    _canvas.GreenOre.text = $"Green: {amount}";
                    break;
            }
        }

        private void SetActiveUI(bool active)
        {
            if(_active != active)
            {
                UpdateAllResources();

                _active = active;

                _canvas.gameObject.SetActive(active);
            }
        }
    }
}