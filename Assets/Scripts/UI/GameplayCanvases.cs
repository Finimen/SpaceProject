using Assets.Scripts.SpaceShip;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameplayCanvases : MonoBehaviour, IInitializable
    {
        [SerializeField] private Ship _player;

        [SerializeField] private GameObject[] _gameplayUI;

        public void SetActive(bool active)
        {
            foreach (var ui in _gameplayUI)
            {
                ui.SetActive(active);
            }
        }

        public void Initialize() 
        {
            _player.OnStateUpdated += (state) =>
            {
                SetActive(state == Ship.ShipState.Gameplay);
            };
        }
    }
}