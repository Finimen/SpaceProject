using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    public class PlayerShipInput : MonoBehaviour, IInitializable
    {
        private Camera _camera;

        private ShipMovement _shipMovement;

        private bool _isSelected;

        public void Initialize()
        {
            _shipMovement = GetComponent<ShipMovement>();

            _camera = FindObjectOfType<Camera>();
        }

        public void EnableInput()
        {
            _isSelected = true;
        }

        public void DisableInput()
        {
            _isSelected = false;
        }

        private void Update()
        {
            if (_isSelected && Input.GetMouseButtonDown(1))
            {
                _shipMovement.SetTargetPoint(_camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
}