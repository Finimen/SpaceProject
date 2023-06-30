using Assets.Scripts.ResourcesSystem;
using Assets.Scripts.TreadingSystem;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.TrainingSystem
{
    public class ResourceModule : TrainingModule
    {
        [SerializeField] private ResourcesHandler _player;

        [SerializeField] private TMP_Text _progress;

        [SerializeField] private int _neededCount = 5;

        [SerializeField] private GameObject[] _ores;

        private int _currentCount;

        public override void Complete()
        {
            FindObjectOfType<Training>().NextState();

            _player.OnOreChanged -= IncreaseCount;

            foreach (var ore in _ores)
            {
                ore?.SetActive(false);
            }
        }

        public override void Enable()
        {
            _player.gameObject.AddComponent<ResourcesHandlerUI>().Initialize();
            _player.GetComponent<ResourcesCollector>().enabled = true;
            _player.GetComponent<ResourcesCollector>().Initialize();
            _player.GetComponent<ResourcesCollectorUI>().enabled = true;

            _player.OnOreChanged += IncreaseCount;

            _progress.gameObject.SetActive(true);

            foreach (var ore in _ores)
            {
                ore.SetActive(true);
            }
        }

        private void IncreaseCount(OreType ore, int count)
        {
            _currentCount++;

            _progress.text = $"Собрано: {_currentCount} / {_neededCount}";

            if (_currentCount >= _neededCount)
            {
                Complete();
                _progress.gameObject.SetActive(false);
            }
        }
    }
}