using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform starUIPrefab;
    [SerializeField] private RectTransform starCounterTarget;

    public void SpawnStar(Vector3 worldPosition)
    {
        var parentRect = transform as RectTransform;

        Vector2 screenStart = Camera.main.WorldToScreenPoint(worldPosition);
        Vector2 screenTarget = RectTransformUtility.WorldToScreenPoint(Camera.main, starCounterTarget.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            screenStart,
            Camera.main,
            out Vector2 localStartPos
        );

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            screenTarget,
            Camera.main,
            out Vector2 localTargetPos
        );

        RectTransform star = Instantiate(starUIPrefab, parentRect);
        star.anchoredPosition = localStartPos;
        star.localScale = Vector3.one;

        StartCoroutine(MoveStar(star, localTargetPos));
    }


    private IEnumerator MoveStar(RectTransform star, Vector2 target)
    {
        float duration = 0.6f;
        float elapsed = 0f;
        Vector2 start = star.anchoredPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);
            star.anchoredPosition = Vector2.Lerp(start, target, t);
            star.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            yield return null;
        }

        Destroy(star.gameObject);
    }
}