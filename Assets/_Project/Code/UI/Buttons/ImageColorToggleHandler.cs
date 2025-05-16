using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.Buttons
{
    [RequireComponent(typeof(Image))]
    public class ImageColorToggleHandler : MonoBehaviour
    {
        [SerializeField] private Color _colorOn = Color.white;
        [SerializeField] private Color _colorOff = Color.gray;

        private Image _image;

        private void Awake() => 
            _image = GetComponent<Image>();

        public void Toggle(bool isOn) => 
            _image.color = isOn ? _colorOn : _colorOff;
    }
}