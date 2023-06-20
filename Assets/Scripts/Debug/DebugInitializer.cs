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
            foreach(var gameObject in FindObjectsOfType<GameObject>())
            {
                if(gameObject.GetComponent<IInitializable>() != null)
                {
                    gameObject.GetComponent<IInitializable>().Initialize();
                }
            }
        }
    }
}
#endif