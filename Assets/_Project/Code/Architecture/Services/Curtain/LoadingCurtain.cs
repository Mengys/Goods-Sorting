using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _animationDuration = 1.5f;

    private bool IsVisible => _canvasGroup.gameObject.activeSelf;
    
    public IEnumerator Show()
    {
        if (IsVisible) yield break;

        _canvasGroup.alpha = 0;
        
        _canvasGroup.gameObject.SetActive(true);
        
        yield return _canvasGroup.DOFade(1, _animationDuration).WaitForCompletion();
    }

    public IEnumerator Hide()
    {
        if (!IsVisible) yield break;

        _canvasGroup.alpha = 1;
        
        yield return _canvasGroup.DOFade(0, _animationDuration).WaitForCompletion();
        
        _canvasGroup.gameObject.SetActive(false);
    }
}
