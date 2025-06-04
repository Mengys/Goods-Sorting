using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IntroPopup : MonoBehaviour
{
    [SerializeField] private RectTransform popupTransform;
    [SerializeField] private Button closeButton;
    [SerializeField] private string prefsKey = "IntroPopupShown";

    private void Start()
    {
        closeButton.onClick.AddListener(ClosePopup);

        if (!PlayerPrefs.HasKey(prefsKey))
        {
            ShowPopup();
            PlayerPrefs.SetInt(prefsKey, 1);
            PlayerPrefs.Save();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void ShowPopup()
    {
        gameObject.SetActive(true);
        popupTransform.localScale = Vector3.zero;

        popupTransform.DOScale(1f, 0.4f)
            .SetEase(Ease.OutBack);
    }

    private void ClosePopup()
    {
        popupTransform.DOScale(0f, 0.3f)
            .SetEase(Ease.InBack)
            .OnComplete(() => gameObject.SetActive(false));
    }
}