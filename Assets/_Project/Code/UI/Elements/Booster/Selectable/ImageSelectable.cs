using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.Elements.Booster.Selectable
{
    public class ImageSelectable : Selectable
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _selectedSprite;
        [SerializeField] private Sprite _deselectedSprite;

        public override void Select() => 
            _image.sprite = _selectedSprite;

        public override void Deselect() => 
            _image.sprite = _deselectedSprite;
    }
}