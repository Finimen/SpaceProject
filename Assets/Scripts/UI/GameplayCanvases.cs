using Assets.Scripts.SpaceShip;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameplayCanvases : MonoBehaviour
    {
        [SerializeField] private Ship _player;

        [SerializeField] private GameObject[] _gameplayUI;

        private void OnEnable()
        {
            _player.OnStateUpdated += (state) =>
            {
                foreach(var ui in _gameplayUI)
                {
                    ui.SetActive(state == Ship.ShipState.Gameplay);
                }
            };
        }
    }
}