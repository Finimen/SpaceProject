using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    [RequireComponent(typeof(ShipMovement), typeof(Ship))]
    public class ShipMovementUI : MonoBehaviour
    {
        [SerializeField] private LineRenderer _pathLine;

        private ShipMovement _movement;

        private void OnEnable()
        {
            _movement = GetComponent<ShipMovement>();

            GetComponent<Ship>().OnStateUpdated += (state) => _pathLine.gameObject.SetActive(state == Ship.ShipState.Gameplay);

            _pathLine = Instantiate(_pathLine, Vector3.zero, Quaternion.identity);
            _pathLine.gameObject.SetActive(false);

            _pathLine.positionCount = 2;
        }

        private void Update()
        {
            _pathLine.SetPosition(0, transform.position);
            _pathLine.SetPosition(1, _movement.Target);
        }
    }
}