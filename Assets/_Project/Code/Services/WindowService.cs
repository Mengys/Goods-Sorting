using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Services
{
    public class WindowService : MonoBehaviour
    {
        [SerializeField] private Image _lossWindow;

        private void Awake()
        {
            RemoveLossWindow();
        }

        public void ShowLossWindow()
        {
            _lossWindow.gameObject.SetActive(true);
        } 
    
        public void RemoveLossWindow()
        {
            _lossWindow.gameObject.SetActive(false);
        }
    }
}