using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    public class PlayerShipInput : MonoBehaviour, IInitializable
    {
        [SerializeField] private Camera _camera;

        private ShipMovement _shipMovement;

        private bool _isSelected;

        void IInitializable.Initialize()
        {
            _shipMovement = GetComponent<ShipMovement>();
        }

        private void Update()
        {
            if (_isSelected && Input.GetMouseButtonDown(1))
            {
                _shipMovement.SetTargetPoint(_camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        public void EnableInput()
        {
            _isSelected = true;
        }

        public void DisableInput()
        {
            _isSelected = false;
        }
    }
}