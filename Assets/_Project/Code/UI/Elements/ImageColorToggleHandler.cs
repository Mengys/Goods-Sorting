using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.Elements
{
    [RequireComponent(typeof(Image))]
    public class ImageColorToggleHandler : MonoBehaviour
    {
        [SerializeField] private Color _colorOn = Color.white;
        [SerializeField] private Color _colorOff = Color.gray;

        [SerializeField] private Sprite _audioOnSprite;
        [SerializeField] private Sprite _audioOffSprite;

        [SerializeField] private Sprite _vibrationOnSprite;
        [SerializeField] private Sprite _vibrationOffSprite;

        private Image _image;

        private void Awake() => 
            _image = GetComponent<Image>();

        public void Toggle(bool isOn) => 
            _image.color = isOn ? _colorOn : _colorOff;

        public void ToggleAudio(bool isOn)
        {
            _image.sprite = isOn ? _audioOnSprite : _audioOffSprite;
            AudioListener.volume = isOn ? 1f : 0f;
        }

        public void ToggleVibration(bool isOn)
        {
            _image.sprite = isOn ? _vibrationOnSprite : _vibrationOffSprite;
            PlayerPrefs.SetInt("VibrationEnabled", isOn ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}