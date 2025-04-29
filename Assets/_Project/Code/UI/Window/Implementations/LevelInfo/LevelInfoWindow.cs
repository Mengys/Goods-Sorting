using _Project.Code.Gameplay.Level;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.Window.Implementations.LevelInfo
{
    public class LevelInfoWindow : WindowBase
    {
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _difficulty;
        
        [SerializeField] private Button _play;
        [SerializeField] private Button _close;

        public Observable<Unit> PlayClicked => _play.OnClickAsObservable();
        
        private void OnEnable() =>
            _close.onClick.AddListener(Close);
        
        private void OnDisable() =>
            _close.onClick.RemoveListener(Close);
        
        public override void Close() => Destroy(gameObject);

        public void Initialize(int level, DifficultyType difficulty)
        {
            _level.text = $"Level {level}";
            _difficulty.text = $"{difficulty} level";
        }
    }
}