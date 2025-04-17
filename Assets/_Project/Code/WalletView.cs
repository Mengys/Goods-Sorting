using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textWallet;

    public void UpdateView(int money)
    {
        _textWallet.text = money.ToString();
    }
}
