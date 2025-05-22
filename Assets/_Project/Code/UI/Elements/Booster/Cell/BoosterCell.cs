using _Project.Code.Data.Static.Booster;
using _Project.Code.Services.ConfigProvider;
using R3;
using UnityEngine;

namespace _Project.Code.UI.Elements.Booster.Cell
{
    public class BoosterCell
    {
        public readonly ReactiveProperty<bool> IsBlocked;
        public readonly ReactiveProperty<bool> IsSelected;
        
        public readonly ReactiveProperty<BoosterId> Id;
        public readonly ReactiveProperty<int> Count;

        private readonly Subject<BoosterCell> _clicked = new();
        private readonly IConfigProvider _configProvider;
        
        public BoosterCell(
            IConfigProvider configProvider,
            BoosterId id, int count,
            bool isBlocked = false,
            bool isSelected = false)
        {
            _configProvider = configProvider;
            
            Id = new ReactiveProperty<BoosterId>(id);
            Count = new ReactiveProperty<int>(count);
            IsBlocked = new ReactiveProperty<bool>(isBlocked);
            IsSelected = new ReactiveProperty<bool>(isSelected);
        }
        
        public Observable<BoosterCell> Clicked => _clicked;
        public Sprite Icon => GetIcon();

        public void HandleClick() => _clicked?.OnNext(this);

        private Sprite GetIcon()
        {
            var config = _configProvider.ForBooster(Id.Value);

            if (config == null)
                throw new System.ArgumentException($"Booster config not found for id {Id.Value}");

            return config.Value.Asset.Icon;
        }
    }
}