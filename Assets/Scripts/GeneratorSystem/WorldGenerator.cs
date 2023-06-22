using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Assets.Scripts.GeneratorSystem
{
    public class WorldGenerator : MonoBehaviour, IInitializable
    {
        [Serializable]
        private struct Template
        {
            public GameObject Prefab;
            public int Count;
        }

        [SerializeField] private Template[] templates;

        [SerializeField] private float _randomPosition = 1000;

        [SerializeField] private float _zPosition = -10;

        void IInitializable.Initialize()
        {
            foreach (var template in templates)
            {
                for (int i = 0; i < template.Count; i++)
                {
                    var newObject = Instantiate(template.Prefab, 
                        new Vector3(Random.Range(-_randomPosition, _randomPosition),
                        Random.Range(-_randomPosition, _randomPosition), _zPosition), 
                        Quaternion.identity, transform);

                    foreach(var initializable in newObject.GetComponents<IInitializable>())
                    {
                        initializable.Initialize();
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;

            Gizmos.DrawWireCube(transform.position, new Vector3(_randomPosition * 2, _randomPosition * 2, 0));
        }
    }
}