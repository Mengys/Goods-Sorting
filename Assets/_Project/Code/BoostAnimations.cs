using _Project.Code.Data.Dynamic;
using _Project.Code.Gameplay.GridFeature;
using _Project.Code.UI.Buttons.Window;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BoostAnimations : MonoBehaviour
{
    [SerializeField] private GameObject _bombAnimation;
    [SerializeField] private GameObject _snow;
    [SerializeField] private GameObject _iceTimer;
    [SerializeField] private GameObject _glowTimer;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _rawImage;

    public static BoostAnimations Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void BombAnimation(List<CellGridPosition> positions, IGrid grid, IItemCollectHandler itemCollectHandler) {
        Instantiate(_bombAnimation, _canvas.transform);
        _rawImage.transform.SetAsLastSibling();
        StartCoroutine(DeleteOnTimer(positions, grid, itemCollectHandler));
    }

    IEnumerator DeleteOnTimer(List<CellGridPosition> positions, IGrid grid, IItemCollectHandler itemCollectHandler) {
        float timer = 1f;
        while (timer > 0f) {
            timer -= Time.deltaTime;
            yield return null;
        }
        positions.ForEach(grid.ItemInventory.Clear);
        itemCollectHandler.Handle(positions.Count);
    }

    public void EnableSnow() {
        _snow.SetActive(true);
        _iceTimer.SetActive(true);
        _glowTimer.SetActive(true);
    }

    public void DisableSnow() {
        _snow.SetActive(false);
        _iceTimer.SetActive(false);
        _glowTimer.SetActive(false);
    }
}
