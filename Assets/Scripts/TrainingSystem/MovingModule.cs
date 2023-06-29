using Assets.Scripts.TreadingSystem;
using UnityEngine;

namespace Assets.Scripts.TrainingSystem
{
    internal class MovingModule : TrainingModule
    {
        [SerializeField] private Transform _player;

        [SerializeField] private TargetMovePoint _first;

        public override void Complete()
        {
            FindObjectOfType<Training>().NextState();
        }

        public override void Enable()
        {
            foreach(var movePoint in FindObjectsOfType<TargetMovePoint>(true))
            {
                movePoint.Initialize(_player, this);
            }

            _first.gameObject.SetActive(true);
        }
    }
}
