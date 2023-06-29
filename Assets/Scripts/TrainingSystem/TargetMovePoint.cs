using Assets.Scripts.TreadingSystem;
using UnityEngine;

namespace Assets.Scripts.TrainingSystem
{
    internal class TargetMovePoint : MonoBehaviour
    {
        [SerializeField] private float _minDistance;

        [SerializeField] private TargetMovePoint _next;

        private TrainingModule _currentModule;

        private Transform _player;

        public void Initialize(Transform player, TrainingModule current)
        {
            _player = player;

            _currentModule  = current;
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, _player.position) < _minDistance)
            {
                if (_next == null)
                {
                    _currentModule.Complete();
                }
                else
                {
                    _next.gameObject.SetActive(true);
                }

                gameObject.SetActive(false);
            }
        }
    }
}