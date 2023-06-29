using UnityEngine;

namespace Assets.Scripts.TrainingSystem
{
    internal class TrainingStateChanger : MonoBehaviour
    {
        [SerializeField] private Training.TrainingState _state;

        public void SetState()
        {
            FindObjectOfType<Training>().SetState(_state);
        }
    }
}