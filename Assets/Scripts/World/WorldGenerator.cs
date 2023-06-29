using Assets.Scripts.AI;
using Assets.Scripts.SpaceShip;
using System;
using System.Collections;
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

        [SerializeField] private bool _generating;

        [Header("Spawning")]
        [SerializeField] private Template[] _templates;

        [SerializeField] private float _randomPosition = 1000;

        [SerializeField] private float _maxDistanceBetweenPlayer = 100;
        [SerializeField] private float _minDistanceForSpawn = 50;
        [SerializeField] private int _playerId = 0;

        [SerializeField] private float _zPosition = -10;

        [Space(25), Header("AI")]
        [SerializeField] private int _aiMaxCount = 5;
        [SerializeField] private float _spawnDelay = 2.5f;
        [SerializeField] private float _minDistanceForShip = 5;
        [SerializeField, Range(0, 1)] private float _spawnChance = .1f;

        private List<Ship> _spawnedShips;

        private List<GameObject> _spawnedObjects;

        private AIFactory _factory;

        public void Initialize()
        {
            _factory = FindObjectOfType<AIFactory>();

            _spawnedObjects = new List<GameObject>();

            _spawnedShips = new List<Ship>(_aiMaxCount);

            StartCoroutine(SpawnAI());

            if (!_generating)
            {
                return;
            }

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

        public void StartGenerating()
        {
            _generating = true;
        }

        public void StopGenerating()
        {
            StopAllCoroutines();
            Destroy(this);
        }

        private IEnumerator SpawnAI()
        {
            while (true)
            {
                if (_generating)
                {
                    if (_spawnedShips.Count < _aiMaxCount && _spawnChance > Random.Range(0f, 1f))
                    {
                        var player = World.Ships.Find(x => x.DamageDealer.Id == _playerId);

                        Ship newAI = _factory.GetShip(player.transform.position, Quaternion.identity, transform);

                        newAI.transform.position += new Vector3(
                            Random.Range(_minDistanceForShip, _maxDistanceBetweenPlayer) * (Random.Range(0, 2) == 0 ? -1 : 1),
                            Random.Range(_minDistanceForShip, _maxDistanceBetweenPlayer) * (Random.Range(0, 2) == 0 ? -1 : 1), _zPosition);

                        newAI.Initialize();
                        newAI.GetComponent<ShipAI>().Initialize();

                        _spawnedShips.Add(newAI);
                    }

                    for (int i = 0; i < _spawnedShips.Count; i++)
                    {
                        if (_spawnedShips[i] == null)
                        {
                            _spawnedShips.RemoveAt(i);
                        }
                        else
                        {
                            var needDestroy = true;

                            foreach (var playerShip in World.Ships.FindAll(x => x.DamageDealer.Id == _playerId))
                            {
                                if (Vector3.Distance(_spawnedShips[i].transform.position, playerShip.transform.position) < _maxDistanceBetweenPlayer)
                                {
                                    needDestroy = false;
                                }
                            }

                            if (needDestroy)
                            {
                                Destroy(_spawnedShips[i].gameObject);
                                _spawnedShips.RemoveAt(i);
                            }
                        }
                    }
                }

                yield return new WaitForSeconds(_spawnDelay);
            }
        }

        private void RemoveUnwantedObjects()
        {
            for (var i = 0; i < _spawnedObjects.Count; i++)
            {
                if (_spawnedObjects[i] != null)
                {
                    var doDeleteObject = true;

                    var player = World.Ships.Find(x => x.DamageDealer.Id == _playerId);

                    if (Vector3.Distance(player.transform.position, _spawnedObjects[i].transform.position) < _maxDistanceBetweenPlayer)
                    {
                        doDeleteObject = false;
                        break;
                    }

                    if (doDeleteObject)
                    {
                        Destroy(_spawnedObjects[i]);
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
                    var player = World.Ships.Find(x => x.DamageDealer.Id == _playerId);

                    var difference = (int)(_maxDistanceBetweenPlayer / _minDistanceForSpawn);
                    newObject.transform.position = player.transform.position +
                        new Vector3(_minDistanceForSpawn * Random.Range(-difference, difference),
                        _minDistanceForSpawn * Random.Range(-difference, difference));
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

            StartCoroutine(WaitForDestroy(newObject, template));

            return newObject;
        }

        private IEnumerator WaitForDestroy(GameObject gameObject, Template template)
        {
            while (gameObject != null)
            {
                yield return null;
            }

            _spawnedObjects.Remove(gameObject);
            template.CurrentCount--;
        }

        private void FixedUpdate()
        {
            if (!_generating)
            {
                return;
            }

            UnityEngine.Debug.Log("TEDFEF");

            RemoveUnwantedObjects();
            CreateNewObjects();
        }

        private void OnDrawGizmos()
        {
            if(World.Ships == null)
            {
                return;
            }

            Gizmos.color = Color.white;

            foreach(var entity in World.Ships)
            {
                if(entity != null)
                {
                    Gizmos.DrawWireSphere(entity.transform.position, _maxDistanceBetweenPlayer);
                }
            }
        }
    }
}