using UnityEngine;

#if UNITY_EDITOR
namespace Assets.Scripts.Debug
{
    /// <summary>
    /// Initializes all objects IInitializable in the scene, for tests only
    /// </summary>
    public class DebugInitializer : MonoBehaviour
    {
        private void Awake()
        {
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