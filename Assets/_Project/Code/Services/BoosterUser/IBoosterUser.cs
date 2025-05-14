using _Project.Code.Data.Static.Booster;
using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Services.ConfigProvider;
using Zenject;

namespace _Project.Code.UI.Buttons.Booster
{
    public interface IBoosterUser
    {
        void Use(BoosterId id);
    }

    public class BoosterUser : IBoosterUser
    {
        private readonly IConfigProvider _configProvider;
        private readonly DiContainer _container;

        public BoosterUser(IConfigProvider configProvider, DiContainer container)
        {
            _container = container;
            _configProvider = configProvider;
        }
        
        public void Use(BoosterId id)
        {
            var config = _configProvider.ForBooster(id);
            
            if (config == null)
                throw new System.ArgumentException($"Booster config not found for id {id}");

            IAbility ability = config.Value.Asset.Ability.GetAbility();
            
            ability.Initialize(_container);
            ability.Use();
        }
    }
}