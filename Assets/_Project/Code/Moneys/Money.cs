using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMoney;

    private int _money = 0;

    public int CyrrentMoney => _money;

    private void Start()
    {
        ShoweMoney();
    }

    public void AddMoney(int money)
    {
        _money += money;
        ShoweMoney();
    }

    public void RemoveMoney(int money)
    {
        _money -= money;
        ShoweMoney();
    }

    public void ShoweMoney()
    {
        _textMoney.text = $"Money {_money}";
    }
}
