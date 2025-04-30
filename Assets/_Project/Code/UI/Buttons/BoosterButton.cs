using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Code.UI
{
    public class BoosterButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        
        private Color _selectedColor;
        private Color _unselectedColor;
        private bool _selected;

        public UnityEvent Clicked => _button.onClick;
        
        private void Awake()
        {
            _selectedColor = Color.green;
            _unselectedColor = _image.color;
            
            SetSelected(_selected);
        }

        public void Initialize(BoosterData data)
        {
            _unselectedColor = data.TestColor;  
            SetSelected(_selected);
        }
        
        public void SetSelected(bool selected)
        {
            _selected = selected;
            _image.color = selected ? _selectedColor : _unselectedColor;
        }
    }

    public struct BoosterData
    {
        public Color TestColor;
    }
}