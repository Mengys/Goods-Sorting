using _Project.Code.Infrastructure.UIRoot.Implementations;
using _Project.Code.Services.ProgressProvider;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

public class BustersTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _hand;
    [SerializeField] private List<Transform> _transformList;
    [Inject] private GameplayUIRoot _gameplayUIRoot;

    private void Start()
    {
        _hand.SetActive(true);
        transform.SetAsLastSibling();
        switch (_gameplayUIRoot.Container.Resolve<IProgressProvider>().PlayerProgress.Level.Number) {
            case 2:
                StartMagnetTutorial();
                break;
            case 3:
                StartComboTutorial();
                break;
            case 4:
                StartReplaceTutorial();
                break;
            case 5:
                StartFreezeTutorial();
                break;
            case 6:
                StartShufleTutorial();
                break;
            default:
                Destroy(gameObject);
                break;
        }
        _hand.transform.DOScale(1.5f, 0.6f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update() {
        if (Input.touchCount > 0) {
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(0)) {
            Destroy(gameObject);
        }
    }

    private void StartMagnetTutorial() {
        if (PlayerPrefs.GetInt("MagnetTutorial") == 1) {
            Destroy(gameObject);
            return;
        }
        _hand.transform.position = _transformList[_gameplayUIRoot.Container.Resolve<IProgressProvider>().PlayerProgress.Level.Number - 2].position;
        PlayerPrefs.SetInt("MagnetTutorial",1);
    }

    private void StartComboTutorial() {
        if (PlayerPrefs.GetInt("ComboTutorial") == 1) {
            Destroy(gameObject);
            return;
        }
        _hand.transform.position = _transformList[_gameplayUIRoot.Container.Resolve<IProgressProvider>().PlayerProgress.Level.Number - 2].position;
        PlayerPrefs.SetInt("ComboTutorial", 1);
    }

    private void StartReplaceTutorial() {
        if (PlayerPrefs.GetInt("ReplaceTutorial") == 1) {
            Destroy(gameObject);
            return;
        }
        _hand.transform.position = _transformList[_gameplayUIRoot.Container.Resolve<IProgressProvider>().PlayerProgress.Level.Number - 2].position;
        PlayerPrefs.SetInt("ReplaceTutorial", 1);

    }

    private void StartFreezeTutorial() {
        _hand.transform.position = _transformList[_gameplayUIRoot.Container.Resolve<IProgressProvider>().PlayerProgress.Level.Number - 2].position;
        if (PlayerPrefs.GetInt("FreezeTutorial") == 1) {
            Destroy(gameObject);
            return;
        }
        PlayerPrefs.SetInt("FreezeTutorial", 1);

    }

    private void StartShufleTutorial() {
        if (PlayerPrefs.GetInt("ShufleTutorial") == 1) {
            Destroy(gameObject);
            return;
        }
        _hand.transform.position = _transformList[_gameplayUIRoot.Container.Resolve<IProgressProvider>().PlayerProgress.Level.Number - 2].position;
        PlayerPrefs.SetInt("ShufleTutorial", 1);

    }
}
