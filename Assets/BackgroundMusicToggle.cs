using _Project.Code.UI.Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicToggle : MonoBehaviour
{
    private bool isOn = true;
    private void Start() {
        if (PlayerPrefs.GetInt("IsMusicDisabled") == 1) {
            isOn = false;
            Toggle(false);
        } else {
            isOn = true;
            Toggle(true);
        }
    }

    public void Toggle(bool isOn) {
        PlayerPrefs.SetInt("IsMusicDisabled", isOn ? 0 : 1);
        SoundManager.Instance.IsMusicEnabled = isOn;
        GetComponent<ImageColorToggleHandler>().Toggle(isOn);
    }

    public void Toggle() {
        isOn = !isOn;
        Toggle(isOn);
    }
}
