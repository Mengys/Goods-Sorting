using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowService : MonoBehaviour
{
    [SerializeField] private Image _lossWindow;

    private void Awake()
    {
        RemoveLossWindow();
    }

    public void ShowLossWindow()
    {
        _lossWindow.gameObject.SetActive(true);
    } 
    
    public void RemoveLossWindow()
    {
        _lossWindow.gameObject.SetActive(false);
    }
}