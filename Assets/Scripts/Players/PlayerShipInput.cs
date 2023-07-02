using Assets.Scripts.SpaceShip;
using UnityEngine;

namespace Assets.Scripts.Players
{
    [RequireComponent(typeof(Ship))]
    public class PlayerShipInput : MonoBehaviour, IInitializable
    {
        private Joystick _joystick;

        private PlayerMovement _movement;
        private Ship _ship;

        public void Initialize()
        {
            _joystick = FindObjectOfType<Joystick>();

            _ship = GetComponent<Ship>();

            _ship.OnStateUpdated += UpdateEnabled;

            _movement = (PlayerMovement)_ship.Movement;
        }

        private void UpdateEnabled(Ship.ShipState state)
        {
            enabled = state == Ship.ShipState.Gameplay;
        }

        private void Update()
        {
            _movement.SetMovementDirection(_joystick.Direction);
        }
    }
}