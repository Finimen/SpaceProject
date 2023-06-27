using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Damageable
{
    [RequireComponent(typeof(DamageableObject))]
    public class DamageableObjectUIText : MonoBehaviour
    {
        [SerializeField] protected TMPro.TMP_Text _text;

        private DamageableObject _damageableObject;

        private void OnEnable()
        {
            _damageableObject = GetComponentInChildren<DamageableObject>();
            _damageableObject.OnHealthChanged += OnHealthChanged;

            OnHealthChanged(_damageableObject.MaxHealth);
        }

        private void OnHealthChanged(float health)
        {
            var duration = .25f;

            DOTween.Sequence()
                .SetLink(gameObject)
                .Append(_text.DOFade(1, duration))
                .AppendInterval(duration)
                .Append(_text.DOFade(0, duration));

            _text.text = $"Health: {(int)health}";
        }

        private void OnDestroy()
        {
            _damageableObject.OnHealthChanged -= OnHealthChanged;
        }
    }
}