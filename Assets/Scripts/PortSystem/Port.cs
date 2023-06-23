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

        [SerializeField] private Button _leavePortButton;

        private Ship _current;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Ship>())
            {
                _current = other.GetComponent<Ship>();

                _current.OnSelectedForTreading += ShowUI;
                _current.OnDeselected += HideUI;

                OnShipEnter?.Invoke(_current);

                _current.transform.position = _treadingPoint.position;
                _current.transform.rotation = _treadingPoint.rotation;

                _current.SetState(Ship.ShipState.Trading);
            }
        }

        private void ShowUI()
        {
            _leavePortButton.gameObject.SetActive(true);
            _leavePortButton.onClick.RemoveAllListeners();
            _leavePortButton.onClick.AddListener(() => LeavePort());
        }

        private void HideUI()
        {
            _leavePortButton.gameObject.SetActive(false);
        }

        private void LeavePort()
        {
            _current.transform.position = _leavingPoint.position;
            _current.transform.rotation = _leavingPoint.rotation;

            _current.SetState(Ship.ShipState.Gameplay);

            _current.OnSelectedForTreading -= ShowUI;
            _current.OnDeselected -= HideUI;

            HideUI();

            OnShipLeave?.Invoke(_current);
        }
    }
}