using _Project.Code.Gameplay.Boosters.Boosters;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Code.Gameplay.Boosters
{
    public class BostersInitializeButtons : MonoBehaviour
    {
        [SerializeField] private Button _stopTimerButton;
        [SerializeField] private Button _comboBreakerButton;
        [SerializeField] private Button _replaceObjectsButton;
        [SerializeField] private Button _shuffleButton;
        [SerializeField] private Button _bombButton;

        [Inject] private BoostActivator _boostActivator;

        private void Start()
        {
            _stopTimerButton.onClick.AddListener(_boostActivator.ActivateTimerStopAbility);
            _comboBreakerButton.onClick.AddListener(_boostActivator.ActivateComboBreaker);
            _replaceObjectsButton.onClick.AddListener(_boostActivator.ActivateReplaceObjects);
            _shuffleButton.onClick.AddListener(_boostActivator.ActivateShuffle);
            _bombButton.onClick.AddListener(_boostActivator.ActivateBomb);
        }

        private void OnDisable()
        {
            _stopTimerButton.onClick.RemoveListener(_boostActivator.ActivateTimerStopAbility);
            _comboBreakerButton.onClick.RemoveListener(_boostActivator.ActivateComboBreaker);
            _replaceObjectsButton.onClick.RemoveListener(_boostActivator.ActivateReplaceObjects);
            _shuffleButton.onClick.RemoveListener(_boostActivator.ActivateShuffle);
            _bombButton.onClick.RemoveListener(_boostActivator.ActivateBomb);
        }
    }
}