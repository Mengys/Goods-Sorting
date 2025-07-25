using _Project.Code.Infrastructure.UIRoot;
using _Project.Code.Infrastructure.UIRoot.Implementations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BombAnimation : MonoBehaviour
{
    private float time = 1f;
    private Vector2 targetPos = new Vector2(700,1400);
    private RectTransform rectTransform;
    [SerializeField] private GameObject _explosionPref;
    private GameObject _explosion;
    private GameObject _uiRoot;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        _uiRoot = FindFirstObjectByType<GameplayUIRoot>().gameObject;
        _explosion = Instantiate(_explosionPref, transform.parent);
        targetPos = _explosion.transform.position;
        _explosion.SetActive(false);
    }

    void Update()
    {
        if (time < 0) Destroy(gameObject);

        var pos = rectTransform.position;
        pos.x += (targetPos.x - pos.x) / time * Time.deltaTime;
        pos.y += (targetPos.y - pos.y) / time * Time.deltaTime;
        rectTransform.position = pos;
        time -= Time.deltaTime;
    }

    private void OnDestroy() {
        Instantiate(_explosionPref);
    }
}
