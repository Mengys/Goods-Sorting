using System.Collections;
using _Project.Code.Services.CoroutinePerformer;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Code.Services.Curtain
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _animationDuration = 0.1f;

        [Inject] ICoroutinePerformer _coroutinePerformer;

        private bool IsVisible => _canvasGroup.gameObject.activeSelf;

        private Coroutine _coroutine;

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.gameObject.SetActive(false);
        }
    }
}