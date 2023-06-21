using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Ore : MonoBehaviour, IInitializable
    {
        [SerializeField] private int _amount = 1;

        [SerializeField] private float _collectingTime = 1;

        [SerializeField] private OreType _type;

        private PlayerResources _resources;

        private Coroutine _collecting;

        void IInitializable.Initialize()
        {
            _resources = FindObjectOfType<PlayerResources>();
        }

        public void StartCollecting(float collectingPower = 1)
        {
            _collecting = StartCoroutine(Collecting(collectingPower));
        }

        public void StopCollecting()
        {
            StopCoroutine(_collecting);
        }

        private IEnumerator Collecting(float collectingPower)
        {
            yield return new WaitForSeconds(_collectingTime / collectingPower);

            Collect();
        }

        private void Collect()
        {
            _resources.IncreaseOre(_amount, _type);

            Destroy(gameObject);
        }
    }
}