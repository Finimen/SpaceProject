using System;
using UnityEngine;

using Object = UnityEngine.Object;

namespace Assets.Scripts.SpaceShip
{
    [Serializable]
    internal class ShipView
    {
        [SerializeField] private float _debug;

        private GameObject _viewObject;

        public void Initialize(GameObject viewObject)
        {
            _viewObject = viewObject;
        }

        public void DestroyView()
        {
            if(Application.isPlaying)
            {
                Object.Destroy(_viewObject);
            }
        }

        public void UpdateHealth(float health)
        {
            _debug = health;
        }
    }
}