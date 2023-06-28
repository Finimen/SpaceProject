using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class OnDisableCreator : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;

        private void OnDisable()
        {
            if (gameObject.scene.isLoaded)
            {
                Instantiate(_prefab, transform.position, transform.rotation);
            }
        }
    }
}