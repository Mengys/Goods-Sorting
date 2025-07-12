using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostAnimations : MonoBehaviour
{
    [SerializeField] private GameObject _bombAnimation;
    [SerializeField] private GameObject _snow;
    [SerializeField] private GameObject _iceTimer;
    [SerializeField] private GameObject _canvas;

    public static BoostAnimations Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void BombAnimation() {
        Instantiate(_bombAnimation, _canvas.transform);
    }

    public void EnableSnow() {
        _snow.SetActive(true);
        _iceTimer.SetActive(true);
    }

    public void DisableSnow() {
        _snow.SetActive(false);
        _iceTimer.SetActive(false);
    }
}
