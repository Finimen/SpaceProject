using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.WeaponInstallationSystem
{
    public class WeaponUI : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClicked;

        [SerializeField] private TMP_Text _price;
        [SerializeField] private Image _icon;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke();
        }

        public void Render(WeaponData weapon)
        {
            _icon.sprite = weapon.Icon;
            _price.text = $"{weapon.Price} GCoins";
        }
    }
}