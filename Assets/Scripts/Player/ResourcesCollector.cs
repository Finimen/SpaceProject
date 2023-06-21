using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class ResourcesCollector : MonoBehaviour, IInitializable
    {
        [SerializeField] private float _radius = 5;
        [SerializeField] private float _power = 1;

        private List<Ore> _ores;
        private Ore _currentOre;

        private Transform _transform;

        void IInitializable.Initialize()
        {
            _ores = FindObjectsOfType<Ore>().ToList();

            _transform = transform;
        }

        private void FixedUpdate()
        {
            if (_currentOre == null)
            {
                for(int i = 0; i < _ores.Count; i++)
                {
                    if (_ores[i] == null)
                    {
                        _ores.RemoveAt(i);
                    }
                    else if (Vector3.Distance(_ores[i].transform.position, _transform.position) < _radius)
                    {
                        _currentOre = _ores[i];
                        break;
                    }
                }
             
                return;
            }

            if (Vector3.Distance(_currentOre.transform.position, _transform.position) > _radius)
            {
                _currentOre.StartCollecting();
                _currentOre = null;
            }
            else
            {
                _currentOre.StartCollecting();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}