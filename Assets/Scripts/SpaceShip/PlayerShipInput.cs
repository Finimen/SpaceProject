using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    internal class PlayerShipInput : MonoBehaviour, IInitializable
    {
        [SerializeField] private Camera _camera;

        private ShipPresenter _shipPresenter;
        private ShipMovement _shipMovement;

        void IInitializable.Initialize()
        {
            _shipPresenter = GetComponent<ShipPresenter>();
            _shipMovement = GetComponent<ShipMovement>();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                _shipMovement.SetTargetPoint(_camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
}