using UnityEngine;

namespace Assets.Scripts.TreadingSystem
{
    public abstract class TrainingModule : MonoBehaviour
    {
        public abstract void Enable();
        public abstract void Complete();
    }
}