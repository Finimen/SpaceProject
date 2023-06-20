using Assets.Scripts.Damageable;
using System;
using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    public class ShipPresenter : DamageableObject, IInitializable
    {
        [SerializeField] private ShipModel _model;
        [SerializeField] private ShipView _view;

        public override event Action OnDestroyed;

        public override float Health => _model.Health;
        public override float MaxHealth => _model.MaxHealth;

        public override void GetDamage(float amount)
        {
            _model.GetDamage(amount);
            _view.UpdateHealth(_model.Health);
        }

        public void Initialize()
        {
            _model.Initialize();
            _view.Initialize(gameObject);

            _model.OnDestroyed += Dispose;
        }

        private void Dispose()
        {
            _model.OnDestroyed -= Dispose;

            OnDestroyed?.Invoke();

            _view.DestroyView();
        }
    }
}