using Assets.Scripts.SpaceShip;
using System;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class GameOverController : MonoBehaviour, IInitializable
    {
        public event Action OnGameStopped;

        private Ship _player;

        public void Initialize()
        {
            _player = FindObjectOfType<PlayerShipInput>().GetComponent<Ship>();
            _player.DamageDealer.OnDestroyed += StopGame;
        }

        private void StopGame()
        {
            OnGameStopped?.Invoke();
        }
    }
}