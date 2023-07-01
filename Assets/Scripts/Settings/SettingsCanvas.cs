using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Settings
{
    public class SettingsCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;

        [SerializeField] private ScaleController[] _windows;

        public void ShowSettingsWindow(ScaleController selected)
        {
            foreach (var window in _windows)
            {
                if(window != selected)
                {
                    window.SetActive(false);
                }
            }

            selected.SetActive(true);
        }
    }
}