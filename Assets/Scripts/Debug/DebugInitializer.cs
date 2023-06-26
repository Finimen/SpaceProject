using UnityEngine;

#if UNITY_EDITOR
namespace Assets.Scripts.Debug
{
    /// <summary>
    /// Initializes all objects IInitializable in the scene, for tests only
    /// </summary>
    public class DebugInitializer : MonoBehaviour
    {
        [SerializeField] private bool _enabled;

        private void Awake()
        {
            if (!_enabled)
            {
                return;
            }

            World.Initialize();

            foreach(var gameObject in FindObjectsOfType<GameObject>())
            {
                if(gameObject.GetComponents<IInitializable>() != null)
                {
                    foreach(var initializable in gameObject.GetComponents<IInitializable>())
                    {
                        initializable.Initialize();
                    }
                }
            }
        }
    }
}
#endif