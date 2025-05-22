using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.Elements.Booster
{
    public class BoosterCellView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;

        [SerializeField] private Button _button;
        [SerializeField] private Selectable _selectable;

        [SerializeField] private GameObject _block;

        public Observable<Unit> Clicked =>
            _button.OnClickAsObservable();

        public void SetCount(int count) =>
            _count.text = count == 0 ? "+" : count.ToString();

        public void SetIcon(Sprite icon) =>
            _icon.sprite = icon;

        public void SetBlocked(bool isBlocked)
        {
            _button.interactable = !isBlocked;
            _block.SetActive(isBlocked);
        }

        public void SetSelected(bool isSelected)
        {
            if (isSelected)
                _selectable?.Select();
            else
                _selectable?.Deselect();
        }
    }
}