using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Code.Data.Static.Booster;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.UI.Buttons.Booster;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Code.UI.Windows.Implementations
{
    public class BoostersBar : MonoBehaviour
    {
        [SerializeField] private List<BoosterView> _buttons;

        private IConfigProvider _configProvider;
        private IBoosterInventory _inventory;
        private IBoosterUser _user;

        [Inject]
        public void Construct(IConfigProvider configProvider, IBoosterInventory inventory, IBoosterUser user)
        {
            _user = user;
            _inventory = inventory;
            _configProvider = configProvider;
        }

        private void Start() => Initialze();

        public void Initialze()
        {
            int buttonsCount = _buttons.Count;

            if (_inventory.Boosters.Count != buttonsCount)
                throw new Exception("Boosters count mismatch");

            for (int i = 0; i < buttonsCount; i++)
            {
                BoosterId id = _inventory.Boosters.Keys.ElementAt(i);
                int count = _inventory.Boosters[id];

                var config = _configProvider.ForBooster(id);

                if (!config.HasValue)
                    throw new KeyNotFoundException(id.ToString());

                var assetConfig = config.Value.Asset;

                var button = _buttons[i];

                button.Initialize(assetConfig.Icon, assetConfig.Name, count);

                button.ButtonClicked
                    .Subscribe(_ =>
                    {
                        if (_inventory.Has(id))
                        {
                            _inventory.Remove(id);
                            button.Initialize(assetConfig.Icon, assetConfig.Name, _inventory.Boosters[id]);
                            _user.Use(id);
                        }
                    })
                    .AddTo(button);
            }
        }
    }
}