using UnityEngine;

namespace Assets.Scripts
{
    public class OnDestroyCreator : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;

        private void OnDestroy()
        {
            if (gameObject.scene.isLoaded)
            {
                Instantiate(_prefab, transform.position, transform.rotation);
            }
        }
    }
}