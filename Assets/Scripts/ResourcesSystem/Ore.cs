using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ResourcesSystem
{
    public class Ore : MonoBehaviour
    {
        [SerializeField] private int _amount = 1;

        [SerializeField] private float _collectingTime = 1;

        [SerializeField] private OreType _type;

        public event Action OnOreCollected;

        private Coroutine _collecting;

        public OreType Type => _type;

        public int Amount => _amount;

        private void OnEnable()
        {
            World.Ores.Add(this);
        }

        public void StartCollecting(float collectingPower = 1)
        {
            if(collectingPower > 0)
            {
                _collecting = StartCoroutine(Collecting(collectingPower));
            }
            else
            {
                Collect();
            }
        }

        public void StopCollecting()
        {
            if(_collecting != null)
            {
                StopCoroutine(_collecting);
            }
        }

        private IEnumerator Collecting(float collectingPower)
        {
            yield return new WaitForSeconds(_collectingTime / collectingPower);

            Collect();
        }

        private void Collect()
        {
            OnOreCollected?.Invoke();

            World.Ores.Add(this);

            if(Application.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}