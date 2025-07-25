using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class YandexAdv : MonoBehaviour
{
    public static YandexAdv Instance { get; private set; }
    public string rewardID;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public void ShowInterstitialAdv() {
        YG2.InterstitialAdvShow();
    }

    public void ShowRewardAdv() {
        YG2.RewardedAdvShow(rewardID);
    }
}
