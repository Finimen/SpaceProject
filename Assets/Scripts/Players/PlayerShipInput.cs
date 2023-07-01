using Assets.Scripts.SpaceShip;
using UnityEngine;

namespace Assets.Scripts.Players
{
    [RequireComponent(typeof(Ship))]
    public class PlayerShipInput : MonoBehaviour, IInitializable
    {
        private Camera _camera;

        private Ship _ship;

        private Vector3 _start;

        private float _maxDistance = 3.5f;

        public void Initialize()
        {
            _ship = GetComponent<Ship>();

            _camera = FindObjectOfType<Camera>();

            _ship.OnStateUpdated += UpdateEnabled;
        }

        private void UpdateEnabled(Ship.ShipState state)
        {
            enabled = state == Ship.ShipState.Gameplay;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _start = Input.mousePosition;
            }

            if(Input.GetMouseButtonUp(0) && Vector3.Distance(_start, Input.mousePosition) < _maxDistance)
            {
                _ship.Movement.SetTargetPoint(_camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
}