using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Damageable
{
    [RequireComponent(typeof(DamageableObject))]
    public class HealthRegenerator : MonoBehaviour
    {
        [SerializeField] private float _delay = 1;
        [SerializeField] private float _healthPerFrame = .02f;

        private DamageableObject damageableObject;

        private Coroutine regeneration;

        private void OnEnable()
        {
            damageableObject = GetComponent<DamageableObject>();
            damageableObject.OnHealthChanged += StartRegeneration;
        }

        private void StartRegeneration(float health)
        {
            if(regeneration != null)
            {
                StopCoroutine(regeneration);
            }

            regeneration = StartCoroutine(Regeneration());
        }

        private IEnumerator Regeneration()
        {
            yield return new WaitForSeconds(_delay);

            while(damageableObject.Health < damageableObject.MaxHealth)
            {
                damageableObject.Regenerate(_healthPerFrame * Time.deltaTime);

                yield return null;
            }
        }
    }
}