using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerGlow : MonoBehaviour
{
    void Start()
    {
        transform.DOScale(1.1f,1f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
    }
}
