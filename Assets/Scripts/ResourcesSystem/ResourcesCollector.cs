using System;
using UnityEngine;

namespace Assets.Scripts.ResourcesSystem
{
    public class ResourcesCollector : MonoBehaviour, IInitializable
    {

        [SerializeField] private float _radius = 5;
        [SerializeField] private float _power = 1;

        public event Action<Ore> OnOreStartCollecting;
        public event Action<Ore> OnOreEndCollecting;

        private ResourcesHandler _handler;
        private Transform _transform;

        private Ore _currentOre;

        private bool _startCollecting;

        public Ore Current
        {
            get
            {
                return _currentOre;
            }
        }

        void IInitializable.Initialize()
        {
            _transform = transform;
            _handler = GetComponent<ResourcesHandler>();
        }

        private void FixedUpdate()
        {
            if (_currentOre == null)
            {
                for(int i = 0; i < World.Ores.Count; i++)
                {
                    if (World.Ores[i] == null)
                    {
                        World.Ores.RemoveAt(i);
                    }
                    else if (Vector3.Distance(World.Ores[i].transform.position, _transform.position) < _radius)
                    {
                        _currentOre = World.Ores[i];
                        _startCollecting = false;
                        break;
                    }
                }
             
                return;
            }

            if (Vector3.Distance(_currentOre.transform.position, _transform.position) > _radius)
            {
                _currentOre.StopCollecting();
                _currentOre.OnOreCollected -= ApplyResources;

                OnOreEndCollecting?.Invoke(_currentOre);
                
                _startCollecting = false;
                _currentOre = null;
            }
            else if(!_startCollecting)
            {
                _startCollecting = true;

                OnOreStartCollecting?.Invoke(_currentOre);

                _currentOre.StartCollecting(_power);
                _currentOre.OnOreCollected += ApplyResources;
            }
        }

        private void ApplyResources()
        {
            _handler.IncreaseOre(_currentOre.Amount, _currentOre.Type);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}