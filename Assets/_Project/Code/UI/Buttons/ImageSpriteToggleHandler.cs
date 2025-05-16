using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.Buttons
{
    [RequireComponent(typeof(Image))]
    public class ImageSpriteToggleHandler : MonoBehaviour
    {
        [SerializeField] private Sprite _spriteOn;
        [SerializeField] private Sprite _spriteOff;

        private Image _image;

        private void Awake() => 
            _image = GetComponent<Image>();

        public void Toggle(bool isOn) => 
            _image.sprite = isOn ? _spriteOn : _spriteOff;
    }
}