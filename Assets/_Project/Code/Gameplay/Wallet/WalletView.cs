using TMPro;
using UnityEngine;

namespace _Project.Code.Gameplay.Wallet
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;

        public void UpdateVievMoney(int money)
        {
            _moneyText.text = money.ToString();
        }
    }
}
