﻿using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Assets.Scripts.GeneratorSystem
{
    public class WorldGenerator : MonoBehaviour, IInitializable
    {
        [Serializable]
        private class Template
        {
            public GameObject Prefab;
            public int Count;
            public int CurrentCount;
        }

        [SerializeField] private Template[] _templates;

        [SerializeField] private float _randomPosition = 1000;

        [SerializeField] private float _maxDistanceBetweenPlayer = 100;
        [SerializeField] private float _minDistanceForSpawn = 50;
        [SerializeField] private int _playerId = 0;

        [SerializeField] private float _zPosition = -10;

        private List<GameObject> _spawnedObjects;

        public void Initialize()
        {
            _spawnedObjects = new List<GameObject>();

            foreach (var template in _templates)
            {
                for (int i = 0; i < template.Count; i++)
                {
                    var newObject = CreateObject(template);

                    foreach (var initializable in newObject.GetComponents<IInitializable>())
                    {
                        initializable.Initialize();
                    }
                }
            }
        }

        private GameObject CreateObject(Template template)
        {
            var newObject = Instantiate(template.Prefab,
                        new Vector3(Random.Range(-_randomPosition, _randomPosition),
                        Random.Range(-_randomPosition, _randomPosition), _zPosition),
                        Quaternion.identity, transform);

            _spawnedObjects.Add(newObject);

            template.CurrentCount++;

            return newObject;
        }

        private void RemoveUnwantedObjects()
        {
            for (var i = 0; i < _spawnedObjects.Count; i++)
            {
                if (_spawnedObjects[i] == null)
                {
                    _spawnedObjects.Remove(_spawnedObjects[i]);
                }
                else
                {
                    var doDeleteObject = true;

                    foreach (var entity in World.Entities)
                    {
                        if (Vector3.Distance(entity.transform.position, _spawnedObjects[i].transform.position) < _maxDistanceBetweenPlayer)
                        {
                            doDeleteObject = false;
                            break;
                        }
                    }

                    if (doDeleteObject)
                    {
                        Destroy(_spawnedObjects[i]);
                        _spawnedObjects.RemoveAt(i);
                    }
                }
            }
        }

        private void CreateNewObjects()
        {
            foreach(var template in _templates)
            {
                if(template.CurrentCount < template.Count)
                {
                    var newObject = CreateObject(template);
                    var playersEntity = World.Entities.Find(x => x.Id == _playerId);
                    newObject.transform.position = 
                        playersEntity.transform.up * _minDistanceForSpawn
                        + playersEntity.transform.right * Random.Range(-_minDistanceForSpawn, _minDistanceForSpawn);
                }
            }
        }

        private void FixedUpdate()
        {
            RemoveUnwantedObjects();
            CreateNewObjects();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;

            Gizmos.DrawWireCube(transform.position, new Vector3(_randomPosition * 2, _randomPosition * 2, 0));
        }
    }
}