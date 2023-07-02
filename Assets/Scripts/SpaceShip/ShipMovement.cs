using UnityEngine;

namespace Assets.Scripts.SpaceShip
{
    public abstract class ShipMovement : MonoBehaviour, IInitializable
    {
        public abstract float Speed { get; }

        public void Initialize()
        {
            InitializeInternal();
        }

        protected virtual void InitializeInternal()
        {
            UnityEngine.Debug.Log("Initialized");
        }
    }
}