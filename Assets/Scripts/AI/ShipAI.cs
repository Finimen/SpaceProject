using Assets.Scripts.SpaceShip;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Ship))]
    public class ShipAI : MonoBehaviour
    {
        [SerializeField] private float _distanceToAttack;

        private Ship _ship;
        private Transform _transform;

        private void OnEnable()
        {
            _ship = GetComponent<Ship>();
            _transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            foreach(var entity in World.Entities)
            {
                if(Vector3.Distance(_transform.position, entity.transform.position) < _distanceToAttack)
                {
                    _ship.Movement.SetTargetPoint(entity.transform.position);
                }
            }
        }
    }
}