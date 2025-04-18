using _Project.Code;
using Zenject;

public class BoostActivator
{
    private ConfigProvider _configProvider;
    private DiContainer _container;

    public BoostActivator(ConfigProvider configProvider, DiContainer container)
    {
        _configProvider = configProvider;
        _container = container;
    }

    public void ActivateTimerStopAbility()
    {
        var stopTimerAbility = _configProvider.AbilityConfigProvider.GetAbilityConfig<TimeStopAbilityConfig>();

        IAbility stopTimer = stopTimerAbility.GetAbility();
        stopTimer.Initialize(_container);
        stopTimer.Use();
    }

    public void ActivateComboBreaker()
    {
        var comboBreakerAbility = _configProvider.AbilityConfigProvider.GetAbilityConfig<ComboBreakerAbilityConfig>();

        IAbility comboBreaker = comboBreakerAbility.GetAbility();
        comboBreaker.Initialize(_container);
        comboBreaker.Use();
    }

    public void ActivateReplaceObjects()
    {
        var replaceObjectsAbility = _configProvider.AbilityConfigProvider.GetAbilityConfig<ReplaceObjectsAbilityConfig>();

        IAbility replaceObjects = replaceObjectsAbility.GetAbility();
        replaceObjects.Initialize(_container);
        replaceObjects.Use();
    }

    public void ActivateShuffle()
    {
        var shuffleAbility = _configProvider.AbilityConfigProvider.GetAbilityConfig<ShuffleAbilityConfig>();

        IAbility shuffle = shuffleAbility.GetAbility();
        shuffle.Initialize(_container);
        shuffle.Use();
    }

    public void ActivateBomb()
    {
        var bombAbility = _configProvider.AbilityConfigProvider.GetAbilityConfig<BombAbilityConfig>();

        IAbility bomb = bombAbility.GetAbility();
        bomb.Initialize(_container);
        bomb.Use();
    }
}