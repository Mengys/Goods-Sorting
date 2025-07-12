using _Project.Code.Infrastructure.UIRoot.Implementations;
using _Project.Code.Services.ProgressProvider;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShelfTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _hand;
    [SerializeField] private GameObject _start;
    [SerializeField] private GameObject _finish;

    [Inject] private GameplayUIRoot _gameplayUIRoot;

    private void Start() {
        if (PlayerPrefs.GetInt("ShelfTutorial") == 1) {
            Destroy(gameObject);
            return;
        }

        if (_gameplayUIRoot.Container.Resolve<IProgressProvider>().PlayerProgress.Level.Number != 1) {
            Destroy(gameObject);
            return;
        }
        _hand.SetActive(true);
        _hand.transform.position = _start.transform.position;
        transform.SetAsLastSibling();
        Sequence seq = DOTween.Sequence();

        seq.Append(_hand.transform.DOScale(0.8f, 0.4f))
            .Append(_hand.transform.DOMove(_finish.transform.position, 1f))
            .Append(_hand.transform.DOScale(1, 0.4f))
            .SetLoops(-1);
    }

    private void Update() {
        if (Input.touchCount > 0) {
            PlayerPrefs.SetInt("ShelfTutorial", 1);
            Destroy(gameObject);
        }
    }
}
