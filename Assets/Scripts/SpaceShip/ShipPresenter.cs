using Assets.Scripts.Damageable;
using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    public class ShipPresenter : MonoBehaviour, IDamageable, IInitializable
    {
        [SerializeField] private ShipModel _model;
        [SerializeField] private ShipView _view;

        public float Health => _model.Health;
        public float MaxHealth => _model.MaxHealth;

        void IDamageable.GetDamage(float amount)
        {
            _model.GetDamage(amount);
            _view.UpdateHealth(_model.Health);
        }

        public void Initialize()
        {
            _model.Initialize();
            _view.Initialize(gameObject);

            _model.OnDestroyed += OnDestroyed;
        }

        private void OnDestroyed()
        {
            _model.OnDestroyed -= OnDestroyed;

            _view.DestroyView();
        }
    }
}