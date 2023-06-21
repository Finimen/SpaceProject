using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PoolSystem
{
    internal class ObjectPool : MonoBehaviour, IInitializable
    {
        [Serializable]
        private struct PoolingObject
        {
            public GameObject Prefab;
            public int StartCount;
        }

        public struct ConcreteObjectPool
        {
            public GameObject Prefab;
            public Transform Parent;
            public Queue<GameObject> ReadyObjects;
        }

        [SerializeField] private PoolingObject[] _poolingObjects;
        
        private ConcreteObjectPool[] _concreteObjectPools;

        void IInitializable.Initialize()
        {
            _concreteObjectPools = new ConcreteObjectPool[_poolingObjects.Length];

            for (var i = 0; i <  _concreteObjectPools.Length; i++)
            {
                var parent = new GameObject(_poolingObjects[i].Prefab.name).transform;
                parent.transform.parent = transform;

                _concreteObjectPools[i].Parent = parent;
                _concreteObjectPools[i].ReadyObjects = new Queue<GameObject>(_poolingObjects[i].StartCount);
                _concreteObjectPools[i].Prefab = _poolingObjects[i].Prefab;

                for (var j = 0; j < _poolingObjects[i].StartCount; j++)
                {
                    var objectClone = Instantiate(_concreteObjectPools[i].Prefab, parent);
                    objectClone.name = objectClone.name.Replace("(Clone)", "");
                    objectClone.SetActive(false);
                    _concreteObjectPools[i].ReadyObjects.Enqueue(objectClone);
                }
            }
        }

        public GameObject Get(GameObject template)
        {
            UnityEngine.Debug.Log("GET_USED");

            foreach(var pool in _concreteObjectPools)
            {
                if(pool.Prefab == template)
                {
                    if(pool.ReadyObjects.Count == 0)
                    {
                        var objectClone = Instantiate(pool.Prefab, pool.Parent);
                        objectClone.name = objectClone.name.Replace("(Clone)", "");
                        return objectClone;
                    }
                    else
                    {
                        var objectClone = pool.ReadyObjects.Dequeue();
                        objectClone.SetActive(true);
                        return objectClone;
                    }
                }
            }

            return null;
        }

        public void Add(GameObject gameObject)
        {
            UnityEngine.Debug.Log("ADD_USED");

            gameObject.SetActive(false);

            foreach(var pool in _concreteObjectPools)
            {
                if(pool.Prefab.name == gameObject.name)
                {
                    pool.ReadyObjects.Enqueue(gameObject);
                    break;
                }
            }
        }
    }
}