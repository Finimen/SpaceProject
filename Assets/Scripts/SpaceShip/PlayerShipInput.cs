using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    internal class PlayerShipInput : MonoBehaviour, IInitializable
    {
        [SerializeField] private Camera _camera;

        private ShipMovement _shipMovement;

        void IInitializable.Initialize()
        {
            _shipMovement = GetComponent<ShipMovement>();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(1))
            {
                _shipMovement.SetTargetPoint(_camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
}