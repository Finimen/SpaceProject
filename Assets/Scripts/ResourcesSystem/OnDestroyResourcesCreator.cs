using UnityEngine;

namespace Assets.Scripts.ResourcesSystem
{
    public class OnDestroyResourcesCreator : MonoBehaviour
    {
        [System.Serializable]
        private struct ResourceData
        {
            public GameObject Prefab;

            [Range(0, 1)] public float Chance;

            public int MinCount;
            public int MaxCount;
        }

        [SerializeField] private ResourceData[] _resources;

        [Space(25)]
        [SerializeField] private float _randomizePosition = 1;

        private void OnDestroy()
        {
            if (!gameObject.scene.isLoaded)
            {
                return;
            }

            foreach(var resource in _resources)
            {
                if(resource.Chance > Random.Range(0f, 1f))
                {
                    for(int i = 0; i < Random.Range(resource.MinCount, resource.MaxCount); i++)
                    {
                        Instantiate(resource.Prefab, transform.position +
                            new Vector3(Random.Range(-_randomizePosition, _randomizePosition),
                            Random.Range(-_randomizePosition, _randomizePosition)), transform.rotation);
                    }
                }
            }
        }
    }
}