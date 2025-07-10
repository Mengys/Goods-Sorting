using _Project.Code.Data.Static.Item;
using _Project.Code.Gameplay.DragAndDrop;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Gameplay.Items
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private MyDragAndDrop _dragAndDrop;
        [SerializeField] private Image _image;

        private Color _activeColor;
        private Color _inactiveColor;

        private void Awake()
        {
            _activeColor = _image.color;
            _inactiveColor = GetDarker(_activeColor, 0.85f);
        }

        public IDragAndDropEvents DragAndDropEvents => _dragAndDrop;

        public void UpdateView(ItemConfig config) => 
            _image.sprite = config.Sprite;

        public void SetActive(bool value) => 
            _image.color = value ? _activeColor : _inactiveColor;

        private Color GetDarker(Color color, float factor)
        {
            float multiplier = 1 - factor;
            return new Color(color.r * multiplier, color.g * multiplier, color.b * multiplier, color.a * 0.9f);
        }
    }
}