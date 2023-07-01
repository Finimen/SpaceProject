using UnityEngine;

namespace Assets.Scripts.Debug
{
    public class NewPlayerInput : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;

        [SerializeField] private NewShipMovement shipMovement;

        private void Update()
        {
            shipMovement.SetMovementDirection(joystick.Direction);
        }
    }
}