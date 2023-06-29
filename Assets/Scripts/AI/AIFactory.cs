using Assets.Scripts.SpaceShip;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Assets.Scripts.AI
{
    internal class AIFactory : MonoBehaviour
    {
        [Serializable]
        private struct AI
        {
            public Ship Ship;
            public float Chance;
        }

        [SerializeField] private AI[] _bots;

        [SerializeField] private float maxSumm;
        [SerializeField] private float currentSumm;
        [SerializeField] private int index;

        [ContextMenu("D")]
        private void DEbug()
        {
            var summ = 0f;

            foreach (var bot in _bots)
            {
                summ += bot.Chance;
            }

            maxSumm = Random.Range(0, summ);
            currentSumm = 0f;
            index = 0;

            for (var i = 0; i < _bots.Length; i++)
            {
                currentSumm += _bots[i].Chance;
                if (currentSumm > maxSumm)
                {
                    index = i;
                    break;
                }
            }
        }

        public Ship GetShip(Vector3 position, Quaternion rotation, Transform parent)
        {
            var summ = 0f;

            foreach (var bot in _bots)
            {
                summ += bot.Chance;
            }

            var maxSumm = Random.Range(0, summ);
            var currentSumm = 0f;
            var index = 0;

            for(var i = 0; i < _bots.Length; i++)
            {
                currentSumm += _bots[i].Chance;
                if(currentSumm > maxSumm)
                {
                    index = i;
                    break;
                }
            }

            var ship = Instantiate(_bots[index].Ship, position, rotation, transform);

            return ship;
        }
    }
}