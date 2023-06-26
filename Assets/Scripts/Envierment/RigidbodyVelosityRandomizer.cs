using UnityEngine;

namespace Assets.Scripts.Environment
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal class RigidbodyVelosityRandomizer : MonoBehaviour
    {
        [SerializeField] private float _random = 10;

        private void OnEnable()
        {
            GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(-_random, _random), Random.Range(-_random, _random));
        }
    }
}