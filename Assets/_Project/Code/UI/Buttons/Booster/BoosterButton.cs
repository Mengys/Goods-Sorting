using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.Buttons.Booster
{
    public class BoosterButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Button _button;
        [SerializeField] private Sprite _selectedSprite;
        
        private Sprite _unselectedSprite;
        
        public Observable<Unit> Clicked => _button.OnClickAsObservable();
        
        private void Awake() => 
            _unselectedSprite = _image.sprite;

        public void Initialize(BoosterData data) => 
            _name.text = data.Name;

        public void SetSelected(bool value) => 
            _image.sprite = value ? _selectedSprite : _unselectedSprite;
    }
}