using Assets.Scripts.SpaceShip;
using UnityEngine;

namespace Assets.Scripts.PortSystem
{
    public class Port : MonoBehaviour
    {
        [SerializeField] private Transform _shipPoint;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Ship>())
            {
                other.transform.position = _shipPoint.position;
                other.transform.rotation = _shipPoint.rotation;

                other.GetComponent<Ship>().SetState(Ship.ShipState.Trading);
            }
        }
    }
}