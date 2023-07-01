using Assets.Scripts.Players;
using Assets.Scripts.SpaceShip;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PortSystem
{
    public class Port : MonoBehaviour
    {
        public event Action<Ship> OnShipEnter;
        public event Action<Ship> OnShipLeave;

        [SerializeField] private Transform _treadingPoint;
        [SerializeField] private Transform _leavingPoint;

        private Button _leavePortButton;

        private Ship _current;

        public void SetLeavePortButton(Button button)
        {
            _leavePortButton = button;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerShipInput>())
            {
                _current = other.GetComponent<Ship>();

                _current.transform.position = _treadingPoint.position;
                _current.transform.rotation = _treadingPoint.rotation;
                
                ShowUI();
                
                OnShipEnter?.Invoke(_current);
            }
        }

        private void ShowUI()
        {
            _leavePortButton.onClick.RemoveAllListeners();
            _leavePortButton.onClick.AddListener(() => LeavePort());
        }
        
        private void LeavePort()
        {
            _current.transform.position = _leavingPoint.position;
            _current.transform.rotation = _leavingPoint.rotation;

            _current.SetState(Ship.ShipState.Gameplay);

            OnShipLeave?.Invoke(_current);

            _current = null;
        }
    }
}