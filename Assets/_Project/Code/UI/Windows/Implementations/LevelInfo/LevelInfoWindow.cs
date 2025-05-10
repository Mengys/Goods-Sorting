using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Wallet.Infrastructure.Bootstrappers;
using _Project.Code.Infrastructure.GameStateMachine;
using _Project.Code.Infrastructure.GameStateMachine.State;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.SceneArgs;
using _Project.Code.UI.Buttons;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Code.UI.Windows.Implementations.LevelInfo
{
    public class LevelInfoBoosterProvider
    {
        private IConfigProvider _configProvider;

        public LevelInfoBoosterProvider(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public BoosterData GetRandomBooster()
        {
            var names = new List<string>() { "Bomb", "Shuffle", "Pair", "Replace" };

            var name = names[Random.Range(0, names.Count)];

            return new BoosterData { Name = name, Id = new BoosterId(name) };
        }
    }

    public class LevelLaunchData
    {
        public BoosterId? InitialBooster { get; set; }
    }

    public class LevelInfoWindow : Window
    {
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _difficulty;
        [SerializeField] private Button _play;

        [SerializeField] private List<BoosterButton> _boosterButtons;

        private BoosterButton _selectedBoosterButton;

        private readonly Dictionary<BoosterButton, BoosterData> _boosters = new();
        private readonly CompositeDisposable _disposable = new();

        private LevelInfo _levelInfo;
        private IStateMachine<GameStateId> _stateMachine;
        private LevelInfoBoosterProvider _boosterProvider;
        private ISceneOutputArgs _outputArgs;

        private BoosterData? SelectedBooster
        {
            get
            {
                if (_selectedBoosterButton == null) return null;

                return _boosters.TryGetValue(_selectedBoosterButton, out var booster)
                    ? booster
                    : null;
            }
        }

        [Inject]
        public void Construct(
            PlayerProgress playerProgress,
            IStateMachine<GameStateId> stateMachine,
            LevelInfoBoosterProvider boosterProvider,
            ISceneOutputArgs outputArgs)
        {
            _outputArgs = outputArgs;
            _boosterProvider = boosterProvider;
            _levelInfo = playerProgress.Level;
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

            _boosterButtons.ForEach(b =>
            {
                var booster = _boosterProvider.GetRandomBooster();
                b.Initialize(booster);
                _boosters.Add(b, booster);
            });

            _boosterButtons
                .ForEach(b =>
                    b.Clicked.Subscribe(_ => SelectBooster(b))
                        .AddTo(_disposable));
        }

        private void OnPlayClicked()
        {
            var data = _outputArgs.Output.TryResolve<LevelLaunchData>();

            if (data == null)
            {
                data = new LevelLaunchData();
                _outputArgs.Output.Bind<LevelLaunchData>().FromInstance(data);
            }

            data.InitialBooster = SelectedBooster?.Id;

            _stateMachine.Enter(GameStateId.Gameplay);
        }

        private void SelectBooster(BoosterButton boosterButton)
        {
            if (_selectedBoosterButton == boosterButton)
            {
                boosterButton.SetSelected(false);
                _selectedBoosterButton = null;
                return;
            }

            _selectedBoosterButton?.SetSelected(false);
            _selectedBoosterButton = boosterButton;
            _selectedBoosterButton.SetSelected(true);
        }

        public override void OnDestroy() =>
            _disposable.Dispose();
    }
}