using _Project.Code.Gameplay.Boosters.Configs.AbilityConfigProviders;
using _Project.Code.Infrastructure.GameStateMachine.Config;
using _Project.Code.Infrastructure.GameStateMachine.State;
using _Project.Code.Services.ParticlesPlayer.Config;
using _Project.Code.Services.SoundPlayer.Config;
using _Project.Code.Services.UIFactory.Config;
using _Project.Code.UI.Window;

namespace _Project.Code.Services.ConfigProvider
{
    public interface IConfigProvider
    {
        WindowConfig? ForWindow(WindowId id);
        GameStateConfig? ForState(GameStateId id);
        ParticleConfig? ForParticle(ParticleId id);
        SoundConfig? ForSound(SoundId id);
        AbilityConfigProvider AbilityConfigProvider { get; set; }
    }
}