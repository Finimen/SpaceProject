using Assets.Scripts.Damageable;
using Assets.Scripts.Environment;
using Assets.Scripts.PoolSystem;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Assets.Scripts.SpaceShip
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    public class ShipPresenter : DamageableObject, IInitializable
    {
        [SerializeField] private ShipModel _model = new ShipModel();
        [SerializeField] private ShipView _view = new ShipView();

        [SerializeField] private DamageableLevel[] _levels = new DamageableLevel[0];

        private ObjectPool _pool;

        private int _currentLevel;

        public override event Action OnDestroyed;
        public override event Action<float> OnDamaged;

        public override float Health => _model.Health;
        public override float MaxHealth => _model.MaxHealth;

        public override void GetDamage(float amount)
        {
            _model.GetDamage(amount);
            _view.UpdateHealth(_model.Health);

            OnDamaged?.Invoke(Health);

            UpdateDamageLevels();
        }

        public void Initialize()
        {
            _pool = FindObjectOfType<ObjectPool>();

            _model.Initialize();
            _view.Initialize(gameObject);

            _model.OnDestroyed += Dispose;

            _currentLevel = -1;

            World.Entities.Add(this);
        }

        private void UpdateDamageLevels()
        {
            for (int i = 0; i < _levels.Length; i++)
            {
                if (Health < _levels[i].MinHealth && _currentLevel < i)
                {
                    _currentLevel = i;

                    var effect = _pool.Get(_levels[i].EffectsTemplate.gameObject).GetComponent<ObjectChaser>();
                    effect.transform.position = transform.position + new Vector3(
                       Random.Range(-_levels[i].RandomSpawnOffset, _levels[i].RandomSpawnOffset),
                       Random.Range(-_levels[i].RandomSpawnOffset, _levels[i].RandomSpawnOffset));
                    effect.transform.rotation = transform.rotation;
                    effect.Initialize(transform, true);
                }
            }
        }

        private void Dispose()
        {
            _model.OnDestroyed -= Dispose;

            OnDestroyed?.Invoke();

            _view.DestroyView();

            World.Entities.Remove(this);
        }
    }
}