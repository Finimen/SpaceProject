using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Damageable
{
    [RequireComponent(typeof(DamageableObject))]
    public class DamageableObjectUIBar : MonoBehaviour
    {
        [SerializeField] private Image _amount;
        [SerializeField] private Image _background;

        [SerializeField] private Color _maxHealthColor;
        [SerializeField] private Color _minHealthColor;

        private DamageableObject _damageableObject;

        private float _maxHealth;

        private void OnEnable()
        {
            _damageableObject = GetComponent<DamageableObject>();

            _damageableObject.OnDamaged += UpdateIU;
            _maxHealth = _damageableObject.Health;
            UpdateIU(_damageableObject.MaxHealth);
        }

        private void UpdateIU(float health) 
        {
            var duration = .25f;

            float amountValue = health / _maxHealth;

            DOTween.Sequence()
                .SetLink(gameObject)
                .Append(_background.DOFade(1, duration))
                .AppendInterval(duration)
                .Append(_background.DOFade(0, duration));

            DOTween.Sequence()
                .SetLink(gameObject)
                .Append(_amount.DOFillAmount(amountValue, duration / 2))
                .Append(_amount.DOColor(Color.Lerp(_minHealthColor, _maxHealthColor, amountValue), duration / 2))
                .AppendInterval(duration)
                .Append(_amount.DOFade(0, duration));
        }
    }
}