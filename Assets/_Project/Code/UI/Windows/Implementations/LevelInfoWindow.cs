using System.Collections.Generic;
using _Project.Code.Data.Dynamic;
using _Project.Code.Data.Static.Booster;
using _Project.Code.Data.Static.GameState;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.ProgressProvider;
using _Project.Code.Services.SceneArgs;
using _Project.Code.Services.StateMachine;
using _Project.Code.UI.Buttons.Booster;
using _Project.Code.UI.Elements;
using _Project.Code.UI.Elements.Booster.Installers;
using _Project.Code.UI.Windows.Base;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Code.UI.Windows.Implementations
{
    public class LevelInitialBooster
    {
        public BoosterId? Value;
    }

    public class LevelInfoWindow : Window
    {
        [SerializeField] private SelectableBoosterInventoryInstaller _inventoryInstaller;
        
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _difficulty;
        [SerializeField] private Button _play;
        [SerializeField] private List<BoosterButton> _boosterButtons;

        private LevelInfo _levelInfo;
        private IStateMachine<GameStateId> _stateMachine;
        private ISceneOutputArgs _outputArgs;

        private readonly CompositeDisposable _disposable = new();

        [Inject]
        public void Construct(
            IProgressProvider progressProvider,
            IStateMachine<GameStateId> stateMachine,
            ISceneOutputArgs outputArgs)
        {
            _outputArgs = outputArgs;
            _levelInfo = progressProvider.PlayerProgress.Level;
            _stateMachine = stateMachine;
        }

        private Observable<Unit> PlayClicked =>
            _play.OnClickAsObservable();

        public override void Initialize()
        {
            _level.text = $"Level {_levelInfo.Number}";
            _difficulty.text = $"{_levelInfo.Difficulty} level";

            PlayClicked
                .Subscribe(_ => OnPlayClicked())
                .AddTo(_disposable);
        }

        private void OnPlayClicked()
        {
            var booster = _outputArgs.Output.Resolve<LevelInitialBooster>();
            
            booster.Value = _inventoryInstaller.GetBooster();

            _stateMachine.Enter(GameStateId.Gameplay);
        }

        public override void OnDestroy() =>
            _disposable.Dispose();
    }
}