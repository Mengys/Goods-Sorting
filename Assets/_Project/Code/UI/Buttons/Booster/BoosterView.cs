using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.Buttons.Booster
{
    public class BoosterView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;
        
        private float _targetWidth;
        private float _targetHeight;

        public Observable<Unit> ButtonClicked => 
            _button.onClick.AsObservable().AsUnitObservable();

        private void Awake()
        {
            _targetWidth = _icon.rectTransform.rect.width;
            _targetHeight = _icon.rectTransform.rect.height;
        }

        public void Initialize(Sprite icon, string name = "", int count = 0)
        {
            _icon.sprite = icon;

            SetNativeSizeFitting(_icon);
          
            _name.text = "";
            _count.text = count.ToString();
        }

        private void SetNativeSizeFitting(Image icon)
        {
            _icon.SetNativeSize();

            // Получаем размеры изображения после SetNativeSize
            float nativeWidth = _icon.rectTransform.rect.width;
            float nativeHeight = _icon.rectTransform.rect.height;

            // Вычисляем масштаб по обеим осям
            float scaleX = _targetWidth / nativeWidth;
            float scaleY = _targetHeight / nativeHeight;

            // Выбираем наименьший масштаб, чтобы сохранить пропорции и вписаться
            float scale = Mathf.Min(scaleX, scaleY);

            // Применяем масштаб
            _icon.rectTransform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}