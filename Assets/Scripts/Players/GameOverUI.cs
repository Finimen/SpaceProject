using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Players
{
    [RequireComponent(typeof(GameOverController))]
    internal class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Transform _canvas;

        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        [Space(25)]
        [SerializeField] private CanvasGroup[] _otherCanvases;

        private GameOverController _controller;

        private Vector3 _startScale;

        private void OnEnable()
        {
            _controller = GetComponent<GameOverController>();
            _controller.OnGameStopped += ShowUI;

            _startScale = _canvas.localScale;
            _canvas.localScale = Vector3.zero;
        }

        private void ShowUI()
        {
            _canvas.DOScale(_startScale, _duration).SetEase(_ease);

            foreach(var otherCanvas in _otherCanvases)
            {
                otherCanvas.DOFade(0, _duration).SetEase(_ease);
            }
        }
    }
}