using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.Buttons
{
    public class ButtonClickHandler : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public event Action Clicked;

        private void OnEnable() =>
            GetComponent<Button>().onClick.AddListener(OnClicked);
        
        private void OnDisable() =>
            GetComponent<Button>().onClick.RemoveListener(OnClicked);

        protected virtual void OnClicked() => 
            Clicked?.Invoke();
    }
}